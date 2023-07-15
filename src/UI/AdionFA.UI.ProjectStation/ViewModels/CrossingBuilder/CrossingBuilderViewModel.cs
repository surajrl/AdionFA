using AdionFA.Infrastructure.CrossingBuilder.Contracts;
using AdionFA.Infrastructure.CrossingBuilder.Model;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Weka.Model;
using AdionFA.Infrastructure.Weka.Services;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Common;
using AdionFA.UI.Station.Project.Validators.CrossingBuilder;
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

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class CrossingBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly IStrategyBuilderService _strategyBuilderService;
        private readonly ICrossingBuilderService _crossingBuilderService;

        private readonly IEventAggregator _eventAggregator;
        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly IProjectServiceAgent _projectService;

        private readonly IMapper _mapper;

        private Task _processTask;

        private readonly ManualResetEventSlim _manualResetEventSlim;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock;
        private bool _disposedValue;

        public CrossingBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _strategyBuilderService = IoC.Kernel.Get<IStrategyBuilderService>();
            _crossingBuilderService = IoC.Kernel.Get<ICrossingBuilderService>();

            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            InitializeEventAggregators();

            _mapper = new MapperConfiguration(mapperConfiguration =>
            {
                mapperConfiguration.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            CrossingBuilderProcessesUP = new();
            CrossingBuilderProcessesDOWN = new();
            CrossingBuilder = new();
            CrossingHistoricalData = new();

            MaxParallelism = Environment.ProcessorCount - 1;

            _cancellationTokenSource = new();
            _manualResetEventSlim = new(true);
            _lock = new();

            _crossingBuilderService.LoadExistingCrossingBuilder(ProcessArgs.ProjectName, CrossingBuilder);
            if (CrossingBuilder.WinningStrategyNodesUP.Count == 0 && CrossingBuilder.WinningStrategyNodesDOWN.Count == 0)
            {
                _crossingBuilderService.LoadNewCrossingBuilder(ProcessArgs.ProjectName, CrossingBuilder);
            }

            ResetBuilderProcesses();
        }

        private void InitializeEventAggregators()
        {
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);
            _eventAggregator.GetEvent<ExtractorTemplatesUpdatedEvent>().Subscribe(updated =>
            {
                if (updated
                && !IsTransactionActive
                && CrossingBuilderProcessesUP.All(process => process.Message == BuilderProcessStatus.CBNotStarted.GetMetadata().Description)
                && CrossingBuilderProcessesDOWN.All(process => process.Message == BuilderProcessStatus.CBNotStarted.GetMetadata().Description))
                {
                    ResetBuilderProcesses();
                }
            });
            _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Subscribe(assemblyBuilderCompleted =>
            {
                if (assemblyBuilderCompleted)
                {
                    DeleteCrossingBuilder();
                    _crossingBuilderService.LoadNewCrossingBuilder(ProcessArgs.ProjectName, CrossingBuilder);
                    ResetBuilderProcesses();
                }
                else
                {
                    DeleteCrossingBuilder();
                }
            });
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.CrossingBuilderTrim)
            {
                ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);

                var historicalData = await _marketDataService.GetAllHistoricalDataAsync().ConfigureAwait(true);
                CrossingHistoricalData.Clear();
                CrossingHistoricalData.AddRange(historicalData.Where(hd => hd.HistoricalDataId != ProjectConfiguration.HistoricalDataId).Select(hd => new Metadata
                {
                    Id = hd.HistoricalDataId,
                    Name = hd.Description
                }));
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            // Wait for task to hit WaitOne()

            foreach ((var processUP, var processDOWN) in CrossingBuilderProcessesUP.Zip(CrossingBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                if (processUP.Message != BuilderProcessStatus.CBCompleted.GetMetadata().Description
                    && processUP.Message != BuilderProcessStatus.CBNotStarted.GetMetadata().Description)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;
                    processUP.Message = $"{processUP.Message} - {stopped}";
                }

                if (processDOWN.Message != BuilderProcessStatus.CBCompleted.GetMetadata().Description
                    && processDOWN.Message != BuilderProcessStatus.CBNotStarted.GetMetadata().Description)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;
                    processDOWN.Message = $"{processDOWN.Message} - {stopped}";
                }
            }

            IsStopped = true;
        }, () => IsTransactionActive && !IsStopped)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => IsStopped);

        public ICommand Cancel => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
            _manualResetEventSlim.Set();

            IsStopped = false;
        }, () => IsStopped).ObservesProperty(() => IsStopped);

        public ICommand Continue => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Set();

            var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;
            foreach ((var processUP, var processDOWN) in CrossingBuilderProcessesUP.Zip(CrossingBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                processUP.Message = $"{processUP.Message} - {stopped}";
                processDOWN.Message = $"{processDOWN.Message} - {stopped}";
            }

            IsStopped = false;
        }, () => IsStopped).ObservesProperty(() => IsStopped);

        public ICommand Reset => new DelegateCommand(async () =>
        {
            var reset = await MessageHelper.ShowMessageInput(this,
                    "Crossing Builder",
                    "This will reset the Strategy Nodes.\n"
                    + "Do you want to continue?").ConfigureAwait(true);

            if (reset != MessageDialogResult.Affirmative)
            {
                return;
            }

            DeleteCrossingBuilder();
            _crossingBuilderService.LoadNewCrossingBuilder(ProcessArgs.ProjectName, CrossingBuilder);
            ResetBuilderProcesses();
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand Process => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                var validator = Validate(new CrossingBuilderValidator());
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.CrossingBuilder.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                ResetBuilderProcesses();

                var mainHistoricalData = await _marketDataService.GetHistoricalDataAsync(ProjectConfiguration.HistoricalDataId.Value, true).ConfigureAwait(true);
                var mainCandles = mainHistoricalData.HistoricalDataCandles.Select(candle => new Candle
                {
                    Date = candle.StartDate,
                    Time = candle.StartTime,
                    Open = candle.Open,
                    High = candle.High,
                    Low = candle.Low,
                    Close = candle.Close,
                    Volume = candle.Volume,
                    Spread = candle.Spread
                })
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                var crossingHistoricalData = await _marketDataService.GetHistoricalDataAsync(CrossingHistoricalDataId, true).ConfigureAwait(true);
                var crossingCandles = crossingHistoricalData.HistoricalDataCandles.Select(candle => new Candle
                {
                    Date = candle.StartDate,
                    Time = candle.StartTime,
                    Open = candle.Open,
                    High = candle.High,
                    Low = candle.Low,
                    Close = candle.Close,
                    Volume = candle.Volume,
                    Spread = candle.Spread
                })
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                var symbol = await _marketDataService.GetSymbolAsync(CrossingHistoricalDataId).ConfigureAwait(true);
                CrossingSymbolName = symbol.Name;

                _processTask = Task.Factory.StartNew(() =>
                {
                    ExtractionProcess(CrossingBuilderProcessesUP, "up", crossingCandles);
                    ExtractionProcess(CrossingBuilderProcessesDOWN, "down", crossingCandles);

                    var allBacktests = new List<StrategyNodeModel>
                    {
                        FindBacktestNodes(CrossingBuilderProcessesUP, "up"),
                        FindBacktestNodes(CrossingBuilderProcessesDOWN, "down")
                    };

                    BacktestProcess(allBacktests, mainCandles);

                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                await _processTask;

                ResetBuilderProcesses();
            }
            catch (OperationCanceledException)
            {
                foreach ((var processUP, var processDOWN) in CrossingBuilderProcessesUP.Zip(CrossingBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    var canceled = BuilderProcessStatus.Canceled.GetMetadata().Description;
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;

                    processUP.Message = processUP.Message.Replace(stopped, canceled);
                    processDOWN.Message = processUP.Message.Replace(stopped, canceled);
                }

                IsStopped = false;
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

        private void ExtractionProcess(IList<BuilderProcess> processes, string label, IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                processes,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                process =>
                {
                    process.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Description;

                    var backtestOperations = process.PreviousStrategyNode.BacktestIS.BacktestOperations;

                    // Perfrom extraction
                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        backtestOperations.First().Date,
                        backtestOperations.Last().Date,
                        indicators,
                        candles.ToList(),
                        ProjectConfiguration.TimeframeId);

                    var filter = (from il in extractionResult[0].IntervalLabels.Select((_il, _idx) => new { _idx, _il })
                                  let backtestOperation = backtestOperations.Where(operation => operation.Date == il._il.Interval)
                                  where backtestOperation.Any()
                                  select new
                                  {
                                      idx = il._idx,
                                      il = new IntervalLabel
                                      {
                                          Interval = il._il.Interval,
                                          Label = backtestOperation.Any(operation => operation.IsWinner) ? "UP" : "DOWN"
                                      },
                                  }).ToList();

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

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss.FFFF", CultureInfo.InvariantCulture);
                    var nameSignature = process.ExtractionTemplateName.Replace(".csv", string.Empty);

                    _extractorService.ExtractorWrite(
                        ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv"),
                        extractionResult,
                        0,
                        0);

                    process.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                    process.ExtractionPath = ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv");
                    process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Description;
                });
        }

        private List<StrategyNodeModel> FindBacktestNodes(IEnumerable<BuilderProcess> processes, string label)
        {
            var backtestNodes = new List<StrategyNodeModel>();

            foreach (var process in processes)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Description;

                var wekaApi = new WekaApiClient();
                var responseWeka = wekaApi.GetREPTreeClassifier(
                        process.ExtractionPath,
                        ProjectConfiguration.DepthWeka,
                        ProjectConfiguration.TotalDecimalWeka,
                        ProjectConfiguration.MinimalSeed,
                        ProjectConfiguration.MaximumSeed,
                        ProjectConfiguration.TotalInstanceWeka,
                        (double)ProjectConfiguration.ABWekaMaxRatioTree,
                        (double)ProjectConfiguration.ABWekaNTotalTree);

                if (responseWeka.Count == 0)
                {
                    continue;
                }

                process.Tree = responseWeka[0];

                // UP   ->  WINNER
                // DOWN ->  LOSER
                var nodes = process.Tree.NodeOutput.Where(node => node.Winner && node.Label.ToLowerInvariant() == "up")
                    .Select(node =>
                    {
                        node.Node = node.Node.OrderByDescending(node => node).ToList();
                        node.Label = label.ToUpperInvariant();
                        var newStrategyNode = new StrategyNodeModel
                        {
                            ParentNodeData = new(process.PreviousStrategyNode.ParentNodeData),
                            ChildNodesData = new(process.PreviousStrategyNode.ChildNodesData),
                            CrossingNodesData = new(process.PreviousStrategyNode.CrossingNodesData),
                            BacktestIS = new(),
                            BacktestOS = new(),
                        };

                        newStrategyNode.CrossingNodesData.Add(Tuple.Create(node, CrossingHistoricalDataId, CrossingSymbolName));

                        return newStrategyNode;
                    }).ToList();

                backtestNodes.AddRange(nodes);
                process.BacktestStrategyNodes.Clear();
                process.BacktestStrategyNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                if (process.BacktestStrategyNodes.Count == 0)
                {
                    // No backtests to do
                    process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Description;
                }
                else
                {
                    process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Description;
                }
            }

            return backtestNodes;
        }

        private void BacktestProcess(IEnumerable<StrategyNodeModel> backtestNodes, IEnumerable<Candle> mainCandles)
        {
            var backtestNodesPartition = Partitioner.Create(backtestNodes, EnumerablePartitionerOptions.NoBuffering);

            CrossingBuilder.WinningStrategyNodesUP.Clear();
            CrossingBuilder.WinningStrategyNodesDOWN.Clear();

            Parallel.ForEach(
                backtestNodesPartition,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token,
                },
                backtestingNode =>
                {
                    _manualResetEventSlim.Wait();
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    var process = backtestingNode.ParentNodeData.Label.ToLowerInvariant() == "up"
                    ? CrossingBuilderProcessesUP.Where(process => process.BacktestStrategyNodes.Any(processNode => processNode == backtestingNode)).FirstOrDefault()
                    : CrossingBuilderProcessesDOWN.Where(process => process.BacktestStrategyNodes.Any(processNode => processNode == backtestingNode)).FirstOrDefault();

                    // Lock due to process being shared by other threads, as one
                    // process could be working on various backtests in parallel
                    lock (_lock)
                    {
                        process.ExecutingBacktests++;
                        process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                    }

                    var isWinningNode = _strategyBuilderService.BuildBacktestOfStrategyNode(
                        backtestingNode,
                        mainCandles,
                        process.PreviousStrategyNode.BacktestIS.BacktestOperations,
                        process.PreviousStrategyNode.BacktestOS.BacktestOperations,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        _manualResetEventSlim,
                        _cancellationTokenSource.Token);

                    if (isWinningNode)
                    {
                        SerializerHelper.SerializeStrategyNode(ProcessArgs.ProjectName, backtestingNode);
                        if (backtestingNode.ParentNodeData.Label.ToLowerInvariant() == "up")
                        {
                            CrossingBuilder.WinningStrategyNodesUP.Add(backtestingNode);
                        }
                        else if (backtestingNode.ParentNodeData.Label.ToLowerInvariant() == "down")
                        {
                            CrossingBuilder.WinningStrategyNodesDOWN.Add(backtestingNode);
                        }
                    }

                    // Lock due to process being shared by other threads, as one
                    // process could be working on various backtests in parallel
                    lock (_lock)
                    {
                        process.ExecutingBacktests--;
                        process.CompletedBacktests++;
                        process.ProgressCounter++;

                        process.Message = process.CompletedBacktests == process.BacktestStrategyNodes.Count
                        ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Description
                        : process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                    }
                });
        }

        private void ResetBuilderProcesses()
        {

            CrossingBuilderProcessesUP.Clear();
            CrossingBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
            foreach (var file in extractionTemplates)
            {
                foreach (var strategyNode in CrossingBuilder.WinningStrategyNodesUP)
                {
                    CrossingBuilderProcessesUP.Add(new BuilderProcess
                    {
                        ExtractionTemplatePath = file.FullName,
                        ExtractionTemplateName = file.Name,
                        ExtractionName = file.Name,
                        Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Description,
                        Tree = new(),
                        BacktestStrategyNodes = new(),
                        PreviousStrategyNode = strategyNode
                    });
                }
                foreach (var strategyNode in CrossingBuilder.WinningStrategyNodesDOWN)
                {
                    CrossingBuilderProcessesDOWN.Add(new BuilderProcess
                    {
                        ExtractionTemplatePath = file.FullName,
                        ExtractionTemplateName = file.Name,
                        ExtractionName = file.Name,
                        Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Description,
                        Tree = new(),
                        BacktestStrategyNodes = new(),
                        PreviousStrategyNode = strategyNode
                    });
                }
            }
        }

        private void DeleteCrossingBuilder()
        {
            // Delete Crossing Builder Nodes
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderNodesUPDirectory(), "*.xml", isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderNodesDOWNDirectory(), "*.xml", isBackup: false);
            // Delete Crossing Builder Extractions
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory("up"), isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory("down"), isBackup: false);

            CrossingBuilder.WinningStrategyNodesUP.Clear();
            CrossingBuilder.WinningStrategyNodesDOWN.Clear();

            ResetBuilderProcesses();
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

        private bool _isStopped;
        public bool IsStopped
        {
            get => _isStopped;
            set => SetProperty(ref _isStopped, value);
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

        private CrossingBuilderModel _crossingBuilder;

        public CrossingBuilderModel CrossingBuilder
        {
            get => _crossingBuilder;
            set => SetProperty(ref _crossingBuilder, value);
        }

        public ObservableCollection<BuilderProcess> CrossingBuilderProcessesUP { get; }
        public ObservableCollection<BuilderProcess> CrossingBuilderProcessesDOWN { get; }

        public ObservableCollection<Metadata> CrossingHistoricalData { get; }
        public int CrossingHistoricalDataId { get; set; }
        public string CrossingSymbolName { get; set; }

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
        // ~CrossingBuilderViewModel()
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
