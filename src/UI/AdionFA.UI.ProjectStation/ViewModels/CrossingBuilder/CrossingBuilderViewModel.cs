using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Model;
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
    public class CrossingBuilderViewModel : MenuItemViewModel, IDisposable
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

        public CrossingBuilderViewModel(MainViewModel mainViewModel)
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

            CrossingBuilder = new();
            CrossingBuilderProcessesUP = new();
            CrossingBuilderProcessesDOWN = new();

            CrossingHistoricalData = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.CrossingBuilderTrim)
            {
                // Load most recent project configuration

                ProjectConfiguration = _mapper.Map<ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));

                // Load crossing historical data

                CrossingHistoricalData.Clear();
                CrossingHistoricalData.AddRange(_marketDataService.GetAllHistoricalData(false)
                    .Where(historicalData => historicalData.HistoricalDataId != ProjectConfiguration.Project.HistoricalDataId)
                    .Select(historicalData => new Metadata
                    {
                        Id = historicalData.HistoricalDataId,
                        Name = historicalData.Description
                    }));

                // Load XML files containing winning strategy nodes

                if (!IsTransactionActive)
                {
                    CrossingBuilder.AllWinningNodes.Clear();

                    new DirectoryInfo(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription()))
                    .GetFiles("*", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(file =>
                    {
                        CrossingBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<StrategyNodeModel>(file.FullName));
                    });

                    new DirectoryInfo(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription()))
                    .GetFiles("*", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(file =>
                    {
                        CrossingBuilder.AllWinningNodes.Add(SerializerHelper.XMLDeSerializeObject<StrategyNodeModel>(file.FullName));
                    });
                }
            }
        });

        public ICommand LoadCrossingBuilderCommand => new DelegateCommand(async () =>
        {
            var load = await MessageHelper.ShowMessageInputAsync(this,
                    Resources.CrossingBuilder,
                    "Loading a new Crossing Builder will delete all the existing Strategy Nodes\n"
                    + "Do you want to continue?").ConfigureAwait(true);

            if (load != MessageDialogResult.Affirmative)
            {
                return;
            }

            if (LoadFromNodeBuilder)
            {
                DeleteCrossingBuilder(true);
                FromNodeBuilder();
            }
            else if (LoadFromAssemblyBuilder)
            {
                DeleteCrossingBuilder(true);
                FromAssemblyBuilder();
            }
            else if (LoadFromCrossingBuilder)
            {
                FromCrossingBuilder();
                DeleteCrossingBuilder(true);
            }
            else
            {
                // ...
            }
        }, () => CanLoad && (LoadFromNodeBuilder || LoadFromAssemblyBuilder || LoadFromCrossingBuilder))
            .ObservesProperty(() => CanLoad)
            .ObservesProperty(() => LoadFromNodeBuilder)
            .ObservesProperty(() => LoadFromAssemblyBuilder)
            .ObservesProperty(() => LoadFromCrossingBuilder);

        public ICommand ResetCrossingBuilderCommand => new DelegateCommand(async () =>
        {
            var reset = await MessageHelper.ShowMessageInputAsync(this,
                Resources.CrossingBuilder,
                $"Resetting the {Resources.CrossingBuilder} will delete all Strategy Nodes found")
            .ConfigureAwait(true);

            if (reset != MessageDialogResult.Affirmative)
            {
                return;
            }

            IsCrossingStarted = false;

            CrossingBuilderProcessesUP.Clear();
            CrossingBuilderProcessesDOWN.Clear();

            DeleteCrossingBuilder(true);

        }, () => !IsTransactionActive && IsCrossingStarted)
            .ObservesProperty(() => IsCrossingStarted)
            .ObservesProperty(() => IsTransactionActive);

        public ICommand CancelCommand => new DelegateCommand(() => _cancellationTokenSource.Cancel(), () => IsTransactionActive)
            .ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            var validator = Validate(new CrossingBuilderValidator());
            if (!validator.IsValid)
            {
                MessageHelper.ShowMessagesAsync(this,
                    EntityTypeEnum.CrossingBuilder.GetMetadata().Name,
                    validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

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

                var crossingCandles = _marketDataService.GetHistoricalDataCandles(CrossingHistoricalDataId)
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

                var historicalData = _marketDataService.GetHistoricalData(CrossingHistoricalDataId, false);
                CrossingSymbolName = _marketDataService.GetSymbol(historicalData.SymbolId).Name;

                // Create the UP and DOWN processes from the previous strategy nodes

                if (IsCrossingStarted)
                {
                    if (CrossingBuilder.AllWinningNodes.Count <= 0)
                    {
                        await MessageHelper.ShowMessageAsync(this,
                            Resources.CrossingBuilder,
                            $"No previous Strategy Nodes found")
                         .ConfigureAwait(true);

                        return;
                    }

                    await MessageHelper.ShowMessageAsync(this,
                        Resources.CrossingBuilder,
                        $"Adding {CrossingSymbolName} to previous Strategy Nodes")
                     .ConfigureAwait(true);

                    FromCrossingBuilder();
                    DeleteCrossingBuilder(false);
                }

                IsCrossingStarted = true;

                await Task.Factory.StartNew(() =>
                {
                    ExtractionProcess(Domain.Enums.Label.UP, crossingCandles);
                    ExtractionProcess(Domain.Enums.Label.DOWN, crossingCandles);

                    CurrentWekaDepth = ProjectConfiguration.CrossingBuilderConfiguration.WekaStartDepth;
                    while (true)
                    {
                        // Reset processes

                        foreach ((var builderProcessUP, var builderProcessDOWN) in CrossingBuilderProcessesUP.Zip(CrossingBuilderProcessesDOWN, (builderProcessUP, builderProcessDOWN) => (builderProcessUP, builderProcessDOWN)))
                        {
                            builderProcessUP.CompletedBacktests = 0;
                            builderProcessUP.ExecutingBacktests = 0;

                            builderProcessDOWN.CompletedBacktests = 0;
                            builderProcessDOWN.ExecutingBacktests = 0;
                        }

                        // Backtest

                        var allBacktestStrategyNodes = new List<StrategyNodeModel>();
                        allBacktestStrategyNodes.AddRange(FindBacktestStrategyNodes(Domain.Enums.Label.UP));
                        allBacktestStrategyNodes.AddRange(FindBacktestStrategyNodes(Domain.Enums.Label.DOWN));
                        BacktestProcess(allBacktestStrategyNodes, allProjectCandles);

                        // Check correlation

                        foreach ((var builderProcessUP, var builderProcessDOWN) in CrossingBuilderProcessesUP.Zip(CrossingBuilderProcessesDOWN, (builderProcessUP, builderProcessDOWN) => (builderProcessUP, builderProcessDOWN)))
                        {
                            builderProcessUP.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                            builderProcessDOWN.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Name;
                        }

                        CrossingBuilder.AllWinningNodes.Clear();
                        CrossingBuilder.AllWinningNodes.AddRange(
                            _builderService.Correlation<StrategyNodeModel>(
                                ProcessArgs.ProjectName,
                                EntityTypeEnum.CrossingBuilder,
                                ProjectConfiguration.MaxCorrelationPercent));

                        // Stop if target is met

                        var totalTrades = CrossingBuilder.AllWinningNodes.Select(strategyNode => strategyNode.BacktestIS.TotalTrades).Sum();

                        if (CrossingBuilder.WinningNodesUP.Count >= ProjectConfiguration.CrossingBuilderConfiguration.StrategyNodesUPTarget
                        && CrossingBuilder.WinningNodesDOWN.Count >= ProjectConfiguration.CrossingBuilderConfiguration.StrategyNodesDOWNTarget
                        && totalTrades >= ProjectConfiguration.CrossingBuilderConfiguration.TotalTradesTarget)
                        {
                            break;
                        }

                        // Stop if weka depth is met

                        CurrentWekaDepth++;
                        if (CurrentWekaDepth > ProjectConfiguration.CrossingBuilderConfiguration.WekaEndDepth)
                        {
                            CurrentWekaDepth--;
                            break;
                        }
                    }

                    foreach ((var builderProcessUP, var builderProcessDOWN) in CrossingBuilderProcessesUP.Zip(CrossingBuilderProcessesDOWN, (builderProcessUP, builderProcessDOWN) => (builderProcessUP, builderProcessDOWN)))
                    {
                        builderProcessUP.Message = BuilderProcessStatus.CBCompleted.GetMetadata().Name;
                        builderProcessDOWN.Message = BuilderProcessStatus.CBCompleted.GetMetadata().Name;
                    }
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                // Result message

                var msgUP = CrossingBuilder.WinningNodesUP.Count > 0
                ? $"{CrossingBuilder.WinningNodesUP.Count} UP Strategy {(CrossingBuilder.WinningNodesUP.Count == 1 ? "Node" : "Nodes")} Found"
                : "No UP Strategy Nodes Found";

                var msgDOWN = CrossingBuilder.WinningNodesDOWN.Count > 0
                ? $"{CrossingBuilder.WinningNodesDOWN.Count} DOWN Strategy {(CrossingBuilder.WinningNodesDOWN.Count == 1 ? "Node" : "Nodes")} Found"
                : "No DOWN Strategy Nodes Found";

                await MessageHelper.ShowMessageAsync(this,
                    Resources.CrossingBuilder,
                    $"{Resources.CrossingBuilderCompleted}\n\n" +
                    $"{msgUP}\n" +
                    $"{msgDOWN}")
                .ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                await MessageHelper.ShowMessageAsync(this,
                    Resources.CrossingBuilder,
                    Resources.CrossingBuilder + " cancelled")
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

        }, () => !IsTransactionActive && (CrossingBuilderProcessesUP.Count > 0 || CrossingBuilderProcessesDOWN.Count > 0))
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CrossingBuilderProcessesUP.Count)
            .ObservesProperty(() => CrossingBuilderProcessesDOWN.Count);

        private void ExtractionProcess(Label label, IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                label == Domain.Enums.Label.UP ? CrossingBuilderProcessesUP : CrossingBuilderProcessesDOWN,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = ProjectConfiguration.MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                builderProcess =>
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    builderProcess.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Name;

                    // Perform extraction

                    var extractionIndicators = _extractorService.BuildIndicatorsFromCSV(builderProcess.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        builderProcess.PreviousBacktestOperationsIS.First().Date,
                        builderProcess.PreviousBacktestOperationsIS.Last().Date,
                        extractionIndicators,
                        candles.ToList(),
                        ProjectConfiguration.Project.HistoricalData.TimeframeId,
                        0);

                    var filter = (from il in extractionResult[0].IntervalLabels.Select((_il, _idx) => new { _idx, _il })
                                  let backtestOperation = builderProcess.PreviousBacktestOperationsIS.Where(operation => operation.Date == il._il.Interval)
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
                    var nameSignature = builderProcess.ExtractionTemplateName.Replace(".csv", string.Empty);

                    var extractionName = $"{nameSignature}.{timeSignature}.csv";
                    var extractionPath = ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(label, extractionName);

                    _extractorService.ExtractorWrite(
                        extractionPath,
                        extractionResult);

                    builderProcess.ExtractionName = extractionName;
                    builderProcess.ExtractionPath = extractionPath;
                    builderProcess.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Name;
                });
        }

        private List<StrategyNodeModel> FindBacktestStrategyNodes(Label label)
        {
            var allBacktestStrategyNodes = new List<StrategyNodeModel>();

            foreach (var builderProcess in label == Domain.Enums.Label.UP ? CrossingBuilderProcessesUP : CrossingBuilderProcessesDOWN)
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
                    (double)ProjectConfiguration.CrossingBuilderConfiguration.WekaMaxRatio,
                    ProjectConfiguration.CrossingBuilderConfiguration.WekaNTotal);

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

                        var newStrategyNode = new StrategyNodeModel
                        {
                            ParentNodesData = new(builderProcess.CurrentParentNodes),
                            ChildNodesData = new(builderProcess.CurrentChildNodes),
                            CrossingNodesData = new(builderProcess.CurrentCrossingNodes),
                            BacktestIS = new(),
                            BacktestOS = new(),
                            BacktestStatusIS = BacktestStatus.NotStarted,
                            BacktestStatusOS = BacktestStatus.NotStarted,
                        };

                        // Add the node found to the current crossing nodes

                        newStrategyNode.CrossingNodesData.Add(Tuple.Create(node, CrossingHistoricalDataId, CrossingSymbolName));

                        return newStrategyNode;
                    })
                    .ToList();

                allBacktestStrategyNodes.AddRange(nodes);
                builderProcess.BacktestStrategyNodes.Clear();
                builderProcess.BacktestStrategyNodes.AddRange(nodes);

                builderProcess.Message = builderProcess.BacktestStrategyNodes.Count == 0
                    ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name // No backtests to do
                    : builderProcess.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Name;
            }

            return allBacktestStrategyNodes;
        }

        private void BacktestProcess(IEnumerable<StrategyNodeModel> strategyNodes, IEnumerable<Candle> candles)
        {
            Parallel.ForEach(
                Partitioner.Create(strategyNodes, EnumerablePartitionerOptions.NoBuffering),
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = ProjectConfiguration.MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token,
                },
                strategyNode =>
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    // Get a reference to the process being backtested

                    var builderProcess = strategyNode.Label == Domain.Enums.Label.UP
                    ? CrossingBuilderProcessesUP.FirstOrDefault(builderProcess => builderProcess.BacktestStrategyNodes.Any(processNode => processNode == strategyNode))
                    : CrossingBuilderProcessesDOWN.FirstOrDefault(builderProcess => builderProcess.BacktestStrategyNodes.Any(processNode => processNode == strategyNode));

                    // Lock due to builder process being shared by other threads, as one
                    // builder process could be working on different nodes in parallel

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests++;
                        builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} {(builderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";
                    }

                    strategyNode.WinningStrategy = _builderService.BuildBacktestOfStrategyNode(
                        strategyNode,
                        candles,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        builderProcess.CurrentSuccessRateIS,
                        builderProcess.CurrentSuccessRateOS,
                        _cancellationTokenSource.Token);

                    if (strategyNode.WinningStrategy)
                    {
                        SerializerHelper.SerializeNode(ProcessArgs.ProjectName, strategyNode);
                    }

                    // Lock due to builder process being shared by other threads, as one
                    // builder process could be working on different nodes in parallel

                    lock (_lock)
                    {
                        builderProcess.ExecutingBacktests--;
                        builderProcess.CompletedBacktests++;

                        builderProcess.Message = builderProcess.CompletedBacktests == builderProcess.BacktestStrategyNodes.Count
                        ? builderProcess.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Name
                        : builderProcess.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Name} of {builderProcess.ExecutingBacktests} {(builderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";
                    }
                });
        }


        private void FromCrossingBuilder()
        {
            if (CrossingBuilder.WinningNodesUP.Count > 0)
            {
                var directoryName = string.Join("-",
                    ProjectConfiguration.Project.HistoricalData.Symbol.Name,
                    CrossingBuilder.WinningNodesUP.First().CrossingNodesData.Select(crossingNode => crossingNode.Item3).First());

                var directoryInfo = new DirectoryInfo(Path.Combine(ProcessArgs.ProjectName.ProjectNodesUPDirectory(CrossingBuilder.WinningNodesUP.First().WinningUPDirectory)));
                directoryInfo.CreateSubdirectory(directoryName);
                foreach (var winningStrategyNodeUP in CrossingBuilder.WinningNodesUP)
                {
                    SerializerHelper.XMLSerializeObject(winningStrategyNodeUP, Path.Combine(directoryInfo.FullName, directoryName, winningStrategyNodeUP.Name + ".xml"));
                }
            }
            if (CrossingBuilder.WinningNodesDOWN.Count > 0)
            {
                var directoryName = string.Join("-",
                    ProjectConfiguration.Project.HistoricalData.Symbol.Name,
                    CrossingBuilder.WinningNodesDOWN.First().CrossingNodesData.Select(crossingNode => crossingNode.Item3).First());

                var directoryInfo = new DirectoryInfo(Path.Combine(ProcessArgs.ProjectName.ProjectNodesUPDirectory(CrossingBuilder.WinningNodesDOWN.First().WinningDOWNDirectory)));
                directoryInfo.CreateSubdirectory(directoryName);
                foreach (var winningStrategyNodeDOWN in CrossingBuilder.WinningNodesDOWN)
                {
                    SerializerHelper.XMLSerializeObject(winningStrategyNodeDOWN, Path.Combine(directoryInfo.FullName, directoryName, winningStrategyNodeDOWN.Name + ".xml"));
                }
            }

            CrossingBuilderProcessesUP.Clear();
            CrossingBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), "*.csv");
            foreach (var file in extractionTemplates)
            {
                CrossingBuilderProcessesUP.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                    Tree = new(),

                    BacktestStrategyNodes = new(),
                    CurrentSuccessRateIS = CrossingBuilder.WinningNodesUP.First().BacktestIS.SuccessRatePercent,
                    CurrentSuccessRateOS = CrossingBuilder.WinningNodesUP.First().BacktestOS.SuccessRatePercent,
                    CurrentParentNodes = new(CrossingBuilder.WinningNodesUP.First().ParentNodesData),
                    CurrentChildNodes = new(CrossingBuilder.WinningNodesUP.First().ChildNodesData),
                    CurrentCrossingNodes = new(CrossingBuilder.WinningNodesUP.Select(strategyNode => strategyNode.CrossingNodesData).First()),
                    PreviousBacktestOperationsIS = new(CrossingBuilder.WinningNodesUP.First().BacktestIS.BacktestOperations)
                });

                CrossingBuilderProcessesDOWN.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                    Tree = new(),

                    BacktestStrategyNodes = new(),
                    CurrentSuccessRateIS = CrossingBuilder.WinningNodesDOWN.First().BacktestIS.SuccessRatePercent,
                    CurrentSuccessRateOS = CrossingBuilder.WinningNodesDOWN.First().BacktestOS.SuccessRatePercent,
                    CurrentParentNodes = new(CrossingBuilder.WinningNodesDOWN.First().ParentNodesData),
                    CurrentChildNodes = new(CrossingBuilder.WinningNodesDOWN.First().ChildNodesData),
                    CurrentCrossingNodes = new(CrossingBuilder.WinningNodesDOWN.Select(strategyNode => strategyNode.CrossingNodesData).First()),
                    PreviousBacktestOperationsIS = new(CrossingBuilder.WinningNodesDOWN.First().BacktestIS.BacktestOperations)
                });
            }
        }

        private void FromNodeBuilder()
        {
            var singleNodesUP = new List<SingleNodeModel>();
            var singleNodesDOWN = new List<SingleNodeModel>();

            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml")
            .ToList()
            .ForEach(file =>
            {
                singleNodesUP.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
            });

            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml")
            .ToList()
            .ForEach(file =>
            {
                singleNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
            });

            CrossingBuilderProcessesUP.Clear();
            CrossingBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), "*.csv");
            foreach (var file in extractionTemplates)
            {
                CrossingBuilderProcessesUP.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                    Tree = new(),

                    BacktestStrategyNodes = new(),
                    CurrentSuccessRateIS = singleNodesUP.Select(node => node.BacktestIS.SuccessRatePercent).Sum() / singleNodesUP.Count,
                    CurrentSuccessRateOS = singleNodesUP.Select(node => node.BacktestOS.SuccessRatePercent).Sum() / singleNodesUP.Count,
                    CurrentParentNodes = new(),
                    CurrentChildNodes = new(singleNodesUP.Select(node => node.NodeData)),
                    CurrentCrossingNodes = new(),
                    PreviousBacktestOperationsIS = new(BuilderService.GetBacktestOperations(singleNodesUP))
                });

                CrossingBuilderProcessesDOWN.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                    Tree = new(),

                    BacktestStrategyNodes = new(),
                    CurrentSuccessRateIS = singleNodesDOWN.Select(node => node.BacktestIS.SuccessRatePercent).Sum() / singleNodesDOWN.Count,
                    CurrentSuccessRateOS = singleNodesDOWN.Select(node => node.BacktestOS.SuccessRatePercent).Sum() / singleNodesDOWN.Count,
                    CurrentParentNodes = new(),
                    CurrentChildNodes = new(singleNodesDOWN.Select(node => node.NodeData)),
                    CurrentCrossingNodes = new(),
                    PreviousBacktestOperationsIS = new(BuilderService.GetBacktestOperations(singleNodesDOWN))
                });
            }
        }

        private void FromAssemblyBuilder()
        {
            var assemblyNodesUP = new List<AssemblyNodeModel>();
            var assemblyNodesDOWN = new List<AssemblyNodeModel>();

            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription()), "*.xml")
            .ToList()
            .ForEach(file =>
            {
                assemblyNodesUP.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
            });

            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription()), "*.xml")
            .ToList()
            .ForEach(file =>
            {
                assemblyNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
            });

            CrossingBuilderProcessesUP.Clear();
            CrossingBuilderProcessesDOWN.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), "*.csv");
            foreach (var file in extractionTemplates)
            {
                if (IsMultiAssemblyMode)
                {
                    if (assemblyNodesUP.Count > 0)
                    {
                        CrossingBuilderProcessesUP.Add(new BuilderProcess
                        {
                            ExtractionTemplatePath = file.FullName,
                            ExtractionTemplateName = file.Name,
                            ExtractionName = file.Name,
                            Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                            Tree = new(),

                            BacktestStrategyNodes = new(),
                            CurrentSuccessRateIS = assemblyNodesUP.Select(node => node.BacktestIS.SuccessRatePercent).Sum() / assemblyNodesUP.Count,
                            CurrentSuccessRateOS = assemblyNodesUP.Select(node => node.BacktestOS.SuccessRatePercent).Sum() / assemblyNodesUP.Count,
                            CurrentParentNodes = new(assemblyNodesUP.Select(assemblyNode => assemblyNode.ParentNodeData)),
                            CurrentChildNodes = new(assemblyNodesUP.FirstOrDefault().ChildNodesData),
                            CurrentCrossingNodes = new(),
                            PreviousBacktestOperationsIS = new(BuilderService.GetBacktestOperations(assemblyNodesUP))
                        });
                    }

                    if (assemblyNodesDOWN.Count > 0)
                    {
                        CrossingBuilderProcessesDOWN.Add(new BuilderProcess
                        {
                            ExtractionTemplatePath = file.FullName,
                            ExtractionTemplateName = file.Name,
                            ExtractionName = file.Name,
                            Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                            Tree = new(),

                            BacktestStrategyNodes = new(),
                            CurrentSuccessRateIS = assemblyNodesDOWN.Select(node => node.BacktestIS.SuccessRatePercent).Sum() / assemblyNodesDOWN.Count,
                            CurrentSuccessRateOS = assemblyNodesDOWN.Select(node => node.BacktestOS.SuccessRatePercent).Sum() / assemblyNodesDOWN.Count,
                            CurrentParentNodes = new(assemblyNodesDOWN.Select(assemblyNode => assemblyNode.ParentNodeData)),
                            CurrentChildNodes = new(assemblyNodesDOWN.FirstOrDefault().ChildNodesData),
                            CurrentCrossingNodes = new(),
                            PreviousBacktestOperationsIS = new(BuilderService.GetBacktestOperations(assemblyNodesDOWN))
                        });
                    }
                }
                else
                {
                    foreach (var assemblyNode in assemblyNodesUP)
                    {
                        CrossingBuilderProcessesUP.Add(new BuilderProcess
                        {
                            ExtractionTemplatePath = file.FullName,
                            ExtractionTemplateName = file.Name,
                            ExtractionName = file.Name,
                            Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                            Tree = new(),

                            BacktestStrategyNodes = new(),
                            CurrentSuccessRateIS = assemblyNode.BacktestIS.SuccessRatePercent,
                            CurrentSuccessRateOS = assemblyNode.BacktestOS.SuccessRatePercent,
                            CurrentParentNodes = new() { assemblyNode.ParentNodeData },
                            CurrentChildNodes = new(assemblyNodesUP.FirstOrDefault().ChildNodesData),
                            CurrentCrossingNodes = new(),
                            PreviousBacktestOperationsIS = new(assemblyNode.BacktestIS.BacktestOperations.OrderBy(backtestOperation => backtestOperation.Date))
                        });
                    }

                    foreach (var assemblyNode in assemblyNodesDOWN)
                    {
                        CrossingBuilderProcessesDOWN.Add(new BuilderProcess
                        {
                            ExtractionTemplatePath = file.FullName,
                            ExtractionTemplateName = file.Name,
                            ExtractionName = file.Name,
                            Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Name,
                            Tree = new(),

                            BacktestStrategyNodes = new(),
                            CurrentSuccessRateIS = assemblyNode.BacktestIS.SuccessRatePercent,
                            CurrentSuccessRateOS = assemblyNode.BacktestOS.SuccessRatePercent,
                            CurrentParentNodes = new() { assemblyNode.ParentNodeData },
                            CurrentChildNodes = new(assemblyNodesDOWN.FirstOrDefault().ChildNodesData),
                            CurrentCrossingNodes = new(),
                            PreviousBacktestOperationsIS = new(assemblyNode.BacktestIS.BacktestOperations.OrderBy(backtestOperation => backtestOperation.Date))
                        });
                    }
                }
            }
        }

        private void DeleteCrossingBuilder(bool deleteAll)
        {
            // Reset the winning strategy nodes

            CrossingBuilder.AllWinningNodes.Clear();

            // Delete node files from the crossing Builder

            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription()), "*.xml", false, deleteAll ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription()), "*.xml", false, deleteAll ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            // Delete extractor files from the crossing Builder

            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(Domain.Enums.Label.UP), "*.csv", false, deleteAll ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(Domain.Enums.Label.DOWN), "*.csv", false, deleteAll ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set
            {
                if (SetProperty(ref _isTransactionActive, value))
                {
                    RaisePropertyChanged(nameof(CanLoad));
                }
            }
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                if (SetProperty(ref _canExecute, value))
                {
                    RaisePropertyChanged(nameof(CanLoad));
                }
            }
        }

        private bool _isCrossingStarted;
        public bool IsCrossingStarted
        {
            get => _isCrossingStarted;
            set
            {
                if (SetProperty(ref _isCrossingStarted, value))
                {
                    RaisePropertyChanged(nameof(CanLoad));
                }
            }
        }

        public bool CanLoad => !IsTransactionActive && CanExecute && !IsCrossingStarted;

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }

        private BuilderModel<StrategyNodeModel> _crossingBuilder;
        public BuilderModel<StrategyNodeModel> CrossingBuilder
        {
            get => _crossingBuilder;
            set => SetProperty(ref _crossingBuilder, value);
        }

        public ObservableCollection<BuilderProcess> CrossingBuilderProcessesUP { get; }
        public ObservableCollection<BuilderProcess> CrossingBuilderProcessesDOWN { get; }

        private bool _loadFromCrossingBuilder;
        public bool LoadFromCrossingBuilder
        {
            get => _loadFromCrossingBuilder;
            set => SetProperty(ref _loadFromCrossingBuilder, value);
        }

        private bool _loadFromNodeBuilder;
        public bool LoadFromNodeBuilder
        {
            get => _loadFromNodeBuilder;
            set => SetProperty(ref _loadFromNodeBuilder, value);
        }

        private bool _loadFromAssemblyBuilder;
        public bool LoadFromAssemblyBuilder
        {
            get => _loadFromAssemblyBuilder;
            set => SetProperty(ref _loadFromAssemblyBuilder, value);
        }

        private bool _isMultiAssemblyMode;
        public bool IsMultiAssemblyMode
        {
            get => _isMultiAssemblyMode;
            set => SetProperty(ref _isMultiAssemblyMode, value);
        }

        public ObservableCollection<Metadata> CrossingHistoricalData { get; }
        public int CrossingHistoricalDataId { get; set; }
        public string CrossingSymbolName { get; set; }

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
