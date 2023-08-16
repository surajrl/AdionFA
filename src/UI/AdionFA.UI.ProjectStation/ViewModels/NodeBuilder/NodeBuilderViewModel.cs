using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Strategy;
using AdionFA.Infrastructure.NodeBuilder.Contracts;
using AdionFA.Infrastructure.NodeBuilder.Model;
using AdionFA.Infrastructure.Weka.Services;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.EventAggregator;
using AdionFA.UI.ProjectStation.Features;
using AdionFA.UI.ProjectStation.Model.Common;
using AdionFA.UI.ProjectStation.Validators;
using AutoMapper;
using MahApps.Metro.Controls.Dialogs;
using Ninject;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class NodeBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly INodeBuilderService _nodeBuilderService;

        private readonly IProjectService _projectService;
        private readonly IMarketDataService _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly ManualResetEventSlim _manualResetEventSlim;
        private readonly object _lock;
        private bool _disposedValue;

        public NodeBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _nodeBuilderService = IoC.Kernel.Get<INodeBuilderService>();

            _projectService = IoC.Kernel.Get<IProjectService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            // Event aggregators

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(canExecute => CanExecute = canExecute);

            _eventAggregator.GetEvent<ExtractorTemplatesUpdatedEvent>().Subscribe(updated =>
            {
                var canUpdateProcesses =
                NodeBuilderProcesses.All(process => process.Message == BuilderProcessStatus.NBNotStarted.GetMetadata().Name)
                || NodeBuilderProcesses.All(process => process.Message.Contains(BuilderProcessStatus.Canceled.GetMetadata().Name));

                // Only update the extractor templates if the builder process has not started or has been cancelled
                if (updated && canUpdateProcesses)
                {
                    UpdateExtractorTemplates();
                }
            });

            _eventAggregator.GetEvent<BuilderResetEvent>().Subscribe(builderReset =>
            {
                if (builderReset)
                {
                    DeleteNodeBuilder();
                }
            });

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            _lock = new();
            _manualResetEventSlim = new(true);
            _cancellationTokenSource = new();

            NodeBuilderProcesses = new();
            MaxParallelism = Environment.ProcessorCount - 1;

            // Load XML files containing winning nodes
            NodeBuilder = new();
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml").ToList().ForEach(file =>
            {
                NodeBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
            });
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml").ToList().ForEach(file =>
            {
                NodeBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
            });

            UpdateExtractorTemplates();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.NodeBuilderTrim)
            {
                ProjectConfiguration = _mapper.Map<ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            foreach (var process in NodeBuilderProcesses)
            {
                if (process.Message != BuilderProcessStatus.NBCompleted.GetMetadata().Name
                && process.Message != BuilderProcessStatus.NBNotStarted.GetMetadata().Name)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Name;
                    process.Message = $"{process.Message} - {stopped}";
                }
            }

            CanCancelOrContinue = true;
        }, () => IsTransactionActive && !CanCancelOrContinue)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanCancelOrContinue);

        public ICommand Cancel => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
            _manualResetEventSlim.Set();

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand Continue => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Set();

            var stopped = BuilderProcessStatus.Stopped.GetMetadata().Name;
            foreach (var process in NodeBuilderProcesses)
            {
                process.Message = process.Message.Replace($" - {stopped}", string.Empty);
            }
            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        private void DeleteNodeBuilder()
        {
            // Delete node builder nodes
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), ext: "*.xml", isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), ext: "*.xml", isBackup: false);
            // Delete node builder extractions
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory(), isBackup: false);

            NodeBuilder.AllWinningNodes.Clear();

            UpdateExtractorTemplates();
        }

        public ICommand Process => new DelegateCommand(async () =>
        {
            var validator = Validate(new NodeBuilderValidator());
            if (!validator.IsValid)
            {
                MessageHelper.ShowMessages(this,
                    EntityTypeEnum.NodeBuilder.GetMetadata().Name,
                    validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            if (NodeBuilder.AllWinningNodes.Count > 0)
            {
                var deleteAll = await MessageHelper.ShowMessageInputAsync(this,
                    Resources.NodeBuilder,
                    "Starting a new process will delete all the Nodes, Assembly Nodes and Strategy Nodes.\n"
                    + "Do you want to continue?").ConfigureAwait(true);

                if (deleteAll != MessageDialogResult.Affirmative)
                {
                    return;
                }
            }

            try
            {
                IsTransactionActive = true;

                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);
                _eventAggregator.GetEvent<BuilderResetEvent>().Publish(true);

                _cancellationTokenSource = new();

                // Historical data

                var projectCandles = _marketDataService.GetHistoricalData(ProcessArgs.HistoricalDataId, true)
                .HistoricalDataCandles
                .Select(hdCandle => new Candle
                {
                    Date = hdCandle.StartDate,
                    Time = hdCandle.StartTime,

                    Open = hdCandle.Open,
                    High = hdCandle.High,
                    Low = hdCandle.Low,
                    Close = hdCandle.Close,

                    Volume = hdCandle.Volume,
                    Spread = hdCandle.Spread,

                    Label = hdCandle.Close > hdCandle.Open ? "UP" : "DOWN"
                })
                .OrderBy(candle => candle.Date)
                .ThenBy(candle => candle.Time);

                await Task.Factory.StartNew(() =>
                {
                    // Extraction

                    ExtractionProcess(projectCandles);

                    for (CurrentWekaDepth = ProjectConfiguration.NodeBuilderConfiguration.WekaStartDepth; CurrentWekaDepth <= ProjectConfiguration.NodeBuilderConfiguration.WekaEndDepth; CurrentWekaDepth++)
                    {
                        // Find nodes

                        var backtestNodes = GetBacktestNodes();

                        // Backtest of nodes

                        BacktestProcess(backtestNodes, projectCandles);

                        // Check correlation

                        foreach (var process in NodeBuilderProcesses)
                        {
                            process.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                        }

                        NodeBuilder.AllWinningNodes.AddRange(
                            _nodeBuilderService.Correlation<SingleNodeModel>(
                                ProcessArgs.ProjectName,
                                EntityTypeEnum.NodeBuilder,
                                ProjectConfiguration.MaxCorrelationPercent));

                        // Stop if target is met

                        var totalTrades = NodeBuilder.AllWinningNodes.Select(node => node.BacktestIS.TotalTrades + node.BacktestOS.TotalTrades).Sum();

                        if (NodeBuilder.WinningNodesUP.Count >= ProjectConfiguration.NodeBuilderConfiguration.NodesUPTarget
                        && NodeBuilder.WinningNodesDOWN.Count >= ProjectConfiguration.NodeBuilderConfiguration.NodesDOWNTarget
                        && totalTrades >= ProjectConfiguration.NodeBuilderConfiguration.TotalTradesTarget)
                        {
                            break;
                        }

                        // reset the process

                        foreach (var process in NodeBuilderProcesses)
                        {
                            process.CompletedBacktests = 0;
                            process.ExecutingBacktests = 0;
                        }
                    }
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                foreach (var process in NodeBuilderProcesses)
                {
                    process.Message = BuilderProcessStatus.NBCompleted.GetMetadata().Name;
                }

                // Results

                _eventAggregator.GetEvent<NodeBuilderCompletedEvent>().Publish(true);

                var msgUP = NodeBuilder.WinningNodesUP.Count > 0
                ? $"{NodeBuilder.WinningNodesUP.Count} UP Nodes Found"
                : "No UP Nodes Found";

                var msgDOWN = NodeBuilder.WinningNodesDOWN.Count > 0
                ? $"{NodeBuilder.WinningNodesDOWN.Count} DOWN Nodes Found"
                : "No DOWN Nodes Found";

                MessageHelper.ShowMessage(this,
                    Resources.NodeBuilder,
                    $"{Resources.NodeBuilderCompleted}\n\n"
                    + $"{msgUP}\n"
                    + $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                foreach (var strategyBuilderProcess in NodeBuilderProcesses)
                {
                    strategyBuilderProcess.Message = BuilderProcessStatus.Canceled.GetMetadata().Name;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);

                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private void ExtractionProcess(IEnumerable<Candle> projectCandles)
        {
            Parallel.ForEach(
                NodeBuilderProcesses,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                process =>
                {
                    process.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Name;

                    var extractionIndicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        ProjectConfiguration.FromDateIS.Value,
                        ProjectConfiguration.ToDateIS.Value,
                        extractionIndicators,
                        projectCandles.ToList(),
                        ProcessArgs.TimeframeId,
                        ProjectConfiguration.ExtractorMinVariation);

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                    var nameSignature = process.ExtractionTemplateName.Replace(".csv", string.Empty);

                    var extractionName = $"{nameSignature}.{timeSignature}.csv";
                    var extractionPath = ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory(extractionName);
                    _extractorService.ExtractorWrite(
                        extractionPath,
                        extractionResult,
                        0,
                        0);

                    process.ExtractionName = extractionName;
                    process.ExtractionPath = extractionPath;
                    process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Name;
                });
        }

        private List<SingleNodeModel> GetBacktestNodes()
        {
            var backtestNodes = new List<SingleNodeModel>();

            foreach (var process in NodeBuilderProcesses)
            {
                // Weka

                process.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Name;

                var wekaApi = new WekaApiClient();
                var responseWeka = wekaApi.GetREPTreeClassifier(
                    process.ExtractionPath,
                    CurrentWekaDepth,
                    ProjectConfiguration.TotalDecimalWeka,
                    ProjectConfiguration.MinimalSeed,
                    ProjectConfiguration.MaximumSeed,
                    1,
                    (double)ProjectConfiguration.NodeBuilderConfiguration.WekaMaxRatio,
                    ProjectConfiguration.NodeBuilderConfiguration.WekaNTotal);

                // No tree was found
                if (responseWeka.Count == 0)
                {
                    continue;
                }

                process.Tree = responseWeka[0];

                var nodes = process.Tree.NodeOutput
                    .Where(node => node.Winner)
                    .Select(node =>
                    {
                        node.Node = node.Node.OrderByDescending(node => node).ToList();
                        return new SingleNodeModel
                        {
                            NodeData = node,
                            BacktestStatusIS = BacktestStatus.NotStarted,
                            BacktestStatusOS = BacktestStatus.NotStarted
                        };
                    })
                    .ToList();

                backtestNodes.AddRange(nodes);
                process.BacktestNodes.Clear();
                process.BacktestNodes.AddRange(nodes);

                process.Message = process.BacktestNodes.Count == 0
                    ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name // No backtests to do
                    : process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Name;
            }

            return backtestNodes;
        }

        private void BacktestProcess(List<SingleNodeModel> backtestNodes, IEnumerable<Candle> projectCandles)
        {
            var backtestNodesPartition = Partitioner.Create(backtestNodes, EnumerablePartitionerOptions.NoBuffering);

            Parallel.ForEach(
                backtestNodesPartition,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token,
                },
                node =>
                {
                    _manualResetEventSlim.Wait();
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    // Get a reference to the process being backtested
                    var process = NodeBuilderProcesses
                    .Where(process => process.BacktestNodes.Any(processNode => processNode == node))
                    .FirstOrDefault();

                    lock (_lock)
                    {
                        process.ExecutingBacktests++;
                        process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {process.ExecutingBacktests} Nodes";
                    }

                    node.WinningStrategy = _nodeBuilderService.BuildBacktestOfNode(
                        node,
                        projectCandles,
                        _mapper.Map<ProjectConfigurationDTO>(ProjectConfiguration),
                        ProcessArgs.TimeframeId,
                        _manualResetEventSlim,
                        _cancellationTokenSource.Token);

                    if (node.WinningStrategy)
                    {
                        SerializerHelper.SerializeNode(ProcessArgs.ProjectName, node);
                    }

                    lock (_lock)
                    {
                        process.ExecutingBacktests--;
                        process.CompletedBacktests++;
                        process.ProgressCounter++;

                        process.Message = process.CompletedBacktests == process.BacktestNodes.Count
                        ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name
                        : process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {process.ExecutingBacktests} Nodes";
                    }
                });
        }

        private void UpdateExtractorTemplates()
        {
            NodeBuilderProcesses.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
            foreach (var file in extractionTemplates)
            {
                NodeBuilderProcesses.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.NBNotStarted.GetMetadata().Name,
                    Tree = new(),
                    BacktestNodes = new()
                });
            }
        }

        // View Bindings

        private bool _canCancelOrContinue;
        public bool CanCancelOrContinue
        {
            get => _canCancelOrContinue;
            set => SetProperty(ref _canCancelOrContinue, value);
        }

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }

        private int _maxParallelism;
        public int MaxParallelism
        {
            get => _maxParallelism;
            set => SetProperty(ref _maxParallelism, value);
        }

        private NodeBuilderModel _nodeBuilder;
        public NodeBuilderModel NodeBuilder
        {
            get => _nodeBuilder;
            set => SetProperty(ref _nodeBuilder, value);
        }

        private ObservableCollection<BuilderProcess> _nodeBuilderProcesses;
        public ObservableCollection<BuilderProcess> NodeBuilderProcesses
        {
            get => _nodeBuilderProcesses;
            set => SetProperty(ref _nodeBuilderProcesses, value);
        }

        private int _currentWekaDepth;
        public int CurrentWekaDepth
        {
            get => _currentWekaDepth;
            set => SetProperty(ref _currentWekaDepth, value);
        }

        // IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StrategyBuilderViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
