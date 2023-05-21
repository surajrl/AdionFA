using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Resources;

using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;

using AutoMapper;

using Prism.Commands;
using Prism.Events;
using Prism.Ioc;

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Input;

using AdionFA.UI.Station.Project.Validators.StrategyBuilder;
using AdionFA.UI.Station.Infrastructure.Model.Weka;

using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.TransferObject.Base;

using DynamicData;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.Infrastructure.Common.StrategyBuilder.Services;
using AdionFA.Infrastructure.Common.Managements;
using ReactiveUI;
using AdionFA.Infrastructure.Common.Helpers;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class StrategyBuilderViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private ProjectVM _project;
        private CorrelationModel _correlation;
        private CancellationTokenSource _cancellationTokenSrc;
        private object _lock;

        public StrategyBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            _lock = new();
            CorrelationNodes = new();
            StrategyBuilderProcessList = new ObservableCollection<StrategyBuilderProcessModel>();
            MaxParallelism = Environment.ProcessorCount;
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            try
            {
                if (item == HamburgerMenuItems.StrategyBuilder.Replace(" ", string.Empty))
                {
                    PopulateViewModel();
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        }, (s) => true);

        public ICommand StopCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSrc.Cancel();
        }, () => IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            IsTransactionActive = true;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _cancellationTokenSrc ??= new CancellationTokenSource();
                var token = _cancellationTokenSrc.Token;

                // Validator

                if (!Validate(new StrategyBuilderValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResetBuilder(true, true);

                // Historical Data

                var projectHistoricalData = await _marketDataService.GetHistoricalData(Configuration.HistoricalDataId.Value, true);
                var projectCandles = projectHistoricalData.HistoricalDataCandles
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

                await Task.Run(async () =>
                {
                    var length = Configurations.Count;
                    var idx = 0;

                    while (idx < length)
                    {
                        token.ThrowIfCancellationRequested();

                        ResetBuilder(cleanSerializedObjDirectory: true);

                        var configuration = Configurations[idx];

                        // Asynchronous - Data Mine (Generate Weka Tree and do IS and OS Backtests)
                        if (Configuration.AsynchronousMode)
                        {
                            foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                            {
                                // Beginning

                                var beginning = StrategyBuilderStatusEnum.Beginning.GetMetadata();
                                strategyBuilderProcess.Status = beginning.Name;
                                strategyBuilderProcess.Message = beginning.Description;

                                // Weka

                                var weka = StrategyBuilderStatusEnum.ExecutingWeka.GetMetadata();
                                strategyBuilderProcess.Status = weka.Name;
                                strategyBuilderProcess.Message = weka.Description;

                                var wekaApi = new WekaApiClient();
                                IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                                    strategyBuilderProcess.Path,
                                    configuration.DepthWeka,
                                    configuration.TotalDecimalWeka,
                                    configuration.MinimalSeed,
                                    configuration.MaximumSeed,
                                    configuration.TotalInstanceWeka,
                                    (double)configuration.MaxRatioTree,
                                    (double)configuration.NTotalTree);

                                AddItemToStrategyBuilderProcessList(
                                    strategyBuilderProcess.ExtractionName,
                                    responseWeka.Select(_mapper.Map<REPTreeOutputModel, REPTreeOutputVM>).ToList());

                                if (responseWeka.Any() && strategyBuilderProcess.InstancesList.Count > 0)
                                {
                                    // Iterate over each Weka Tree from one Extraction
                                    foreach (var tree in strategyBuilderProcess.InstancesList)
                                    {
                                        token.ThrowIfCancellationRequested();

                                        CorrelationNodes.Add(tree.NodeOutput
                                            .Where(node => node.Winner)
                                            .Select(node =>
                                            {
                                                strategyBuilderProcess.TotalStrategy++;
                                                node.Tree = tree;
                                                node.StrategyBuilderProcess = strategyBuilderProcess;

                                                return node;
                                            })
                                            .ToList());
                                    }

                                    strategyBuilderProcess.Status = StrategyBuilderStatusEnum.WekaCompleted.GetMetadata().Name;
                                    strategyBuilderProcess.Message = $"{strategyBuilderProcess.TotalStrategy} Nodes Found";
                                }
                            }

                            Parallel.ForEach(
                                CorrelationNodes,
                                new ParallelOptions
                                {
                                    MaxDegreeOfParallelism = MaxParallelism,
                                    CancellationToken = token,
                                },
                                node =>
                                {
                                    lock (_lock)
                                    {
                                        node.StrategyBuilderProcess.ExecutingBacktests++;
                                    }

                                    node.StrategyBuilderProcess.Status = StrategyBuilderStatusEnum.ExecutingBacktests.GetMetadata().Name;
                                    node.StrategyBuilderProcess.Message = $"Executing Backtest of {node.StrategyBuilderProcess.ExecutingBacktests} {(node.StrategyBuilderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";

                                    node.Node = new ObservableCollection<string>(node.Node.OrderByDescending(n => n).ToList());

                                    // Backtest ------------------------------------------------------------------------------
                                    var timer = new Stopwatch();
                                    timer.Start();
                                    Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}\t[NODE] {node.Name}\t[MESSAGE] Backtest Started");
                                    var stb = _strategyBuilderService.BacktestBuild(
                                        node.Label,
                                        node.Node.ToList(),
                                        _mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(configuration),
                                        projectCandles,
                                        token);
                                    timer.Stop();
                                    Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}\t[NODE] {node.Name}\t[MESSAGE] Backtest Finished in {timer.Elapsed:mm\\:ss\\.ffffff}");
                                    // ---------------------------------------------------------------------------------------

                                    // Update Node ---------------------------------------------------------------------------
                                    UpdateTreeNodeVM(node, stb);
                                    // ---------------------------------------------------------------------------------------

                                    if (node.WinningStrategy)
                                    {
                                        // Serialization -------------------------------------------------------------------------
                                        StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.IS, true);
                                        StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.OS, false);
                                        // ---------------------------------------------------------------------------------------

                                        // Update Tree ---------------------------------------------------------------------------
                                        if (node.Label.ToLower() == "up")
                                        {
                                            node.Tree.TotalWinningStrategyUP += node.WinningStrategy ? 1 : 0;
                                        }

                                        if (node.Label.ToLower() == "down")
                                        {
                                            node.Tree.TotalWinningStrategyDOWN += node.WinningStrategy ? 1 : 0;
                                        }

                                        node.Tree.TotalWinningStrategy = node.Tree.TotalWinningStrategyUP + node.Tree.TotalWinningStrategyDOWN;
                                        node.Tree.HasWinningStrategy = node.Tree.TotalWinningStrategy > 0;
                                        node.StrategyBuilderProcess.HasWinningStrategy = true;
                                        // ---------------------------------------------------------------------------------------
                                    }

                                    lock (_lock)
                                    {
                                        node.StrategyBuilderProcess.ExecutingBacktests--;
                                        node.StrategyBuilderProcess.CompletedBacktests++;
                                        node.Tree.CounterProgressBar++;
                                    }

                                    node.StrategyBuilderProcess.Message = $"Executing Backtest of {node.StrategyBuilderProcess.ExecutingBacktests} {(node.StrategyBuilderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";

                                    if (node.StrategyBuilderProcess.TotalStrategy == node.StrategyBuilderProcess.CompletedBacktests)
                                    {
                                        var strategyBuilderCompleted = StrategyBuilderStatusEnum.StrategyBuilderCompleted.GetMetadata();
                                        node.StrategyBuilderProcess.Status = strategyBuilderCompleted.Name;
                                        node.StrategyBuilderProcess.Message = strategyBuilderCompleted.Description;
                                    }
                                });
                        }

                        // Synchronous - Data Mine (Generate Weka Tree and do IS and OS Backtests)
                        else
                        {
                            foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                            {
                                token.ThrowIfCancellationRequested();

                                await Task.Run(() => DataMine(configuration, strategyBuilderProcess, projectCandles, token));
                            }
                        }

                        // Auto Adjust Configuration

                        if (Configuration.AutoAdjustConfig && Configuration.MaxAdjustConfig > length &&
                            (Configuration.WinningStrategyTotalUP > TotalCorrelationUP || Configuration.WinningStrategyTotalDOWN > TotalCorrelationDOWN))
                        {
                            var nodes = StrategyBuilderProcessList.SelectMany(
                                st => st.InstancesList.SelectMany(i => i.NodeOutput.Where(n => !n.WinningStrategy).Select(n => n))
                            ).ToList();

                            var autoAdjustConfig = _mapper.Map<ConfigurationBaseVM, AutoAdjustConfigModel>(
                                    Configurations.Any() ? Configurations.LastOrDefault() : Configuration);

                            if (!configuration.IsDefault)
                            {
                                AdjustConfigurationBuilder(nodes, autoAdjustConfig);
                            }
                            else
                            {
                                AdjustConfigurationWeka(nodes, autoAdjustConfig);
                            }

                            length = Configurations.Count > length ? Configurations.Count : length;
                        }

                        idx++;
                    }
                });

                // Correlation

                await Task.Run(() =>
                {
                    _correlation = _strategyBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        Configuration.MaxPercentCorrelation,
                        EntityTypeEnum.StrategyBuilder);

                    IsCorrelationDetail = _correlation.Success;
                });

                UpdateTotalCorrelation();

                // Update REP Tree Node VM with correlation pass results

                _correlation.ISBacktestUP.ForEach(backtestUP =>
                {
                    var correspondingNode = CorrelationNodes.FirstOrDefault(node => node.Node.SequenceEqual(backtestUP.Node));
                    if (correspondingNode != null)
                    {
                        correspondingNode.CorrelationPass = backtestUP.CorrelationPass;
                    }
                });

                _correlation.ISBacktestDOWN.ForEach(backtestDOWN =>
                {
                    var correspondingNode = CorrelationNodes.FirstOrDefault(node => node.Node.SequenceEqual(backtestDOWN.Node));
                    if (correspondingNode != null)
                    {
                        correspondingNode.CorrelationPass = backtestDOWN.CorrelationPass;
                    }
                });

                stopwatch.Stop();

                // Results Message

                var result = !StrategyBuilderProcessList.Any(strategyBuilderProcess => strategyBuilderProcess.Status != StrategyBuilderStatusEnum.StrategyBuilderCompleted.GetMetadata().Name);
                var msg = result ? MessageResources.StrategyBuilderCompleted : MessageResources.EntityErrorTransaction;

                if (result && !IsCorrelationDetail)
                {
                    msg = $"{msg}\n{MessageResources.CorrelationRunWithNoResults}";
                }

                MessageHelper.ShowMessage(this, CommonResources.StrategyBuilder, $"{msg}\nTotal time taken {stopwatch.Elapsed:mm\\:ss\\.ffffff}");
            }
            catch (OperationCanceledException ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);

                foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                {
                    var suspended = StrategyBuilderStatusEnum.Suspended.GetMetadata();
                    strategyBuilderProcess.Status = suspended.Name;
                    strategyBuilderProcess.Message = suspended.Description;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);

                foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                {
                    var error = StrategyBuilderStatusEnum.Error.GetMetadata();
                    strategyBuilderProcess.Status = error.Name;
                    strategyBuilderProcess.Message = error.Description;
                }

                throw;
            }
            finally
            {
                _cancellationTokenSrc.Dispose();
                _cancellationTokenSrc = null;

                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);

                IsTransactionActive = false;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private bool DataMine(ConfigurationBaseVM configuration, StrategyBuilderProcessModel strategyBuilderProcess, IEnumerable<Candle> candles, CancellationToken token)
        {
            try
            {
                // Beginning

                var beginning = StrategyBuilderStatusEnum.Beginning.GetMetadata();
                strategyBuilderProcess.Status = beginning.Name;
                strategyBuilderProcess.Message = beginning.Description;

                // Weka

                var weka = StrategyBuilderStatusEnum.ExecutingWeka.GetMetadata();
                strategyBuilderProcess.Status = weka.Name;
                strategyBuilderProcess.Message = weka.Description;

                var wekaApi = new WekaApiClient();
                IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                    strategyBuilderProcess.Path,
                    configuration.DepthWeka,
                    configuration.TotalDecimalWeka,
                    configuration.MinimalSeed,
                    configuration.MaximumSeed,
                    configuration.TotalInstanceWeka,
                    (double)configuration.MaxRatioTree,
                    (double)configuration.NTotalTree);

                AddItemToStrategyBuilderProcessList(
                    strategyBuilderProcess.ExtractionName,
                    responseWeka.Select(_mapper.Map<REPTreeOutputModel, REPTreeOutputVM>).ToList());

                if (responseWeka.Any() && strategyBuilderProcess.InstancesList.Count > 0)
                {
                    strategyBuilderProcess.CompletedBacktests = 0;

                    // Iterate over each Weka Tree from one Extraction
                    foreach (var tree in strategyBuilderProcess.InstancesList)
                    {
                        CorrelationNodes.Add(tree.NodeOutput
                            .Where(node => node.Winner)
                            .Select(node =>
                            {
                                strategyBuilderProcess.TotalStrategy++;
                                node.Tree = tree;

                                return node;
                            })
                            .ToList());

                        // Iterate over each Node from one Weka Tree
                        foreach (var node in CorrelationNodes)
                        {
                            if (!node.IsBacktestCompleted)
                            {
                                node.Node = new ObservableCollection<string>(node.Node.OrderByDescending(n => n).ToList());

                                strategyBuilderProcess.Message = $"Executing Backtest of Node {node.Name}";

                                // Backtest ------------------------------------------------------------------------------
                                var timer = new Stopwatch();
                                timer.Start();
                                Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}\t[NODE] {node.Name}\t[MESSAGE] Backtest Started");
                                var stb = _strategyBuilderService.BacktestBuild(
                                    node.Label,
                                    node.Node.ToList(),
                                    _mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(configuration),
                                    candles,
                                    token);
                                timer.Stop();
                                Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}\t[NODE] {node.Name}\t[MESSAGE] Backtest Finished in {timer.Elapsed:mm\\:ss\\.ffffff}");
                                // ---------------------------------------------------------------------------------------

                                // Update Node ---------------------------------------------------------------------------
                                UpdateTreeNodeVM(node, stb);
                                // ---------------------------------------------------------------------------------------

                                if (node.WinningStrategy)
                                {
                                    // Serialization -------------------------------------------------------------------------
                                    StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.IS, true);
                                    StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.OS, false);
                                    // ---------------------------------------------------------------------------------------

                                    // Update Tree ---------------------------------------------------------------------------
                                    if (node.Label.ToLower() == "up")
                                    {
                                        node.Tree.TotalWinningStrategyUP += node.WinningStrategy ? 1 : 0;
                                    }

                                    if (node.Label.ToLower() == "down")
                                    {
                                        node.Tree.TotalWinningStrategyDOWN += node.WinningStrategy ? 1 : 0;
                                    }

                                    node.Tree.TotalWinningStrategy = node.Tree.TotalWinningStrategyUP + node.Tree.TotalWinningStrategyDOWN;
                                    node.Tree.HasWinningStrategy = node.Tree.TotalWinningStrategy > 0;
                                    strategyBuilderProcess.HasWinningStrategy = true;
                                    // ---------------------------------------------------------------------------------------
                                }

                                node.IsBacktestCompleted = true;

                                strategyBuilderProcess.CompletedBacktests++;
                                node.Tree.CounterProgressBar++;
                            }
                        }
                    }
                }

                var completed = StrategyBuilderStatusEnum.StrategyBuilderCompleted.GetMetadata();
                strategyBuilderProcess.Status = completed.Name;
                strategyBuilderProcess.Message = completed.Description;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void UpdateTreeNodeVM(REPTreeNodeVM node, StrategyBuilderModel backtest)
        {
            node.HistoricalData = Configuration.HistoricalDataName;
            node.WinningStrategy = backtest.WinningStrategy;

            // IS

            node.CorrelationPass = backtest.IS.CorrelationPass;

            node.TotalOpportunityIs = backtest.IS.TotalOpportunity;
            node.TotalTradesIs = backtest.IS.TotalTrades;
            node.WinningTradesIs = backtest.IS.WinningTrades;
            node.LosingTradesIs = backtest.IS.LosingTrades;

            node.PercentSuccessIs = backtest.IS.PercentSuccess;
            node.ProgressivenessIs = backtest.IS.Progressiveness;

            // OS

            node.CorrelationPass = backtest.OS.CorrelationPass;

            node.TotalOpportunityOs = backtest.OS.TotalOpportunity;
            node.TotalTradesOs = backtest.OS.TotalTrades;
            node.WinningTradesOs = backtest.OS.WinningTrades;
            node.LosingTradesOs = backtest.OS.LosingTrades;

            node.PercentSuccessOs = backtest.OS.PercentSuccess;
            node.ProgressivenessOs = backtest.OS.Progressiveness;
        }

        private void AdjustConfigurationBuilder(List<REPTreeNodeVM> nodes, AutoAdjustConfigModel autoAdjustConfig)
        {
            try
            {
                var targetUp = Configuration.WinningStrategyTotalUP > TotalCorrelationUP ?
                    Configuration.WinningStrategyTotalUP - TotalCorrelationUP : 0;

                var targetDown = Configuration.WinningStrategyTotalDOWN > TotalCorrelationDOWN ?
                    Configuration.WinningStrategyTotalDOWN - TotalCorrelationDOWN : 0;

                var filteredNodes = nodes.Where(
                    n => n.PercentSuccessIs >= (double)autoAdjustConfig.MinPercentSuccessIS &&
                         n.PercentSuccessOs >= (double)autoAdjustConfig.MinPercentSuccessOS &&
                         n.VariationPercent <= (double)autoAdjustConfig.VariationTransaction &&
                         (!Configuration.IsProgressiveness || n.Progressiveness <= (double)autoAdjustConfig.Progressiveness)
                ).ToList().OrderByDescending(n => n.WinningTradesIs).ThenByDescending(n => n.PercentSuccessIs).ToList();

                var nodesUp = filteredNodes.Where(n => n.Label.ToLower() == "up").ToList();
                if (nodesUp.Count > 0)
                {
                    nodesUp = nodesUp.GetRange(0, nodesUp.Count > targetUp ? targetUp : nodesUp.Count);
                }

                var nodesDown = filteredNodes.Where(n => n.Label.ToLower() == "down").ToList();
                if (nodesDown.Count > 0)
                {
                    nodesDown = nodesDown.GetRange(0, nodesDown.Count > targetDown ? targetDown : nodesDown.Count);
                }

                var upLast = nodesUp.LastOrDefault();
                var downLast = nodesDown.LastOrDefault();

                var winningTradesOs = 0;
                var winningTradesIs = 0;

                if ((upLast?.WinningTradesOs ?? 0) > 0 && (downLast?.WinningTradesOs ?? 0) > 0)
                {
                    winningTradesOs = upLast.WinningTradesOs > downLast.WinningTradesOs ? downLast.WinningTradesOs : upLast.WinningTradesOs;
                    winningTradesIs = upLast.WinningTradesIs > downLast.WinningTradesIs ? downLast.WinningTradesIs : upLast.WinningTradesIs;
                }

                if (winningTradesOs == 0)
                {
                    winningTradesOs = (upLast?.WinningTradesOs ?? 0) == 0 ? (downLast?.WinningTradesOs ?? 0) : (upLast?.WinningTradesOs ?? 0);
                }

                if (winningTradesIs == 0)
                {
                    winningTradesIs = (upLast?.WinningTradesIs ?? 0) == 0 ? (downLast?.WinningTradesIs ?? 0) : (upLast?.WinningTradesIs ?? 0);
                }

                autoAdjustConfig.MinTransactionCountOS = winningTradesOs > 0 ? winningTradesOs : autoAdjustConfig.MinTransactionCountOS;
                autoAdjustConfig.MinTransactionCountIS = winningTradesIs > 0 ? winningTradesIs : autoAdjustConfig.MinTransactionCountIS;

                autoAdjustConfig.AdjustMinTransactionCountOS = Configurations.LastOrDefault().MinTransactionCountOS != autoAdjustConfig.MinTransactionCountOS;
                autoAdjustConfig.AdjustMinTransactionCountIS = Configurations.LastOrDefault().MinTransactionCountIS != autoAdjustConfig.MinTransactionCountIS;

                if (autoAdjustConfig.AdjustMinTransactionCountOS || autoAdjustConfig.AdjustMinTransactionCountIS)
                {
                    Configurations.Add(autoAdjustConfig);
                    CurrentConfiguration = Configurations.Count;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);

                throw;
            }
        }

        private void AdjustConfigurationWeka(List<REPTreeNodeVM> nodes, AutoAdjustConfigModel autoAdjustConfig)
        {
            try
            {
                var lookingForTargetUp = Configuration.WinningStrategyTotalUP > TotalCorrelationUP ?
                    Configuration.WinningStrategyTotalUP - TotalCorrelationUP : 0;

                var lookingForTargetDown = Configuration.WinningStrategyTotalDOWN > TotalCorrelationDOWN ?
                    Configuration.WinningStrategyTotalDOWN - TotalCorrelationDOWN : 0;

                var ntotalesUp = 0;
                var ntotalesDown = 0;

                var nodesGraterThanRationMax = nodes.Where(n => !n.Winner && n.RatioMax >= (double)autoAdjustConfig.MaxRatioTree).ToList();
                var resultNodes = new List<REPTreeNodeVM>();

                if (nodesGraterThanRationMax.Count > 0)
                {
                    // UP

                    var nodesOrderByTotalUpDesc = nodesGraterThanRationMax.Where(n => n.Label.ToLower() == "up").OrderByDescending(n => n.TotalUP).ToList();
                    if (nodesOrderByTotalUpDesc.Count > 0)
                    {
                        nodesOrderByTotalUpDesc = nodesOrderByTotalUpDesc.GetRange(0, nodesOrderByTotalUpDesc.Count > lookingForTargetUp ? lookingForTargetUp : nodesOrderByTotalUpDesc.Count - 1);
                    }

                    resultNodes.AddRange(nodesOrderByTotalUpDesc);
                    var totalUp = nodesOrderByTotalUpDesc.Count;

                    if (totalUp > 0)
                    {
                        ntotalesUp = (int)(resultNodes.Where(_n => _n.Label.ToLower() == "up").OrderBy(w => w.TotalUP).FirstOrDefault()?.TotalUP ?? 50);
                    }

                    // DOWN

                    var nodesOrderByTotalDownDesc = nodesGraterThanRationMax.Where(n => n.Label.ToLower() == "down").OrderByDescending(n => n.TotalDOWN).ToList();
                    if (nodesOrderByTotalDownDesc.Count > 0)
                    {
                        nodesOrderByTotalDownDesc = nodesOrderByTotalDownDesc.GetRange(0, nodesOrderByTotalDownDesc.Count > lookingForTargetDown ? lookingForTargetDown : nodesOrderByTotalDownDesc.Count - 1);
                    }

                    resultNodes.AddRange(nodesOrderByTotalDownDesc);
                    var totalDown = nodesOrderByTotalDownDesc.Count;

                    if (totalDown > 0)
                    {
                        ntotalesDown = (int)(resultNodes.Where(_n => _n.Label.ToLower() == "down").OrderBy(w => w.TotalDOWN).FirstOrDefault()?.TotalDOWN ?? 50);
                    }
                }

                if (ntotalesUp > 0 && ntotalesDown > 0)
                {
                    autoAdjustConfig.NTotalTree = ntotalesUp > ntotalesDown ? ntotalesDown : ntotalesUp;
                    autoAdjustConfig.adjustNTotalTree = Configuration.NTotalTree != autoAdjustConfig.NTotalTree;

                    Configurations.Add(autoAdjustConfig);
                    CurrentConfiguration = Configurations.Count;

                    return;
                }

                var result = ntotalesUp == 0 ? ntotalesDown : ntotalesUp;

                autoAdjustConfig.NTotalTree = result == 0 ? 50 : result;
                autoAdjustConfig.adjustNTotalTree = Configurations.LastOrDefault().NTotalTree != autoAdjustConfig.NTotalTree;

                if (autoAdjustConfig.adjustNTotalTree)
                {
                    Configurations.Add(autoAdjustConfig);
                    CurrentConfiguration = Configurations.Count;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ICommand RefreshCommand => new DelegateCommand(() =>
        {
            try
            {
                PopulateViewModel(true);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);

                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel(bool refresh = false)
        {
            try
            {
                _project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);
                Configuration = _project?.ProjectConfigurations.FirstOrDefault();

                _correlation = _strategyBuilderService.Correlation(ProcessArgs.ProjectName, Configuration.MaxPercentCorrelation, EntityTypeEnum.StrategyBuilder);
                IsCorrelationDetail = _correlation != null;

                UpdateTotalCorrelation();

                if (refresh)
                {
                    StrategyBuilderProcessList.Clear();
                    ResetBuilder(true);
                }

                if (!IsTransactionActive && !StrategyBuilderProcessList.Any())
                {
                    if (Configuration != null)
                    {
                        if (Configuration.WithoutSchedule == false)
                        {
                            PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory());
                            PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory());
                            PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory());
                        }
                        else
                        {
                            PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private void PopulateStrategyBuilderProcessList(string extractionPath)
        {
            var regionName = "WithoutSchedule";
            var regionType = 0;

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America)))
            {
                regionType = (int)MarketRegionEnum.America;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America);
            }

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe)))
            {
                regionType = (int)MarketRegionEnum.Europe;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe);
            }

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia)))
            {
                regionType = (int)MarketRegionEnum.Asia;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia);
            }

            _projectDirectoryService.GetFilesInPath(extractionPath).ToList().ForEach(file =>
            {
                StrategyBuilderProcessList.Add(new StrategyBuilderProcessModel
                {
                    Path = file.FullName,
                    ExtractionName = file.Name,
                    RegionType = regionType,
                    RegionName = regionName,
                    Status = StrategyBuilderStatusEnum.NoStarted.GetMetadata().Name,
                    IsEnabled = false,
                    IsExpanded = false,
                    InstancesList = new ObservableCollection<REPTreeOutputVM>()
                });
            });
        }

        private void AddItemToStrategyBuilderProcessList(string templateName, List<REPTreeOutputVM> treeList)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var strategyBuilderProcess = StrategyBuilderProcessList.FirstOrDefault(m => m.ExtractionName == templateName);
                treeList.ForEach(tree =>
                {
                    tree.Message = tree.HasWinningStrategy == null ? CommonResources.Pending : tree.HasWinningStrategy.Value ? CommonResources.Winner : CommonResources.Loser;
                    tree.ValidNodes = tree.NodeOutput.Where(node => node.Winner).Count();
                });

                strategyBuilderProcess.WinningTrees = treeList.Where(n => n.ValidNodes > 0).Count();
                strategyBuilderProcess.InstancesList.Clear();
                strategyBuilderProcess.InstancesList.AddRange(treeList);

                if (strategyBuilderProcess.WinningTrees > 0)
                {
                    strategyBuilderProcess.IsEnabled = true;
                }
            });
        }

        private void UpdateTotalCorrelation()
        {
            TotalCorrelationUP = TotalCorrelationDOWN = 0;

            Application.Current.Dispatcher.Invoke(() =>
            {
                TotalCorrelationUP += _correlation?.ISBacktestUP?.Count ?? 0;
                TotalCorrelationDOWN += _correlation?.ISBacktestDOWN?.Count ?? 0;
            });
        }

        private void ResetBuilder(bool resetConfigurations = false, bool cleanSerializedObjDirectory = false)
        {
            UpdateTotalCorrelation();

            CorrelationNodes.Clear();

            foreach (var model in StrategyBuilderProcessList)
            {
                model.Reset();
            }

            if (resetConfigurations)
            {
                Configurations = new ObservableCollection<AutoAdjustConfigModel>
                {
                    _mapper.Map<ConfigurationBaseVM, AutoAdjustConfigModel>(Configuration)
                };

                CurrentConfiguration = 1;
            }

            if (cleanSerializedObjDirectory)
            {
                IsCorrelationDetail = false;
            }
        }

        // Bindable Model

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

        private ObservableCollection<REPTreeNodeVM> _correlationNodes;

        public ObservableCollection<REPTreeNodeVM> CorrelationNodes
        {
            get => _correlationNodes;
            set => SetProperty(ref _correlationNodes, value);
        }

        private ProjectConfigurationVM _configuration;

        public ProjectConfigurationVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private int _totalCorrelationUP;

        public int TotalCorrelationUP
        {
            get => _totalCorrelationUP;
            set => SetProperty(ref _totalCorrelationUP, value);
        }

        private int _totalCorrelationDOWN;

        public int TotalCorrelationDOWN
        {
            get => _totalCorrelationDOWN;
            set => SetProperty(ref _totalCorrelationDOWN, value);
        }

        private bool _isCorrelationDetail;

        public bool IsCorrelationDetail
        {
            get => _isCorrelationDetail;
            set => SetProperty(ref _isCorrelationDetail, value);
        }

        private int _maxParallelism;

        public int MaxParallelism
        {
            get => _maxParallelism;
            set => SetProperty(ref _maxParallelism, value);
        }

        private int _currentConfiguration;

        public int CurrentConfiguration
        {
            get => _currentConfiguration;
            set => SetProperty(ref _currentConfiguration, value);
        }

        private ObservableCollection<AutoAdjustConfigModel> _configurations;

        public ObservableCollection<AutoAdjustConfigModel> Configurations
        {
            get => _configurations;
            set => SetProperty(ref _configurations, value);
        }

        private ObservableCollection<StrategyBuilderProcessModel> _strategyBuilderProcessList;

        public ObservableCollection<StrategyBuilderProcessModel> StrategyBuilderProcessList
        {
            get => _strategyBuilderProcessList;
            set => SetProperty(ref _strategyBuilderProcessList, value);
        }
    }
}