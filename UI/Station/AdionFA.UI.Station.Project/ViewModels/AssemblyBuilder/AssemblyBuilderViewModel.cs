using AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Common;
using AutoMapper;
using DynamicData;
using Microsoft.CodeAnalysis;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class AssemblyBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IAssemblyBuilderService _assemblyBuilderService;
        private readonly IStrategyBuilderService _strategyBuilderService;
        private readonly IExtractorService _extractorService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly IEventAggregator _eventAggregator;

        private readonly ManualResetEventSlim _manualResetEventSlim;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock;
        private bool _disposedValue;

        public AssemblyBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
            _assemblyBuilderService = IoC.Get<IAssemblyBuilderService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);
            _eventAggregator.GetEvent<StrategyBuilderCompletedEvent>().Subscribe(strategyBuilderCompleted =>
            {
                if (strategyBuilderCompleted)
                {
                    // Load the new Correlation Nodes
                    AssemblyBuilder = _assemblyBuilderService.LoadAssemblyBuilder(ProcessArgs.ProjectName);
                }
                else
                {
                    // New process starting ...
                    AssemblyBuilder.WinningAssemblyNodesUP.Clear();
                    AssemblyBuilder.WinningAssemblyNodesDOWN.Clear();
                    AssemblyBuilder.ChildNodesUP.Clear();
                    AssemblyBuilder.ChildNodesDOWN.Clear();
                }
            });

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            _cancellationTokenSource = new();
            _manualResetEventSlim = new(true);
            _lock = new();

            AssemblyBuilderProcessesUP = new();
            AssemblyBuilderProcessesDOWN = new();
            MaxParallelism = Environment.ProcessorCount - 1;
            AssemblyBuilder = _assemblyBuilderService.LoadAssemblyBuilder(ProcessArgs.ProjectName);
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.AssemblyBuilder.Replace(" ", string.Empty))
            {
                try
                {
                    ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);

                    if (!IsTransactionActive
                        && AssemblyBuilderProcessesDOWN.All(process => process.Message == BuilderProcessStatus.ABNotStarted.GetMetadata().Description)
                        && AssemblyBuilderProcessesUP.All(process => process.Message == BuilderProcessStatus.ABNotStarted.GetMetadata().Description))
                    {
                        ResetBuilder();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException<AssemblyBuilderViewModel>(ex);
                    throw;
                }
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                if (processUP.Message != BuilderProcessStatus.ABCompleted.GetMetadata().Description
                    && processUP.Message != BuilderProcessStatus.ABNotStarted.GetMetadata().Description)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;
                    processUP.Message = $"{processUP.Message} - {stopped}";
                }

                if (processDOWN.Message != BuilderProcessStatus.ABCompleted.GetMetadata().Description
                    && processDOWN.Message != BuilderProcessStatus.ABNotStarted.GetMetadata().Description)
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
            foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                processUP.Message = $"{processUP.Message} - {stopped}";
                processDOWN.Message = $"{processDOWN.Message} - {stopped}";
            }

            IsStopped = false;
        }, () => IsStopped).ObservesProperty(() => IsStopped);

        public ICommand Process => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                ResetBuilder();

                AssemblyBuilder.WinningAssemblyNodesUP.Clear();
                AssemblyBuilder.WinningAssemblyNodesDOWN.Clear();

                // Delete Assembly Builder Extractions
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("up"), isBackup: false);
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("down"), isBackup: false);
                // Delete Assembly Builder Nodes
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml", isBackup: false);
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml", isBackup: false);


                // Historical Data

                var projectHistoricalData = await _marketDataService.GetHistoricalDataAsync(ProjectConfiguration.HistoricalDataId.Value, true);
                var allProjectCandles = projectHistoricalData.HistoricalDataCandles.
                Select(hdCandle => new Candle
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
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                await Task.Factory.StartNew(() =>
                {
                    if (AssemblyBuilder.ChildNodesUP.Count > 0 && AssemblyBuilder.ChildNodesDOWN.Count > 0)
                    {
                        ExtractionProcess("up", allProjectCandles);
                        ExtractionProcess("down", allProjectCandles);

                        var backtestNodes = new List<AssemblyNodeModel>
                        {
                            GetAssemblyNodeBacktests(AssemblyBuilderProcessesUP, "up"),
                            GetAssemblyNodeBacktests(AssemblyBuilderProcessesDOWN, "down")
                        };

                        BacktestProcess(backtestNodes, allProjectCandles);
                    }
                    else if (AssemblyBuilder.ChildNodesUP.Count > 0)
                    {
                        ExtractionProcess("up", allProjectCandles);
                        BacktestProcess(GetAssemblyNodeBacktests(AssemblyBuilderProcessesUP, "up"), allProjectCandles);
                    }
                    else if (AssemblyBuilder.ChildNodesDOWN.Count > 0)
                    {
                        ExtractionProcess("down", allProjectCandles);
                        BacktestProcess(GetAssemblyNodeBacktests(AssemblyBuilderProcessesDOWN, "down"), allProjectCandles);
                    }
                    else
                    {
                        return;
                    }

                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    processUP.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Description;
                    processDOWN.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Description;
                }

                await Task.Run(() =>
                {
                    _strategyBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        AssemblyBuilder,
                        ProjectConfiguration.SBMaxCorrelationPercent);
                });

                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    processUP.Message = BuilderProcessStatus.ABCompleted.GetMetadata().Description;
                    processDOWN.Message = BuilderProcessStatus.ABCompleted.GetMetadata().Description;
                }

                // Result Message

                _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Publish(true);

                var msgUP = AssemblyBuilder.WinningAssemblyNodesUP.Count > 0 ? $"{AssemblyBuilder.WinningAssemblyNodesUP.Count} UP Assembly Nodes Found." : "No UP Assembly Nodes Found.";
                var msgDOWN = AssemblyBuilder.WinningAssemblyNodesDOWN.Count > 0 ? $"{AssemblyBuilder.WinningAssemblyNodesDOWN.Count} DOWN Assembly Nodes Found." : "No DOWN Assembly Nodes Found.";

                MessageHelper.ShowMessage(this,
                    CommonResources.AssemblyBuilder,
                    $"{MessageResources.AssemblyBuilderCompleted}.\n\n" +
                    $"{msgUP}\n" +
                    $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
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
                LogHelper.LogException<AssemblyBuilderViewModel>(ex);
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

        // ONLY IMPLEMENTED WITHOUT SCHEDULE EXTRACTIONS !!
        private void ExtractionProcess(string label, IEnumerable<Candle> candles)
        {
            IList<BacktestModel> backtests;
            IList<BuilderProcess> assemblyBuilderProcess;

            switch (label.ToLowerInvariant())
            {
                case "up":
                    backtests = AssemblyBuilder.ChildNodesUP.Select(node => node.BacktestIS).ToList();
                    assemblyBuilderProcess = AssemblyBuilderProcessesUP;
                    break;

                case "down":
                    backtests = AssemblyBuilder.ChildNodesDOWN.Select(node => node.BacktestIS).ToList();
                    assemblyBuilderProcess = AssemblyBuilderProcessesDOWN;
                    break;

                default:
                    return;
            }

            var backtestOperations = backtests
            .SelectMany(backtest => backtest.BacktestOperations
            .OrderBy(backtestOperation => backtestOperation.Date)).ToList();

            var dates = new List<DateTime>();
            foreach (var backtestOperation in backtestOperations)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Add the date if it has not been already added
                if (dates.Find(date => date == backtestOperation.Date) == default)
                {
                    dates.Add(backtestOperation.Date);
                }
            }
            var orderedDates = dates.OrderBy(date => date.Date);

            var firstOperation = orderedDates.FirstOrDefault();
            var lastOperation = orderedDates.LastOrDefault();

            Parallel.ForEach(
                assemblyBuilderProcess,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                process =>
                {
                    process.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Description;

                    // Perfrom extraction
                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        firstOperation,
                        lastOperation,
                        indicators,
                        candles.ToList(),
                        ProjectConfiguration.TimeframeId);

                    // Filter the extraction for only the candles with backtest operations
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
                            _manualResetEventSlim.Wait();
                            _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                            outputExtraction.Add(extraction.Output[idx]);
                        }

                        extraction.Output = outputExtraction.ToArray();
                    }

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                    var nameSignature = process.ExtractionTemplateName.Replace(".csv", string.Empty);

                    _extractorService.ExtractorWrite(
                        ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv"),
                        extractionResult,
                        0,
                        0);

                    process.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                    process.ExtractionPath = ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv");
                    process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Description;
                });
        }

        private List<AssemblyNodeModel> GetAssemblyNodeBacktests(IEnumerable<BuilderProcess> assemblyBuilderProcess, string label)
        {
            var backtestNodes = new List<AssemblyNodeModel>();

            foreach (var process in assemblyBuilderProcess)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Weka 

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

                process.Tree = responseWeka[0];

                // UP   ->  WINNER
                // DOWN ->  LOSER
                var nodes = process.Tree.NodeOutput.Where(node => node.Winner && node.Label.ToLowerInvariant() == "up")
                    .Select(node =>
                    {
                        node.Node = node.Node.OrderByDescending(node => node).ToList();
                        node.Label = label.ToUpperInvariant();
                        return new AssemblyNodeModel
                        {
                            ParentNodeData = node,
                            ChildNodesData = (label.ToLowerInvariant() == "up"
                            ? AssemblyBuilder.ChildNodesUP
                            : AssemblyBuilder.ChildNodesDOWN)
                            .Select(node => node.NodeData).ToList()
                        };
                    }).ToList();

                backtestNodes.AddRange(nodes);
                process.BacktestAssemblyNodes.Clear();
                process.BacktestAssemblyNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Description;
            }

            return backtestNodes;
        }

        private void BacktestProcess(List<AssemblyNodeModel> backtestNodes, IEnumerable<Candle> projectCandles)
        {
            var backtestNodesPartition = Partitioner.Create(backtestNodes, EnumerablePartitionerOptions.NoBuffering);

            Parallel.ForEach(
                backtestNodesPartition,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                bactestingAssemblyNode =>
                {
                    _manualResetEventSlim.Wait();
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    BuilderProcess builderProcess;
                    double meanSuccessRatePercentIS;

                    switch (bactestingAssemblyNode.ParentNodeData.Label.ToLowerInvariant())
                    {
                        case "up":
                            builderProcess = AssemblyBuilderProcessesUP
                            .Where(process => process.BacktestAssemblyNodes
                            .Any(processNode => processNode == bactestingAssemblyNode)).FirstOrDefault();
                            meanSuccessRatePercentIS = MeanSuccessRatePercentUP;
                            break;

                        case "down":
                            builderProcess = AssemblyBuilderProcessesDOWN
                            .Where(process => process.BacktestAssemblyNodes
                            .Any(processNode => processNode == bactestingAssemblyNode)).FirstOrDefault();
                            meanSuccessRatePercentIS = MeanSuccessRatePercentDOWN;
                            break;

                        default:
                            return;
                    }

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests++;
                        builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {builderProcess.ExecutingBacktests} Nodes";
                    }

                    var winningNode = _strategyBuilderService.BuildBacktestOfAssemblyNode(
                        bactestingAssemblyNode,
                        projectCandles,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        meanSuccessRatePercentIS,
                        _manualResetEventSlim,
                        _cancellationTokenSource.Token);

                    if (winningNode)
                    {
                        SerializerHelper.SerializeAssemblyNode(ProcessArgs.ProjectName, bactestingAssemblyNode);
                    }

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests--;
                        builderProcess.CompletedBacktests++;
                        builderProcess.ProgressCounter++;

                        builderProcess.Message = builderProcess.CompletedBacktests == builderProcess.BacktestAssemblyNodes.Count
                        ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Description
                        : builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {builderProcess.ExecutingBacktests} Nodes";
                    }
                });
        }

        private void ResetBuilder()
        {
            AssemblyBuilderProcessesUP.Clear();
            AssemblyBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
            foreach (var file in extractionTemplates)
            {
                AssemblyBuilderProcessesUP.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.ABNotStarted.GetMetadata().Description,
                    Tree = new(),
                    BacktestAssemblyNodes = new()
                });
                AssemblyBuilderProcessesDOWN.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.ABNotStarted.GetMetadata().Description,
                    Tree = new(),
                    BacktestAssemblyNodes = new(),
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

        private AssemblyBuilderModel _assembledBuilder;
        public AssemblyBuilderModel AssemblyBuilder
        {
            get => _assembledBuilder;
            set
            {
                if (SetProperty(ref _assembledBuilder, value))
                {
                    MeanSuccessRatePercentUP = _assembledBuilder.ChildNodesUP
                        .Select(node => node.BacktestIS.SuccessRatePercent)
                        .Sum() / _assembledBuilder.ChildNodesUP.Count;

                    MeanSuccessRatePercentDOWN = _assembledBuilder.ChildNodesDOWN
                        .Select(node => node.BacktestIS.SuccessRatePercent)
                        .Sum() / _assembledBuilder.ChildNodesDOWN.Count;
                }
            }
        }

        private double _meanSuccessRatePercentUP;
        public double MeanSuccessRatePercentUP
        {
            get => _meanSuccessRatePercentUP;
            set => SetProperty(ref _meanSuccessRatePercentUP, value);
        }

        private double _meanSuccessRatePercentDOWN;
        public double MeanSuccessRatePercentDOWN
        {
            get => _meanSuccessRatePercentDOWN;
            set => SetProperty(ref _meanSuccessRatePercentDOWN, value);
        }

        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessesUP { get; set; }
        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessesDOWN { get; set; }

        // IDisposable Implementation

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
