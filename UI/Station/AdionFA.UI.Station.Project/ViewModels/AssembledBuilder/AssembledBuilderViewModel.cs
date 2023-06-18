using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
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
using AdionFA.UI.Station.Project.Model.AssembledBuilder;
using AdionFA.UI.Station.Project.Validators.AssembledBuilder;
using AutoMapper;
using DynamicData;
using MahApps.Metro.Controls.Dialogs;
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
    public class AssembledBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IAssembledBuilderService _assembledBuilderService;
        private readonly IExtractorService _extractorService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _historicalDataService;
        private readonly IEventAggregator _eventAggregator;

        private readonly ManualResetEventSlim _manualResetEventSlim;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock;
        private bool _disposedValue;

        public AssembledBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
            _assembledBuilderService = IoC.Get<IAssembledBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            AssembledBuilder = new();
            AssembledBuilderProcessUP = new();
            AssembledBuilderProcessDOWN = new();
            MaxParallelism = Environment.ProcessorCount - 1;

            _cancellationTokenSource = new();
            _manualResetEventSlim = new(true);
            _lock = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.AssembledBuilder.Replace(" ", string.Empty))
            {
                PopulateViewModel();
            }
        });

        public ICommand StopCommand => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            StopProcesses(AssembledBuilderProcessUP);
            StopProcesses(AssembledBuilderProcessDOWN);

            static void StopProcesses(IEnumerable<AssembledBuilderProcessModel> processes)
            {
                foreach (var process in processes)
                {
                    if (process.Message != AssembledBuilderStatus.BacktestCompleted.GetMetadata().Description
                    && process.Message != AssembledBuilderStatus.NotStarted.GetMetadata().Description)
                    {
                        var stopped = AssembledBuilderStatus.Stopped.GetMetadata().Description;
                        process.Message = $"{process.Message} - {stopped}";
                    }
                }
            }

            CanCancelOrContinue = true;
        }, () => IsTransactionActive && !CanCancelOrContinue)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanCancelOrContinue);

        public ICommand CancelCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
            _manualResetEventSlim.Set();

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand ContinueCommand => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Set();

            ContinueProcesses(AssembledBuilderProcessUP);
            ContinueProcesses(AssembledBuilderProcessDOWN);

            static void ContinueProcesses(IEnumerable<AssembledBuilderProcessModel> processes)
            {
                var stopped = AssembledBuilderStatus.Stopped.GetMetadata().Description;
                foreach (var process in processes)
                {
                    process.Message = process.Message.Replace($" - {stopped}", string.Empty);
                }
            }

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (!Validate(new AssembledBuilderValidator()).IsValid)
                {
                    return;
                }

                var deletePrevious = await MessageHelper.ShowMessageInput(this,
                    "Assembled Builder",
                    "Do you want to delete all the previous extractions and assembled nodes?").ConfigureAwait(true);

                if (deletePrevious == MessageDialogResult.Affirmative)
                {
                    // Delete assembled builder extractions
                    var extractionDirectoryUP = ProcessArgs.ProjectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory("up");
                    var extractionDirectoryDOWN = ProcessArgs.ProjectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory("down");
                    _projectDirectoryService.DeleteAllFiles(extractionDirectoryUP, isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(extractionDirectoryDOWN, isBackup: false);

                    // Delete assembled nodes
                    var nodesDirectoryUP = ProcessArgs.ProjectName.ProjectAssembledBuilderNodesUPDirectory();
                    var nodesDirectoryDOWN = ProcessArgs.ProjectName.ProjectAssembledBuilderNodesDOWNDirectory();
                    _projectDirectoryService.DeleteAllFiles(nodesDirectoryUP, ext: "*.xml", isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(nodesDirectoryDOWN, ext: "*.xml", isBackup: false);
                }
                else
                {
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                Refresh();

                // Historical Data

                var projectHistoricalData = await _historicalDataService.GetHistoricalData(ProjectConfiguration.HistoricalDataId.Value, true);
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
                    // Extraction
                    var backtestNodes = new List<REPTreeNodeModel>();

                    if (AssembledBuilder.ChildNodesUP.Count > 0 && AssembledBuilder.ChildNodesDOWN.Count > 0)
                    {
                        ExtractionProcess("up", allProjectCandles);
                        ExtractionProcess("down", allProjectCandles);

                        backtestNodes.Add(GetBacktestNodes(AssembledBuilderProcessUP, "up"));
                        backtestNodes.Add(GetBacktestNodes(AssembledBuilderProcessDOWN, "down"));
                    }
                    else if (AssembledBuilder.ChildNodesUP.Count > 0)
                    {
                        ExtractionProcess("up", allProjectCandles);
                        backtestNodes.Add(GetBacktestNodes(AssembledBuilderProcessUP, "up"));
                    }
                    else if (AssembledBuilder.ChildNodesDOWN.Count > 0)
                    {
                        ExtractionProcess("down", allProjectCandles);
                        backtestNodes.Add(GetBacktestNodes(AssembledBuilderProcessDOWN, "down"));
                    }
                    else
                    {
                        return;
                    }

                    BacktestProcess(backtestNodes, allProjectCandles);
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                // Correlation ...

                // ASSEMBLED BUILDER NOT BEING ADDED THE WINNING ASSMEBLED NODES IN ASSMEBLED BUILDER SERVICE
                IsTransactionActive = false;

                // Result Message

                var msgUP = AssembledBuilder.AssembledNodesUP.Count > 0 ? $"{AssembledBuilder.AssembledNodesUP.Count} UP nodes found." : "No UP nodes found.";
                var msgDOWN = AssembledBuilder.AssembledNodesDOWN.Count > 0 ? $"{AssembledBuilder.AssembledNodesDOWN.Count} DOWN nodes found." : "No DOWN nodes found.";

                MessageHelper.ShowMessage(this,
                    CommonResources.AssembledBuilder,
                    $"{MessageResources.AssembledBuilderCompleted}.\n\n" +
                    $"{msgUP}\n" +
                    $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                CancelProcesses(AssembledBuilderProcessUP);
                CancelProcesses(AssembledBuilderProcessDOWN);

                static void CancelProcesses(IEnumerable<AssembledBuilderProcessModel> processes)
                {
                    foreach (var process in processes)
                    {
                        var canceled = AssembledBuilderStatus.Canceled.GetMetadata().Description;
                        var stopped = AssembledBuilderStatus.Stopped.GetMetadata().Description;
                        process.Message = process.Message.Replace(stopped, canceled);
                    }
                }

                CanCancelOrContinue = false;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
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
            IList<AssembledBuilderProcessModel> assembledBuilderProcess;

            switch (label.ToLowerInvariant())
            {
                case "up":
                    backtests = AssembledBuilder.ChildNodesUP.Select(node => node.BacktestIS).ToList();
                    assembledBuilderProcess = AssembledBuilderProcessUP;
                    break;

                case "down":
                    backtests = AssembledBuilder.ChildNodesDOWN.Select(node => node.BacktestIS).ToList();
                    assembledBuilderProcess = AssembledBuilderProcessDOWN;
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
                    // Update assembled builder process
                    process.Message = AssembledBuilderStatus.ExecutingExtraction.GetMetadata().Description;

                    // Perfrom extraction
                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        firstOperation,
                        lastOperation,
                        indicators,
                        projectCandles.ToList(),
                        ProjectConfiguration.TimeframeId,
                        ProjectConfiguration.ExtractorMinVariation);

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
                        ProcessArgs.ProjectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv"),
                        extractionResult,
                        0,
                        0);

                    // Update assembled builder process
                    process.ExtractionAssembledBuilderName = $"{nameSignature}.{timeSignature}.csv";
                    process.ExtractionAssembledBuilderPath = ProcessArgs.ProjectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv");
                    process.Message = AssembledBuilderStatus.ExtractionCompleted.GetMetadata().Description;
                });
        }

        private List<REPTreeNodeModel> GetBacktestNodes(IEnumerable<AssembledBuilderProcessModel> assembledBuilderProcess, string label)
        {
            var allBacktestNodes = new List<REPTreeNodeModel>();

            foreach (var process in assembledBuilderProcess)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Weka 

                process.Message = AssembledBuilderStatus.ExecutingWeka.GetMetadata().Description;

                var wekaApi = new WekaApiClient();
                var responseWeka = wekaApi.GetREPTreeClassifier(
                        process.ExtractionAssembledBuilderPath,
                        ProjectConfiguration.DepthWeka,
                        ProjectConfiguration.TotalDecimalWeka,
                        ProjectConfiguration.MinimalSeed,
                        ProjectConfiguration.MaximumSeed,
                        ProjectConfiguration.TotalInstanceWeka,
                        (double)ProjectConfiguration.ABWekaMaxRatioTree,
                        (double)ProjectConfiguration.ABWekaNTotalTree,
                        true);

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

                allBacktestNodes.AddRange(nodes);
                process.BacktestNodes.Clear();
                process.BacktestNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = AssembledBuilderStatus.WekaCompleted.GetMetadata().Description;
            }

            return allBacktestNodes;
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
                    ? AssembledBuilder.ChildNodesUP
                    : AssembledBuilder.ChildNodesDOWN;

                    var process = node.Label.ToLowerInvariant() == "up"
                    ? AssembledBuilderProcessUP.Where(process => process.BacktestNodes.Any(processNode => processNode == node)).FirstOrDefault()
                    : AssembledBuilderProcessDOWN.Where(process => process.BacktestNodes.Any(processNode => processNode == node)).FirstOrDefault();

                    lock (_lock)
                    {
                        process.ExecutingBacktests++;
                        process.Message = $"{AssembledBuilderStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                    }

                    _assembledBuilderService.BuildBacktestOfNode(
                        ProcessArgs.ProjectName,
                        AssembledBuilder,
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
                            process.Message = AssembledBuilderStatus.BacktestCompleted.GetMetadata().Description;
                        }
                        else
                        {
                            process.Message = $"{AssembledBuilderStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                        }
                    }
                });
        }

        public ICommand RefreshCommand => new DelegateCommand(() =>
        {
            try
            {
                Refresh();
                PopulateViewModel();
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            try
            {
                var project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);
                ProjectConfiguration = project?.ProjectConfigurations.FirstOrDefault();

                AssembledBuilder = _assembledBuilderService.LoadAssembledBuilder(ProcessArgs.ProjectName);

                if (!IsTransactionActive)
                {
                    var extractionTemplatesDirectory = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                    var extractionTemplates = _projectDirectoryService.GetFilesInPath(extractionTemplatesDirectory);

                    if (!AssembledBuilderProcessUP.Any())
                    {
                        foreach (var file in extractionTemplates)
                        {
                            AssembledBuilderProcessUP.Add(new AssembledBuilderProcessModel
                            {
                                ExtractionTemplatePath = file.FullName,
                                ExtractionTemplateName = file.Name,
                                ExtractionAssembledBuilderName = file.Name,
                                Message = AssembledBuilderStatus.NotStarted.GetMetadata().Description,
                                Tree = new(),
                                BacktestNodes = new(),
                            });
                        }
                    }

                    if (!AssembledBuilderProcessDOWN.Any())
                    {
                        foreach (var file in extractionTemplates)
                        {
                            AssembledBuilderProcessDOWN.Add(new AssembledBuilderProcessModel
                            {
                                ExtractionTemplatePath = file.FullName,
                                ExtractionTemplateName = file.Name,
                                ExtractionAssembledBuilderName = file.Name,
                                Message = AssembledBuilderStatus.NotStarted.GetMetadata().Description,
                                Tree = new(),
                                BacktestNodes = new(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
        }

        private void Refresh()
        {
            AssembledBuilder = _assembledBuilderService.LoadAssembledBuilder(ProcessArgs.ProjectName);

            AssembledBuilderProcessDOWN.Clear();
            AssembledBuilderProcessUP.Clear();

            var extractionTemplatesDirectory = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
            var extractionTemplates = _projectDirectoryService.GetFilesInPath(extractionTemplatesDirectory);

            foreach (var file in extractionTemplates)
            {
                AssembledBuilderProcessUP.Add(new AssembledBuilderProcessModel
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionAssembledBuilderName = file.Name,
                    Message = AssembledBuilderStatus.NotStarted.GetMetadata().Description,
                    BacktestNodes = new(),
                    Tree = new(),
                });

                AssembledBuilderProcessDOWN.Add(new AssembledBuilderProcessModel
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionAssembledBuilderName = file.Name,
                    Message = AssembledBuilderStatus.NotStarted.GetMetadata().Description,
                    BacktestNodes = new(),
                    Tree = new(),
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

        private AssembledBuilderModel _assembledBuilder;
        public AssembledBuilderModel AssembledBuilder
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

        public ObservableCollection<AssembledBuilderProcessModel> AssembledBuilderProcessUP { get; set; }
        public ObservableCollection<AssembledBuilderProcessModel> AssembledBuilderProcessDOWN { get; set; }

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
        // ~AssembledBuilderViewModel()
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
