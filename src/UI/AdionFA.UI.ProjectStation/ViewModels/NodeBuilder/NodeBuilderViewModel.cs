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
using AdionFA.Infrastructure.Modules.Weka.Model;
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
using AdionFA.UI.ProjectStation.Validators.NodeBuilder;
using AutoMapper;
using DynamicData;
using MahApps.Metro.Controls.Dialogs;
using Ninject;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using ReactiveUI;
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

        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly ManualResetEventSlim _manualResetEventSlim;
        private readonly object _lock;
        private bool _disposedValue;

        public NodeBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectService = IoC.Kernel.Get<IProjectService>();
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _nodeBuilderService = IoC.Kernel.Get<INodeBuilderService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            // Event Aggregators

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            _eventAggregator.GetEvent<ExtractorTemplatesUpdatedEvent>().Subscribe(updated =>
            {
                if (updated
                && !IsTransactionActive
                && (NodeBuilderProcesses.All(process => process.Message == BuilderProcessStatus.SBNotStarted.GetMetadata().Name)
                || NodeBuilderProcesses.All(process => process.Message.Contains(BuilderProcessStatus.Canceled.GetMetadata().Name))))
                {
                    ResetBuilderProcesses();
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

            // Load XML files containing Winning Nodes
            NodeBuilder = new();
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodeBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                NodeBuilder.WinningNodesUP.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
            });
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodeBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                NodeBuilder.WinningNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
            });

            ResetBuilderProcesses();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.NodeBuilderTrim)
            {
                ProjectConfiguration = _mapper.Map<ProjectConfigurationDTO, ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            foreach (var process in NodeBuilderProcesses)
            {
                if (process.Message != BuilderProcessStatus.SBCompleted.GetMetadata().Name
                && process.Message != BuilderProcessStatus.SBNotStarted.GetMetadata().Name)
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

        private void DeleteStrategyBuilder()
        {
            // Delete Strategy Builder Nodes
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodeBuilderNodesUPDirectory(), isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodeBuilderNodesUPDirectory(), isBackup: false);
            // Delete Strategy Builder Extractions
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory(), isBackup: false);

            NodeBuilder.WinningNodesUP.Clear();
            NodeBuilder.WinningNodesDOWN.Clear();

            ResetBuilderProcesses();
        }

        public ICommand Process => new DelegateCommand(async () =>
        {
            try
            {
                var validator = Validate(new NodeBuilderValidator());
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.StrategyBuilder.GetMetadata().Name,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                var deleteAll = await MessageHelper.ShowMessageInput(this,
                    Resources.StrategyBuilder,
                    "Starting a new process will delete all the Nodes, Assembly Nodes and Strategy Nodes.\n"
                    + "Do you want to continue?").ConfigureAwait(true);

                if (deleteAll != MessageDialogResult.Affirmative)
                {
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                DeleteStrategyBuilder();

                _eventAggregator.GetEvent<NodeBuilderCompletedEvent>().Publish(false);
                _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Publish(false);

                // Historical Data

                var allProjectCandles = ProcessArgs.Project.HistoricalData.HistoricalDataCandles
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
                    ExtractionProcess(allProjectCandles);
                    BacktestProcess(GetBacktestNodes(), allProjectCandles);
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                // Correlation Analysis

                foreach (var process in NodeBuilderProcesses)
                {
                    process.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                }

                await Task.Run(() =>
                {
                    _nodeBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        NodeBuilder,
                        ProjectConfiguration.SBMaxCorrelationPercent);
                });

                foreach (var process in NodeBuilderProcesses)
                {
                    process.Message = BuilderProcessStatus.SBCompleted.GetMetadata().Name;
                }

                // Results Message

                _eventAggregator.GetEvent<NodeBuilderCompletedEvent>().Publish(true);

                var msgUP = NodeBuilder.WinningNodesUP.Count > 0
                ? $"{NodeBuilder.WinningNodesUP.Count} UP Nodes Found"
                : "No UP Nodes Found";

                var msgDOWN = NodeBuilder.WinningNodesDOWN.Count > 0
                ? $"{NodeBuilder.WinningNodesDOWN.Count} DOWN Nodes Found"
                : "No DOWN Nodes Found";

                MessageHelper.ShowMessage(this,
                    Resources.StrategyBuilder,
                    $"{Resources.StrategyBuilderCompleted}\n\n"
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

                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        ProjectConfiguration.FromDateIS.Value,
                        ProjectConfiguration.ToDateIS.Value,
                        indicators,
                        projectCandles.ToList(),
                        ProcessArgs.Project.HistoricalData.TimeframeId,
                        ProjectConfiguration.ExtractorMinVariation);

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                    var nameSignature = process.ExtractionTemplateName.Replace(".csv", string.Empty);

                    _extractorService.ExtractorWrite(
                        ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv"),
                        extractionResult,
                        0,
                        0);

                    process.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                    process.ExtractionPath = ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv");
                    process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Name;
                });
        }

        private List<NodeModel> GetBacktestNodes()
        {
            var backtestNodes = new List<NodeModel>();

            foreach (var process in NodeBuilderProcesses)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Weka

                process.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Name;

                var wekaApi = new WekaApiClient();
                var responseWeka = wekaApi.GetREPTreeClassifier(
                    process.ExtractionPath,
                    ProjectConfiguration.DepthWeka,
                    ProjectConfiguration.TotalDecimalWeka,
                    ProjectConfiguration.MinimalSeed,
                    ProjectConfiguration.MaximumSeed,
                    ProjectConfiguration.TotalInstanceWeka,
                    (double)ProjectConfiguration.MaxRatioTree,
                    (double)ProjectConfiguration.NTotalTree);

                process.Tree = responseWeka[0];

                var nodes = process.Tree.NodeOutput.Where(node => node.Winner)
                .Select(node =>
                {
                    node.Node = node.Node.OrderByDescending(node => node).ToList();
                    return new NodeModel
                    {
                        NodeData = node
                    };
                })
                .ToList();

                backtestNodes.AddRange(nodes);
                process.BacktestNodes.Clear();
                process.BacktestNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Name;
            }

            return backtestNodes;
        }

        private void BacktestProcess(List<NodeModel> backtestNodes, IEnumerable<Candle> projectCandles)
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

                    var process = NodeBuilderProcesses.Where(process => process.BacktestNodes.Any(processNode => processNode == node)).FirstOrDefault();

                    lock (_lock)
                    {
                        process.ExecutingBacktests++;
                        process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {process.ExecutingBacktests} Nodes";
                    }

                    var winningNode = _nodeBuilderService.BuildBacktestOfNode(
                            node,
                            projectCandles,
                            _mapper.Map<ProjectConfigurationDTO>(ProjectConfiguration),
                            ProcessArgs.Project.HistoricalData.TimeframeId,
                            _manualResetEventSlim,
                            _cancellationTokenSource.Token);

                    if (winningNode)
                    {
                        SerializerHelper.SerializeNode(EntityTypeEnum.StrategyBuilder, ProcessArgs.ProjectName, node);
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

        private void ResetBuilderProcesses()
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
                    Message = BuilderProcessStatus.SBNotStarted.GetMetadata().Name,
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
