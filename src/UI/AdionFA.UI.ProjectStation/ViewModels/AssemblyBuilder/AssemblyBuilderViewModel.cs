using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.AssemblyBuilder.Model;
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
    public class AssemblyBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IAssemblyBuilderService _assemblyBuilderService;
        private readonly INodeBuilderService _nodeBuilderService;
        private readonly IExtractorService _extractorService;

        private readonly IProjectService _projectService;
        private readonly IMarketDataService _marketDataService;
        private readonly IEventAggregator _eventAggregator;

        private readonly ManualResetEventSlim _manualResetEventSlim;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock;
        private bool _disposedValue;

        public AssemblyBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectService = IoC.Kernel.Get<IProjectService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _assemblyBuilderService = IoC.Kernel.Get<IAssemblyBuilderService>();
            _nodeBuilderService = IoC.Kernel.Get<INodeBuilderService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            // Event Aggregators

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(canExecute => CanExecute = canExecute);

            _eventAggregator.GetEvent<ExtractorTemplatesUpdatedEvent>().Subscribe(updated =>
            {
                var canUpdateProcessesUP =
                AssemblyBuilderProcessesDOWN.All(process => process.Message == BuilderProcessStatus.ABNotStarted.GetMetadata().Name)
                || AssemblyBuilderProcessesDOWN.All(process => process.Message.Contains(BuilderProcessStatus.Canceled.GetMetadata().Name));

                var canUpdateProcessesDOWN =
                AssemblyBuilderProcessesUP.All(process => process.Message == BuilderProcessStatus.ABNotStarted.GetMetadata().Name)
                || AssemblyBuilderProcessesUP.All(process => process.Message.Contains(BuilderProcessStatus.Canceled.GetMetadata().Name));

                // Only update the extractor templates if the builder process has not started or has been cancelled
                if (updated && canUpdateProcessesUP && canUpdateProcessesDOWN)
                {
                    UpdateExtractorTemplates();
                }
            });

            _eventAggregator.GetEvent<BuilderResetEvent>().Subscribe(reset =>
            {
                if (reset)
                {
                    DeleteAssemblyBuilder();
                    UpdateExtractorTemplates();
                }
            });

            _eventAggregator.GetEvent<NodeBuilderCompletedEvent>().Subscribe(nodeBuilderCompleted =>
            {
                if (nodeBuilderCompleted)
                {
                    DeleteAssemblyBuilder();
                    AssemblyBuilder = _assemblyBuilderService.CreateNewAssemblyBuilder(ProcessArgs.ProjectName);
                    UpdateExtractorTemplates();
                }
            });

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            AssemblyBuilderProcessesUP = new();
            AssemblyBuilderProcessesDOWN = new();
            AssemblyBuilder = new();

            MaxParallelism = Environment.ProcessorCount - 1;

            _cancellationTokenSource = new();
            _manualResetEventSlim = new(true);
            _lock = new();

            AssemblyBuilder = _assemblyBuilderService.GetExistingAssemblyBuilder(ProcessArgs.ProjectName);
            if (AssemblyBuilder.WinningAssemblyNodesUP.Count == 0 && AssemblyBuilder.WinningAssemblyNodesDOWN.Count == 0)
            {
                AssemblyBuilder = _assemblyBuilderService.CreateNewAssemblyBuilder(ProcessArgs.ProjectName);
            }

            UpdateExtractorTemplates();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.AssemblyBuilderTrim)
            {
                ProjectConfiguration = _mapper.Map<ProjectConfigurationDTO, ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                if (processUP.Message != BuilderProcessStatus.ABCompleted.GetMetadata().Name
                    && processUP.Message != BuilderProcessStatus.ABNotStarted.GetMetadata().Name)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Name;
                    processUP.Message = $"{processUP.Message} - {stopped}";
                }

                if (processDOWN.Message != BuilderProcessStatus.ABCompleted.GetMetadata().Name
                    && processDOWN.Message != BuilderProcessStatus.ABNotStarted.GetMetadata().Name)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Name;
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

            var stopped = BuilderProcessStatus.Stopped.GetMetadata().Name;
            foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
            {
                processUP.Message = $"{processUP.Message} - {stopped}";
                processDOWN.Message = $"{processDOWN.Message} - {stopped}";
            }

            IsStopped = false;
        }, () => IsStopped).ObservesProperty(() => IsStopped);

        public ICommand Process => new DelegateCommand(async () =>
        {
            var validator = Validate(new AssemblyBuilderValidator());
            if (!validator.IsValid)
            {
                MessageHelper.ShowMessages(this,
                    EntityTypeEnum.AssemblyBuilder.GetMetadata().Name,
                    validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            var deleteAll = await MessageHelper.ShowMessageInputAsync(this,
                    Resources.AssemblyBuilder,
                    "Starting a new process will delete all the Assembly Nodes and Strategy Nodes.\n"
                    + "Do you want to continue?").ConfigureAwait(true);

            if (deleteAll != MessageDialogResult.Affirmative)
            {
                return;
            }

            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                DeleteAssemblyBuilder();
                AssemblyBuilder = _assemblyBuilderService.CreateNewAssemblyBuilder(ProcessArgs.ProjectName);
                UpdateExtractorTemplates();

                _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Publish(false);

                // Historical Data

                var projectHistoricalData = _marketDataService.GetHistoricalData(ProcessArgs.HistoricalDataId, true);
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

                        var backtestNodes = new List<AssemblyNodeModel>();
                        backtestNodes.AddRange(GetAssemblyNodeBacktests(AssemblyBuilderProcessesUP, "up"));
                        backtestNodes.AddRange(GetAssemblyNodeBacktests(AssemblyBuilderProcessesDOWN, "down"));

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
                    processUP.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                    processDOWN.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                }

                await Task.Run(() =>
                {
                    _nodeBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        AssemblyBuilder,
                        ProjectConfiguration.SBMaxCorrelationPercent);
                });

                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    processUP.Message = BuilderProcessStatus.ABCompleted.GetMetadata().Name;
                    processDOWN.Message = BuilderProcessStatus.ABCompleted.GetMetadata().Name;
                }

                if (MultiAssemblyMode)
                {
                    // Pass the group of UP and group of DOWN nodes to the Crossing Builder.
                }
                else
                {
                    // Pass the individual UP and individual DOWN nodes to the Crossing Builder.
                }

                // Result Message

                _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Publish(true);

                var msgUP = AssemblyBuilder.WinningAssemblyNodesUP.Count > 0
                ? $"{AssemblyBuilder.WinningAssemblyNodesUP.Count} UP Assembly Nodes Found"
                : "No UP Assembly Nodes Found.";

                var msgDOWN = AssemblyBuilder.WinningAssemblyNodesDOWN.Count > 0
                ? $"{AssemblyBuilder.WinningAssemblyNodesDOWN.Count} DOWN Assembly Nodes Found"
                : "No DOWN Assembly Nodes Found.";

                MessageHelper.ShowMessage(this,
                    Resources.AssemblyBuilder,
                    $"{Resources.AssemblyBuilderCompleted}.\n\n" +
                    $"{msgUP}\n" +
                    $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                foreach ((var processUP, var processDOWN) in AssemblyBuilderProcessesUP.Zip(AssemblyBuilderProcessesDOWN, (processUP, processDOWN) => (processUP, processDOWN)))
                {
                    var canceled = BuilderProcessStatus.Canceled.GetMetadata().Name;
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Name;

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
        }, () => !IsTransactionActive && (AssemblyBuilder.ChildNodesUP.Count > 0 || AssemblyBuilder.ChildNodesDOWN.Count > 0))
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => AssemblyBuilder);

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
                .OrderBy(backtestOperation => backtestOperation.Date))
                .ToList();

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
                    process.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Name;

                    // Perfrom extraction
                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        firstOperation,
                        lastOperation,
                        indicators,
                        candles.ToList(),
                        ProcessArgs.TimeframeId);

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
                    process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Name;
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

                process.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Name;

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

                // No tree was found
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
                        return new AssemblyNodeModel
                        {
                            ParentNodeData = node,
                            ChildNodesData = (label.ToLowerInvariant() == "up"
                            ? AssemblyBuilder.ChildNodesUP
                            : AssemblyBuilder.ChildNodesDOWN)
                            .Select(node => node.NodeData).ToList(),
                            BacktestStatusIS = BacktestStatus.NotStarted,
                            BacktestStatusOS = BacktestStatus.NotStarted
                        };
                    }).ToList();

                backtestNodes.AddRange(nodes);
                process.BacktestAssemblyNodes.Clear();
                process.BacktestAssemblyNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = process.BacktestAssemblyNodes.Count == 0
                    ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name // No backtests to do
                    : process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Name;
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
                        builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} Nodes";
                    }

                    var winningNode = _nodeBuilderService.BuildBacktestOfAssemblyNode(
                        bactestingAssemblyNode,
                        projectCandles,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        ProcessArgs.TimeframeId,
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
                        ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name
                        : builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} Nodes";
                    }
                });
        }

        private void UpdateExtractorTemplates()
        {
            AssemblyBuilderProcessesUP.Clear();
            AssemblyBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
            foreach (var file in extractionTemplates)
            {
                if (AssemblyBuilder.ChildNodesUP.Count > 0)
                {
                    AssemblyBuilderProcessesUP.Add(new BuilderProcess
                    {
                        ExtractionTemplatePath = file.FullName,
                        ExtractionTemplateName = file.Name,
                        ExtractionName = file.Name,
                        Message = BuilderProcessStatus.ABNotStarted.GetMetadata().Name,
                        Tree = new(),
                        BacktestAssemblyNodes = new()
                    });
                }

                if (AssemblyBuilder.ChildNodesDOWN.Count > 0)
                {
                    AssemblyBuilderProcessesDOWN.Add(new BuilderProcess
                    {
                        ExtractionTemplatePath = file.FullName,
                        ExtractionTemplateName = file.Name,
                        ExtractionName = file.Name,
                        Message = BuilderProcessStatus.ABNotStarted.GetMetadata().Name,
                        Tree = new(),
                        BacktestAssemblyNodes = new(),
                    });
                }
            }
        }

        private void DeleteAssemblyBuilder()
        {
            // Reset the winning assembly nodes
            AssemblyBuilder.WinningAssemblyNodesUP.Clear();
            AssemblyBuilder.WinningAssemblyNodesDOWN.Clear();

            // Reset the child nodes
            AssemblyBuilder.ChildNodesUP.Clear();
            AssemblyBuilder.ChildNodesDOWN.Clear();

            // Delete node files from the Assembly Builder
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml", isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml", isBackup: false);

            // Delete extractor files from the Assembly Builder
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("up"), isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("down"), isBackup: false);
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

        private AssemblyBuilderModel _assemblyBuilder;
        public AssemblyBuilderModel AssemblyBuilder
        {
            get => _assemblyBuilder;
            set
            {
                if (SetProperty(ref _assemblyBuilder, value))
                {
                    MeanSuccessRatePercentUP = _assemblyBuilder.ChildNodesUP
                        .Select(node => node.BacktestIS.SuccessRatePercent)
                        .Sum() / _assemblyBuilder.ChildNodesUP.Count;

                    MeanSuccessRatePercentDOWN = _assemblyBuilder.ChildNodesDOWN
                        .Select(node => node.BacktestIS.SuccessRatePercent)
                        .Sum() / _assemblyBuilder.ChildNodesDOWN.Count;
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

        private bool _multiAssemblyMode;
        public bool MultiAssemblyMode
        {
            get => _multiAssemblyMode;
            set => SetProperty(ref _multiAssemblyMode, value);
        }

        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessesUP { get; set; }
        public ObservableCollection<BuilderProcess> AssemblyBuilderProcessesDOWN { get; set; }

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
