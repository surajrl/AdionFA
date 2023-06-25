using AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
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
using AdionFA.UI.Station.Project.Validators.AssemblyBuilder;
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
        private readonly IAssemblyBuilderService _assembledBuilderService;
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
            _assembledBuilderService = IoC.Get<IAssemblyBuilderService>();

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
                    AssemblyBuilder = _assembledBuilderService.LoadAssemblyBuilder(ProcessArgs.ProjectName);
                }
                else
                {
                    // New process starting ...
                    AssemblyBuilder.AssemblyNodesUP.Clear();
                    AssemblyBuilder.AssemblyNodesDOWN.Clear();
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

            AssemblyBuilderProcessUP = new();
            AssemblyBuilderProcessDOWN = new();
            MaxParallelism = Environment.ProcessorCount - 1;
            AssemblyBuilder = _assembledBuilderService.LoadAssemblyBuilder(ProcessArgs.ProjectName);
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.AssemblyBuilder.Replace(" ", string.Empty))
            {
                try
                {
                    ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);

                    if (!IsTransactionActive
                        && !AssemblyBuilderProcessDOWN.Any(process => process.Message == StrategyBuilderStatus.ABCompleted.GetMetadata().Description)
                        && !AssemblyBuilderProcessUP.Any(process => process.Message == StrategyBuilderStatus.ABCompleted.GetMetadata().Description))
                    {
                        AssemblyBuilderProcessUP.Clear();
                        AssemblyBuilderProcessDOWN.Clear();

                        var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                        foreach (var file in extractionTemplates)
                        {
                            AssemblyBuilderProcessUP.Add(new BuilderProcess
                            {
                                ExtractionTemplatePath = file.FullName,
                                ExtractionTemplateName = file.Name,
                                ExtractionName = file.Name,
                                Message = StrategyBuilderStatus.ABNotStarted.GetMetadata().Description,
                                Tree = new(),
                                BacktestNodes = new(),
                            });

                            AssemblyBuilderProcessDOWN.Add(new BuilderProcess
                            {
                                ExtractionTemplatePath = file.FullName,
                                ExtractionTemplateName = file.Name,
                                ExtractionName = file.Name,
                                Message = StrategyBuilderStatus.ABNotStarted.GetMetadata().Description,
                                Tree = new(),
                                BacktestNodes = new(),
                            });
                        }
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

            foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessUP.Zip(AssemblyBuilderProcessDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                if (processUP.Message != StrategyBuilderStatus.ABCompleted.GetMetadata().Description
                    && processUP.Message != StrategyBuilderStatus.ABNotStarted.GetMetadata().Description)
                {
                    var stopped = StrategyBuilderStatus.Stopped.GetMetadata().Description;
                    processUP.Message = $"{processUP.Message} - {stopped}";
                }

                if (processDOWN.Message != StrategyBuilderStatus.ABCompleted.GetMetadata().Description
                    && processDOWN.Message != StrategyBuilderStatus.ABNotStarted.GetMetadata().Description)
                {
                    var stopped = StrategyBuilderStatus.Stopped.GetMetadata().Description;
                    processDOWN.Message = $"{processDOWN.Message} - {stopped}";
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

            var stopped = StrategyBuilderStatus.Stopped.GetMetadata().Description;
            foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessUP.Zip(AssemblyBuilderProcessDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                processUP.Message = $"{processUP.Message} - {stopped}";
                processDOWN.Message = $"{processDOWN.Message} - {stopped}";
            }

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand Process => new DelegateCommand(async () =>
        {
            try
            {
                var validator = Validate(new AssemblyBuilderValidator());
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                       EntityTypeEnum.AssemblyBuilder.GetMetadata().Description,
                       validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                // Delete Assembly Builder Extractions
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("up"), isBackup: false);
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("down"), isBackup: false);
                // Delete Assembly Builder Nodes
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml", isBackup: false);
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml", isBackup: false);
                AssemblyBuilder.AssemblyNodesUP.Clear();
                AssemblyBuilder.AssemblyNodesDOWN.Clear();

                IsTransactionActive = true;

                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

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

                        var backtestNodes = new List<REPTreeNodeModel>
                        {
                            GetBacktestNodes(AssemblyBuilderProcessUP, "up"),
                            GetBacktestNodes(AssemblyBuilderProcessDOWN, "down")
                        };

                        BacktestProcess(backtestNodes, allProjectCandles);
                    }
                    else if (AssemblyBuilder.ChildNodesUP.Count > 0)
                    {
                        ExtractionProcess("up", allProjectCandles);
                        BacktestProcess(GetBacktestNodes(AssemblyBuilderProcessUP, "up"), allProjectCandles);
                    }
                    else if (AssemblyBuilder.ChildNodesDOWN.Count > 0)
                    {
                        ExtractionProcess("down", allProjectCandles);
                        BacktestProcess(GetBacktestNodes(AssemblyBuilderProcessDOWN, "down"), allProjectCandles);
                    }
                    else
                    {
                        return;
                    }

                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessUP.Zip(AssemblyBuilderProcessDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    processUP.Message = StrategyBuilderStatus.ExecutingCorrelation.GetMetadata().Description;
                    processDOWN.Message = StrategyBuilderStatus.ExecutingCorrelation.GetMetadata().Description;
                }

                await Task.Run(() =>
                {
                    _assembledBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        AssemblyBuilder,
                        ProjectConfiguration.SBMaxCorrelationPercent);
                });

                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessUP.Zip(AssemblyBuilderProcessDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    processUP.Message = StrategyBuilderStatus.ABCompleted.GetMetadata().Description;
                    processDOWN.Message = StrategyBuilderStatus.ABCompleted.GetMetadata().Description;
                }

                // Result Message

                _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Publish(true);

                var msgUP = AssemblyBuilder.AssemblyNodesUP.Count > 0 ? $"{AssemblyBuilder.AssemblyNodesUP.Count} UP Assembly Nodes Found." : "No UP Assembly Nodes Found.";
                var msgDOWN = AssemblyBuilder.AssemblyNodesDOWN.Count > 0 ? $"{AssemblyBuilder.AssemblyNodesDOWN.Count} DOWN Assembly Nodes Found." : "No DOWN Assembly Nodes Found.";

                MessageHelper.ShowMessage(this,
                    CommonResources.AssemblyBuilder,
                    $"{MessageResources.AssemblyBuilderCompleted}.\n\n" +
                    $"{msgUP}\n" +
                    $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessUP.Zip(AssemblyBuilderProcessDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    var canceled = StrategyBuilderStatus.Canceled.GetMetadata().Description;
                    var stopped = StrategyBuilderStatus.Stopped.GetMetadata().Description;

                    processUP.Message = processUP.Message.Replace(stopped, canceled);
                    processDOWN.Message = processUP.Message.Replace(stopped, canceled);
                }

                CanCancelOrContinue = false;
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
        private void ExtractionProcess(string label, IEnumerable<Candle> projectCandles)
        {
            IList<BacktestModel> backtests;
            IList<BuilderProcess> assembledBuilderProcess;

            switch (label.ToLowerInvariant())
            {
                case "up":
                    backtests = AssemblyBuilder.ChildNodesUP.Select(node => node.BacktestIS).ToList();
                    assembledBuilderProcess = AssemblyBuilderProcessUP;
                    break;

                case "down":
                    backtests = AssemblyBuilder.ChildNodesDOWN.Select(node => node.BacktestIS).ToList();
                    assembledBuilderProcess = AssemblyBuilderProcessDOWN;
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
                assembledBuilderProcess,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                process =>
                {
                    process.Message = StrategyBuilderStatus.ExecutingExtraction.GetMetadata().Description;

                    // Perfrom extraction
                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        firstOperation,
                        lastOperation,
                        indicators,
                        projectCandles.ToList(),
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
                    process.Message = StrategyBuilderStatus.ExtractionCompleted.GetMetadata().Description;
                });
        }

        private List<REPTreeNodeModel> GetBacktestNodes(IEnumerable<BuilderProcess> assembledBuilderProcess, string label)
        {
            var backtestNodes = new List<REPTreeNodeModel>();

            foreach (var process in assembledBuilderProcess)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Weka 

                process.Message = StrategyBuilderStatus.ExecutingWeka.GetMetadata().Description;

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
                        return node;
                    }).ToList();

                backtestNodes.AddRange(nodes);
                process.BacktestNodes.Clear();
                process.BacktestNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = StrategyBuilderStatus.WekaCompleted.GetMetadata().Description;
            }

            return backtestNodes;
        }

        private void BacktestProcess(List<REPTreeNodeModel> backtestNodes, IEnumerable<Candle> projectCandles)
        {
            var backtestNodesPartition = Partitioner.Create(backtestNodes, EnumerablePartitionerOptions.NoBuffering);

            Parallel.ForEach(
                backtestNodesPartition,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                node =>
                {
                    _manualResetEventSlim.Wait();
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    var childNodes = node.Label.ToLowerInvariant() == "up"
                    ? AssemblyBuilder.ChildNodesUP
                    : AssemblyBuilder.ChildNodesDOWN;

                    var process = node.Label.ToLowerInvariant() == "up"
                    ? AssemblyBuilderProcessUP.Where(process => process.BacktestNodes.Any(processNode => processNode == node)).FirstOrDefault()
                    : AssemblyBuilderProcessDOWN.Where(process => process.BacktestNodes.Any(processNode => processNode == node)).FirstOrDefault();

                    lock (_lock)
                    {
                        process.ExecutingBacktests++;
                        process.Message = $"{StrategyBuilderStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                    }

                    _assembledBuilderService.BuildBacktestOfNode(
                        ProcessArgs.ProjectName,
                        AssemblyBuilder,
                        node,
                        childNodes,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        projectCandles,
                        _manualResetEventSlim,
                        _cancellationTokenSource.Token);

                    lock (_lock)
                    {
                        process.ExecutingBacktests--;
                        process.CompletedBacktests++;
                        process.ProgressCounter++;

                        if (process.CompletedBacktests == process.BacktestNodes.Count)
                        {
                            process.Message = StrategyBuilderStatus.BacktestCompleted.GetMetadata().Description;
                        }
                        else
                        {
                            process.Message = $"{StrategyBuilderStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                        }
                    }
                });
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

        private bool _canCancelOrContinue;
        public bool CanCancelOrContinue
        {
            get => _canCancelOrContinue;
            set => SetProperty(ref _canCancelOrContinue, value);
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

        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessUP { get; set; }
        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessDOWN { get; set; }

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
