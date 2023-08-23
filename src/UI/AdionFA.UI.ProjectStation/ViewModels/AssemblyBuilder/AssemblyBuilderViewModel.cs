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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class AssemblyBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IBuilderService _builderService;
        private readonly IExtractorService _extractorService;

        private readonly IProjectService _projectService;
        private readonly IMarketDataService _marketDataService;
        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock;
        private bool _disposedValue;

        public AssemblyBuilderViewModel(MainViewModel mainViewModel)
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

            AssemblyBuilder = new();
            AssemblyBuilderProcessesUP = new();
            AssemblyBuilderProcessesDOWN = new();

            ChildNodesUP = new();
            ChildNodesDOWN = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.AssemblyBuilderTrim)
            {
                // Load most recent project configuration

                ProjectConfiguration = _mapper.Map<ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));

                // Load XML files containing winning assembly nodes

                if (!IsTransactionActive)
                {
                    AssemblyBuilder.AllWinningNodes.Clear();

                    _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription()), "*.xml")
                    .ToList()
                    .ForEach(file =>
                    {
                        AssemblyBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                    });

                    _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription()), "*.xml")
                    .ToList()
                    .ForEach(file =>
                    {
                        AssemblyBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                    });
                }
            }
        });

        public ICommand LoadAssemblyBuilderCommand => new DelegateCommand(async () =>
        {
            var load = await MessageHelper.ShowMessageInputAsync(this,
                    Resources.AssemblyBuilder,
                    "Loading a new Assembly Builder will delete all the existing Assembly Nodes\n"
                    + "Do you want to continue?").ConfigureAwait(true);

            if (load != MessageDialogResult.Affirmative)
            {
                return;
            }

            LoadAssemblyBuilder();
        }, () => !IsTransactionActive && CanExecute)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanExecute);

        public ICommand CancelCommand => new DelegateCommand(() => _cancellationTokenSource.Cancel(), () => IsTransactionActive)
            .ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            var validator = Validate(new AssemblyBuilderValidator());
            if (!validator.IsValid)
            {
                MessageHelper.ShowMessagesAsync(this,
                    EntityTypeEnum.AssemblyBuilder.GetMetadata().Name,
                    validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                if (AssemblyBuilder.AllWinningNodes.Count > 0)
                {
                    var process = await MessageHelper.ShowMessageInputAsync(this,
                     Resources.NodeBuilder,
                     "Existing Assembly Nodes found, starting a new process will delete them\n"
                     + "Do you want to continue?").ConfigureAwait(true);

                    if (process != MessageDialogResult.Affirmative)
                    {
                        return;
                    }
                    else
                    {
                        LoadAssemblyBuilder();
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
                    Spread = hdCandle.Spread
                })
                .OrderBy(candle => candle.Date)
                .ThenBy(candle => candle.Time);

                await Task.Factory.StartNew(() =>
                {
                    ExtractionProcess(Domain.Enums.Label.UP, allProjectCandles);
                    ExtractionProcess(Domain.Enums.Label.DOWN, allProjectCandles);

                    CurrentWekaDepth = ProjectConfiguration.AssemblyBuilderConfiguration.WekaStartDepth;
                    while (true)
                    {
                        // Reset processes

                        foreach ((var builderProcessUP, var builderProcessDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (builderProcessUP, builderProcessDOWN) => (builderProcessUP, builderProcessDOWN)))
                        {
                            builderProcessUP.CompletedBacktests = 0;
                            builderProcessUP.ExecutingBacktests = 0;

                            builderProcessDOWN.CompletedBacktests = 0;
                            builderProcessDOWN.ExecutingBacktests = 0;
                        }

                        // Backtest

                        var allBacktestAssemblyNodes = new List<AssemblyNodeModel>();
                        allBacktestAssemblyNodes.AddRange(FindBacktestAssemblyNodes(Domain.Enums.Label.UP));
                        allBacktestAssemblyNodes.AddRange(FindBacktestAssemblyNodes(Domain.Enums.Label.DOWN));
                        BacktestProcess(allBacktestAssemblyNodes, allProjectCandles);

                        // Check correlation

                        foreach ((var builderProcessUP, var builderProcessDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (builderProcessUP, builderProcessDOWN) => (builderProcessUP, builderProcessDOWN)))
                        {
                            builderProcessUP.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                            builderProcessDOWN.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                        }

                        AssemblyBuilder.AllWinningNodes.Clear();
                        AssemblyBuilder.AllWinningNodes.AddRange(
                            _builderService.Correlation<AssemblyNodeModel>(
                                ProcessArgs.ProjectName,
                                EntityTypeEnum.AssemblyBuilder,
                                ProjectConfiguration.MaxCorrelationPercent));

                        // Stop if target is met

                        var totalTrades = AssemblyBuilder.AllWinningNodes.Select(assemblyNode => assemblyNode.BacktestIS.TotalTrades).Sum();

                        if (AssemblyBuilder.WinningNodesUP.Count >= ProjectConfiguration.AssemblyBuilderConfiguration.AssemblyNodesUPTarget
                        && AssemblyBuilder.WinningNodesDOWN.Count >= ProjectConfiguration.AssemblyBuilderConfiguration.AssemblyNodesDOWNTarget
                        && totalTrades >= ProjectConfiguration.AssemblyBuilderConfiguration.TotalTradesTarget)
                        {
                            break;
                        }

                        // Stop if weka depth is met

                        CurrentWekaDepth++;
                        if (CurrentWekaDepth > ProjectConfiguration.AssemblyBuilderConfiguration.WekaEndDepth)
                        {
                            CurrentWekaDepth--;
                            break;
                        }
                    }
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                foreach ((var builderProcessUP, var builderProcessDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (builderProcessUP, builderProcessDOWN) => (builderProcessUP, builderProcessDOWN)))
                {
                    builderProcessUP.Message = BuilderProcessStatus.ABCompleted.GetMetadata().Name;
                    builderProcessDOWN.Message = BuilderProcessStatus.ABCompleted.GetMetadata().Name;
                }

                // Result message

                var msgUP = AssemblyBuilder.WinningNodesUP.Count > 0
                ? $"{AssemblyBuilder.WinningNodesUP.Count} UP Assembly {(AssemblyBuilder.WinningNodesUP.Count == 1 ? "Node" : "Nodes")} Found"
                : "No UP Assembly Nodes Found";

                var msgDOWN = AssemblyBuilder.WinningNodesDOWN.Count > 0
                ? $"{AssemblyBuilder.WinningNodesDOWN.Count} DOWN Assembly {(AssemblyBuilder.WinningNodesDOWN.Count == 1 ? "Node" : "Nodes")} Found"
                : "No DOWN Assembly Nodes Found";

                MessageHelper.ShowMessageAsync(this,
                    Resources.AssemblyBuilder,
                    $"{Resources.AssemblyBuilderCompleted}\n\n" +
                    $"{msgUP}\n" +
                    $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                MessageHelper.ShowMessageAsync(this,
                    Resources.AssemblyBuilder,
                    Resources.AssemblyBuilder + " cancelled");
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
        }, () => !IsTransactionActive && (AssemblyBuilderProcessesUP.Count > 0 || AssemblyBuilderProcessesDOWN.Count > 0))
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => AssemblyBuilderProcessesUP.Count)
            .ObservesProperty(() => AssemblyBuilderProcessesDOWN.Count);

        private void ExtractionProcess(Label label, IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                label == Domain.Enums.Label.UP ? AssemblyBuilderProcessesUP : AssemblyBuilderProcessesDOWN,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = ProjectConfiguration.MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                builderProcess =>
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    builderProcess.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Name;

                    // Perfrom extraction

                    var extractionIndicators = _extractorService.BuildIndicatorsFromCSV(builderProcess.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        builderProcess.PreviousBacktestOperationsIS.First().Date,
                        builderProcess.PreviousBacktestOperationsIS.Last().Date,
                        extractionIndicators,
                        candles.ToList(),
                        ProjectConfiguration.Project.HistoricalData.TimeframeId,
                        0);

                    // Filter the extraction for only the candles with backtest operations

                    var filter = extractionResult[0].IntervalLabels.Select((il, idx) => new { idx, il })
                                  .Where(intervalLabel => builderProcess.PreviousBacktestOperationsIS.Any(backtestOperation => backtestOperation.Date == intervalLabel.il.Interval))
                                  .Select(intervalLabel => new
                                  {
                                      intervalLabel.idx,
                                      il = new IntervalLabel
                                      {
                                          Interval = intervalLabel.il.Interval,
                                          Label = builderProcess.PreviousBacktestOperationsIS.Any(backtestOperation => backtestOperation.IsWinner && backtestOperation.Date == intervalLabel.il.Interval) ? "UP" : "DOWN"
                                      },
                                  })
                                  .ToList();

                    foreach (var extraction in extractionResult)
                    {
                        extraction.IntervalLabels = filter.Select(a => a.il).ToArray();

                        var outputExtraction = new List<double>();
                        foreach (var idx in filter.Select(a => a.idx))
                        {
                            outputExtraction.Add(extraction.Output[idx]);
                        }

                        extraction.Output = outputExtraction.ToArray();
                    }

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                    var nameSignature = builderProcess.ExtractionTemplateName.Replace(".csv", string.Empty);

                    builderProcess.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                    builderProcess.ExtractionPath = ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(label, builderProcess.ExtractionName);

                    _extractorService.ExtractorWrite(
                        builderProcess.ExtractionPath,
                        extractionResult);

                    builderProcess.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Name;
                });
        }

        private List<AssemblyNodeModel> FindBacktestAssemblyNodes(Label label)
        {
            var allBacktestAssemblyNodes = new List<AssemblyNodeModel>();

            foreach (var builderProcess in label == Domain.Enums.Label.UP ? AssemblyBuilderProcessesUP : AssemblyBuilderProcessesDOWN)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Weka 

                builderProcess.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Name;

                var wekaApi = new WekaApiClient();
                var responseWeka = wekaApi.GetREPTreeClassifier(
                        builderProcess.ExtractionPath,
                        CurrentWekaDepth,
                        ProjectConfiguration.TotalDecimalWeka,
                        ProjectConfiguration.MinimalSeed,
                        ProjectConfiguration.MaximumSeed,
                        1,
                        (double)ProjectConfiguration.AssemblyBuilderConfiguration.WekaMaxRatio,
                        ProjectConfiguration.AssemblyBuilderConfiguration.WekaNTotal);

                // No tree found

                if (responseWeka.Count == 0)
                {
                    continue;
                }

                builderProcess.Tree = responseWeka[0];

                // UP   ->  WINNER
                // DOWN ->  LOSER

                var nodes = builderProcess.Tree.NodeOutput
                    .Where(node => node.Winner && node.Label.ToLowerInvariant() == "up")
                    .Select(node =>
                    {
                        node.Node = node.Node
                        .OrderByDescending(node => node)
                        .ToList();

                        return new AssemblyNodeModel
                        {
                            ParentNodeData = node,
                            ChildNodesData = (label == Domain.Enums.Label.UP ? ChildNodesUP : ChildNodesDOWN)
                            .Select(node => node.NodeData)
                            .ToList(),
                            BacktestStatusIS = BacktestStatus.NotStarted,
                            BacktestStatusOS = BacktestStatus.NotStarted,
                        };
                    })
                    .ToList();

                allBacktestAssemblyNodes.AddRange(nodes);
                builderProcess.BacktestAssemblyNodes.Clear();
                builderProcess.BacktestAssemblyNodes.AddRange(nodes);

                builderProcess.Message = builderProcess.BacktestAssemblyNodes.Count == 0
                    ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name // No backtests to do
                    : builderProcess.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Name;
            }

            return allBacktestAssemblyNodes;
        }

        private void BacktestProcess(IEnumerable<AssemblyNodeModel> assemblyNodes, IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                Partitioner.Create(assemblyNodes, EnumerablePartitionerOptions.NoBuffering),
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = ProjectConfiguration.MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                assemblyNode =>
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    var meanSuccessRatePercentIS = assemblyNode.Label == Domain.Enums.Label.UP
                    ? MeanSuccessRatePercentUP
                    : MeanSuccessRatePercentDOWN;

                    // Get a reference to the process being backtested

                    var builderProcess = assemblyNode.Label == Domain.Enums.Label.UP
                    ? AssemblyBuilderProcessesUP.FirstOrDefault(builderProcess => builderProcess.BacktestAssemblyNodes.Any(processNode => processNode == assemblyNode))
                    : AssemblyBuilderProcessesDOWN.FirstOrDefault(builderProcess => builderProcess.BacktestAssemblyNodes.Any(processNode => processNode == assemblyNode));

                    // Lock due to builder process being shared by other threads, as one
                    // builder process could be working on different nodes in parallel

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests++;
                        builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} Nodes";
                    }

                    assemblyNode.WinningStrategy = _builderService.BuildBacktestOfAssemblyNode(
                        assemblyNode,
                        candles,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        meanSuccessRatePercentIS,
                        _cancellationTokenSource.Token);

                    if (assemblyNode.WinningStrategy)
                    {
                        SerializerHelper.SerializeNode(ProcessArgs.ProjectName, assemblyNode);
                    }

                    // Lock due to builder process being shared by other threads, as one
                    // builder process could be working on different nodes in parallel

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests--;
                        builderProcess.CompletedBacktests++;

                        builderProcess.Message = builderProcess.CompletedBacktests == builderProcess.BacktestAssemblyNodes.Count
                        ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name
                        : builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} {(builderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";
                    }
                });
        }

        private void LoadAssemblyBuilder()
        {
            // Remove existing winning assembly nodes

            AssemblyBuilder.AllWinningNodes.Clear();

            // Delete assembly node files from assembly builder

            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription()), "*.xml", false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription()), "*.xml", false);

            // Delete extraction files from assembly builder

            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(Domain.Enums.Label.UP), "*.csv", false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(Domain.Enums.Label.DOWN), "*.csv", false);

            // Load child nodes

            ChildNodesUP.Clear();
            ChildNodesDOWN.Clear();

            // Single nodes UP from node builder

            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml")
            .ToList()
            .ForEach(file =>
            {
                ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
            });

            // Single nodes DOWN from node builder

            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml")
            .ToList()
            .ForEach(file =>
            {
                ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
            });

            MeanSuccessRatePercentUP = ChildNodesUP
                .Select(singleNode => singleNode.BacktestIS.SuccessRatePercent)
                .Sum()
                / ChildNodesUP.Count;

            MeanSuccessRatePercentDOWN = ChildNodesDOWN
                .Select(singleNode => singleNode.BacktestIS.SuccessRatePercent)
                .Sum()
                / ChildNodesDOWN.Count;

            // Load processes

            AssemblyBuilderProcessesUP.Clear();
            AssemblyBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), "*.csv");
            foreach (var file in extractionTemplates)
            {
                if (ChildNodesUP.Count > 0)
                {
                    AssemblyBuilderProcessesUP.Add(new BuilderProcess
                    {
                        ExtractionTemplatePath = file.FullName,
                        ExtractionTemplateName = file.Name,
                        ExtractionName = file.Name,
                        Message = BuilderProcessStatus.ABNotStarted.GetMetadata().Name,
                        Tree = new(),

                        BacktestAssemblyNodes = new(),
                        PreviousBacktestOperationsIS = new(BuilderService.GetBacktestOperations(ChildNodesUP))
                    });
                }

                if (ChildNodesDOWN.Count > 0)
                {
                    AssemblyBuilderProcessesDOWN.Add(new BuilderProcess
                    {
                        ExtractionTemplatePath = file.FullName,
                        ExtractionTemplateName = file.Name,
                        ExtractionName = file.Name,
                        Message = BuilderProcessStatus.ABNotStarted.GetMetadata().Name,
                        Tree = new(),

                        BacktestAssemblyNodes = new(),
                        PreviousBacktestOperationsIS = new(BuilderService.GetBacktestOperations(ChildNodesDOWN))
                    });
                }
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

        private BuilderModel<AssemblyNodeModel> _assemblyBuilder;
        public BuilderModel<AssemblyNodeModel> AssemblyBuilder
        {
            get => _assemblyBuilder;
            set => SetProperty(ref _assemblyBuilder, value);
        }

        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessesUP { get; set; }
        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessesDOWN { get; set; }

        private int _currentWekaDepth;
        public int CurrentWekaDepth
        {
            get => _currentWekaDepth;
            set => SetProperty(ref _currentWekaDepth, value);
        }

        public ObservableCollection<SingleNodeModel> ChildNodesUP { get; }
        public ObservableCollection<SingleNodeModel> ChildNodesDOWN { get; }

        private decimal _meanSuccessRatePercentUP;
        public decimal MeanSuccessRatePercentUP
        {
            get => _meanSuccessRatePercentUP;
            set => SetProperty(ref _meanSuccessRatePercentUP, value);
        }

        private decimal _meanSuccessRatePercentDOWN;
        public decimal MeanSuccessRatePercentDOWN
        {
            get => _meanSuccessRatePercentDOWN;
            set => SetProperty(ref _meanSuccessRatePercentDOWN, value);
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
        // ~AssemblyBuilderViewModel()
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
