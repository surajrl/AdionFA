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
using AdionFA.Infrastructure.Modules.Builder;
using AdionFA.Infrastructure.Weka.Services;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.EventAggregator;
using AdionFA.UI.ProjectStation.Features;
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
using System.IO;
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
        private readonly IBuilderService _builderService;

        private readonly IProjectService _projectService;
        private readonly IMarketDataService _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock;
        private bool _disposedValue;

        public NodeBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _builderService = IoC.Kernel.Get<IBuilderService>();

            _projectService = IoC.Kernel.Get<IProjectService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(canExecute => CanExecute = canExecute);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            _lock = new();
            _cancellationTokenSource = new();

            NodeBuilder = new();
            NodeBuilderProcesses = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.NodeBuilderTrim)
            {
                // Load most recent project configuration

                ProjectConfiguration = _mapper.Map<ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));

                // Load XML files containing winning single nodes

                if (!IsTransactionActive)
                {
                    NodeBuilder.AllWinningNodes.Clear();

                    _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml")
                    .ToList()
                    .ForEach(file =>
                    {
                        NodeBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
                    });

                    _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml")
                    .ToList()
                    .ForEach(file =>
                    {
                        NodeBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
                    });
                }
            }
        });

        public ICommand LoadNodeBuilderCommand => new DelegateCommand(async () =>
        {
            var load = await MessageHelper.ShowMessageInputAsync(this,
                Resources.NodeBuilder,
                "Loading a new Node Builder will delete all the existing Single Nodes\n"
                + "Do you want to continue?").ConfigureAwait(true);

            if (load != MessageDialogResult.Affirmative)
            {
                return;
            }

            LoadNodeBuilder();
        }, () => !IsTransactionActive && CanExecute)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanExecute);

        public ICommand CancelCommand => new DelegateCommand(() => _cancellationTokenSource.Cancel(), () => IsTransactionActive)
            .ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            var validator = Validate(new NodeBuilderValidator());
            if (!validator.IsValid)
            {
                MessageHelper.ShowMessagesAsync(this,
                    EntityTypeEnum.NodeBuilder.GetMetadata().Name,
                    validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                if (NodeBuilder.AllWinningNodes.Count > 0)
                {
                    var process = await MessageHelper.ShowMessageInputAsync(this,
                     Resources.NodeBuilder,
                     "Existing Single Nodes found, starting a new process will delete them\n"
                     + "Do you want to continue?").ConfigureAwait(true);

                    if (process != MessageDialogResult.Affirmative)
                    {
                        return;
                    }
                    else
                    {
                        LoadNodeBuilder();
                    }
                }

                // Historical data

                var allProjectCandles = _marketDataService.GetHistoricalDataCandles(ProjectConfiguration.Project.HistoricalDataId)
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

                    Label = hdCandle.Close > hdCandle.Open ? Domain.Enums.Label.UP.GetDescription() : Domain.Enums.Label.DOWN.GetDescription()
                })
                .OrderBy(candle => candle.Date)
                .ThenBy(candle => candle.Time);

                await Task.Factory.StartNew(() =>
                {
                    ExtractionProcess(allProjectCandles);

                    CurrentWekaDepth = ProjectConfiguration.NodeBuilderConfiguration.WekaStartDepth;
                    while (true)
                    {
                        // Reset processes

                        foreach (var builderProcess in NodeBuilderProcesses)
                        {
                            builderProcess.CompletedBacktests = 0;
                            builderProcess.ExecutingBacktests = 0;
                        }

                        var allBacktestSingleNodes = FindBacktestSingleNodes();
                        BacktestProcess(allBacktestSingleNodes, allProjectCandles);

                        // Check correlation

                        foreach (var builderProcess in NodeBuilderProcesses)
                        {
                            builderProcess.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                        }

                        NodeBuilder.AllWinningNodes.Clear();
                        NodeBuilder.AllWinningNodes.AddRange(
                            _builderService.Correlation<SingleNodeModel>(
                                ProcessArgs.ProjectName,
                                EntityTypeEnum.NodeBuilder,
                                ProjectConfiguration.MaxCorrelationPercent));

                        // Stop if target is met

                        var totalTrades = NodeBuilder.AllWinningNodes.Select(node => node.BacktestIS.TotalTrades).Sum();

                        if (NodeBuilder.WinningNodesUP.Count >= ProjectConfiguration.NodeBuilderConfiguration.NodesUPTarget
                        && NodeBuilder.WinningNodesDOWN.Count >= ProjectConfiguration.NodeBuilderConfiguration.NodesDOWNTarget
                        && totalTrades >= ProjectConfiguration.NodeBuilderConfiguration.TotalTradesTarget)
                        {
                            break;
                        }

                        // Stop if weka depth is met

                        CurrentWekaDepth++;
                        if (CurrentWekaDepth > ProjectConfiguration.NodeBuilderConfiguration.WekaEndDepth)
                        {
                            CurrentWekaDepth--;
                            break;
                        }
                    }
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                foreach (var builderProcess in NodeBuilderProcesses)
                {
                    builderProcess.Message = BuilderProcessStatus.NBCompleted.GetMetadata().Name;
                }

                // Results message

                var msgUP = NodeBuilder.WinningNodesUP.Count > 0
                ? $"{NodeBuilder.WinningNodesUP.Count} UP Single Nodes {(NodeBuilder.WinningNodesUP.Count == 1 ? "Node" : "Nodes")} Found"
                : "No UP Single Nodes Found";

                var msgDOWN = NodeBuilder.WinningNodesDOWN.Count > 0
                ? $"{NodeBuilder.WinningNodesDOWN.Count} DOWN Single Nodes {(NodeBuilder.WinningNodesDOWN.Count == 1 ? "Node" : "Nodes")} Found"
                : "No DOWN Single Nodes Found";

                await MessageHelper.ShowMessageAsync(this,
                    Resources.NodeBuilder,
                    $"{Resources.NodeBuilderCompleted}\n\n"
                    + $"{msgUP}\n"
                    + $"{msgDOWN}")
                .ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                await MessageHelper.ShowMessageAsync(this,
                    Resources.NodeBuilder,
                    Resources.NodeBuilder + " cancelled")
                .ConfigureAwait(true);
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
        }, () => !IsTransactionActive && NodeBuilderProcesses.Count > 0)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => NodeBuilderProcesses.Count);

        private void ExtractionProcess(IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                NodeBuilderProcesses,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = ProjectConfiguration.MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                builderProcess =>
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    builderProcess.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Name;

                    var extractionIndicators = _extractorService.BuildIndicatorsFromCSV(builderProcess.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        ProjectConfiguration.FromDateIS.Value,
                        ProjectConfiguration.ToDateIS.Value,
                        extractionIndicators,
                        candles.ToList(),
                        ProjectConfiguration.Project.HistoricalData.TimeframeId,
                        ProjectConfiguration.ExtractorMinVariation);

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                    var nameSignature = builderProcess.ExtractionTemplateName.Replace(".csv", string.Empty);

                    builderProcess.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                    builderProcess.ExtractionPath = ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory(builderProcess.ExtractionName);

                    _extractorService.ExtractorWrite(
                        builderProcess.ExtractionPath,
                        extractionResult);

                    builderProcess.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Name;
                });
        }

        private List<SingleNodeModel> FindBacktestSingleNodes()
        {
            var allBacktestSingleNodes = new List<SingleNodeModel>();

            foreach (var process in NodeBuilderProcesses)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

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

                // No tree found

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

                allBacktestSingleNodes.AddRange(nodes);
                process.BacktestSingleNodes.Clear();
                process.BacktestSingleNodes.AddRange(nodes);

                process.Message = process.BacktestSingleNodes.Count == 0
                    ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name // No backtests to do
                    : process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Name;
            }

            return allBacktestSingleNodes;
        }

        private void BacktestProcess(List<SingleNodeModel> singleNodes, IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                Partitioner.Create(singleNodes, EnumerablePartitionerOptions.NoBuffering),
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = ProjectConfiguration.MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token,
                },
                singleNode =>
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    // Get a reference to the process being backtested

                    var builderProcess = NodeBuilderProcesses
                    .Where(builderProcess => builderProcess.BacktestSingleNodes.Any(processNode => processNode == singleNode))
                    .FirstOrDefault();

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests++;
                        builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} {(builderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";
                    }

                    singleNode.WinningStrategy = _builderService.BuildBacktestOfSingleNode(
                        singleNode,
                        candles,
                        _mapper.Map<ProjectConfigurationDTO>(ProjectConfiguration),
                        _cancellationTokenSource.Token);

                    if (singleNode.WinningStrategy)
                    {
                        SerializerHelper.SerializeNode(ProcessArgs.ProjectName, singleNode);
                    }

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests--;
                        builderProcess.CompletedBacktests++;

                        builderProcess.Message = builderProcess.CompletedBacktests == builderProcess.BacktestSingleNodes.Count
                        ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name
                        : builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} {(builderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";
                    }
                });
        }

        private void LoadNodeBuilder()
        {
            // Remove existing winning single nodes

            NodeBuilder.AllWinningNodes.Clear();

            // Delete single node files from node builder

            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml", false, SearchOption.TopDirectoryOnly);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml", false, SearchOption.TopDirectoryOnly);

            // Delete extraction files from node builder

            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodeBuilderExtractorWithoutScheduleDirectory(), "*.csv", false, SearchOption.TopDirectoryOnly);

            // Load processes

            NodeBuilderProcesses.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), "*.csv");
            foreach (var file in extractionTemplates)
            {
                NodeBuilderProcesses.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.NBNotStarted.GetMetadata().Name,
                    Tree = new(),
                    BacktestSingleNodes = new()
                });
            }
        }

        // View Bindings

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

        private BuilderModel<SingleNodeModel> _nodeBuilder;
        public BuilderModel<SingleNodeModel> NodeBuilder
        {
            get => _nodeBuilder;
            set => SetProperty(ref _nodeBuilder, value);
        }

        public ObservableCollection<BuilderProcess> NodeBuilderProcesses { get; }

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
