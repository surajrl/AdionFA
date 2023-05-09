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
using System.Net.WebSockets;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

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

            TotalNodes = new();
            StrategyBuilderProcessList = new ObservableCollection<StrategyBuilderProcessModel>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
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

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            IsTransactionActive = true;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
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
                    Label = hdCandle.Close > hdCandle.Open ? "UP" : "DOWN"
                })
                .OrderBy(candle => candle.Date)
                .ThenBy(candle => candle.Time)
                .ToList();

                await Task.Factory.StartNew(() =>
                {
                    var length = Configurations.Count;
                    var idx = 0;

                    while (idx < length)
                    {
                        ResetBuilder(cleanSerializedObjDirectory: true);

                        var configuration = Configurations[idx];

                        // Asynchronous - Data Mine (Generate Weka Tree and do IS and OS Backtests)
                        if (Configuration.AsynchronousMode)
                        {
                            var taskPool = StrategyBuilderProcessList.Select(strategyBuilderProcess => new Task(() =>
                            {
                                AsyncDataMine(configuration, strategyBuilderProcess, projectCandles);
                            }, TaskCreationOptions.AttachedToParent)).ToList();

                            taskPool.ForEach(task => task.Start());
                            Task.WaitAll(taskPool.ToArray());
                        }

                        // Synchronous - Data Mine (Generate Weka Tree and do IS and OS Backtests)
                        else
                        {
                            foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                            {
                                var task = new Task(() => DataMine(configuration, strategyBuilderProcess, projectCandles));
                                task.Start();
                                task.Wait();
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
                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);

                // Correlation

                await Task.Run(() =>
                {
                    CorrelationModel = _strategyBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        Configuration.MaxPercentCorrelation,
                        EntityTypeEnum.StrategyBuilder);

                    IsCorrelationDetail = CorrelationModel.Success;
                });

                UpdateTotalCorrelation(CorrelationModel?.ISBacktestUP?.Count ?? 0, CorrelationModel?.ISBacktestDOWN?.Count ?? 0);

                // Update REP Tree Node VM with correlation pass results

                CorrelationModel.ISBacktestUP.ForEach(backtestUP =>
                {
                    var correspondingNode = TotalNodes.FirstOrDefault(node => node.Node.SequenceEqual(backtestUP.Node));
                    if (correspondingNode != null)
                    {
                        correspondingNode.CorrelationPass = backtestUP.CorrelationPass;
                    }
                });

                CorrelationModel.ISBacktestDOWN.ForEach(backtestDOWN =>
                {
                    var correspondingNode = TotalNodes.FirstOrDefault(node => node.Node.SequenceEqual(backtestDOWN.Node));
                    if (correspondingNode != null)
                    {
                        correspondingNode.CorrelationPass = backtestDOWN.CorrelationPass;
                    }
                });

                stopwatch.Stop();

                IsTransactionActive = false;

                // Results Message

                var result = !StrategyBuilderProcessList.Any(e => e.Status != StrategyBuilderStatusEnum.Completed.GetMetadata().Name);
                var msg = result ? MessageResources.StrategyBuilderCompleted : MessageResources.EntityErrorTransaction;

                if (result && !IsCorrelationDetail)
                {
                    msg = $"{MessageResources.CorrelationRunWithNoResults} in {stopwatch.ElapsedMilliseconds}";
                }

                MessageHelper.ShowMessage(this, CommonResources.StrategyBuilder, msg);
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
                IsTransactionActive = false;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private bool AsyncDataMine(ConfigurationBaseVM configuration, StrategyBuilderProcessModel strategyBuilderProcess, IEnumerable<Candle> candles)
        {
            try
            {
                // Beginning

                var beginning = StrategyBuilderStatusEnum.Beginning.GetMetadata();
                strategyBuilderProcess.Status = beginning.Name;
                strategyBuilderProcess.Message = $"{beginning.Description}";

                // Weka

                var weka = StrategyBuilderStatusEnum.ExecutingWeka.GetMetadata();
                strategyBuilderProcess.Status = weka.Name;
                strategyBuilderProcess.Message = $"{weka.Description}";

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

                strategyBuilderProcess.Message = $"Pruning Tree";

                AddItemToStrategyBuilderProcessList(
                    strategyBuilderProcess.TemplateName,
                    responseWeka.Select(_mapper.Map<REPTreeOutputModel, REPTreeOutputVM>).ToList());

                if (responseWeka.Any() && strategyBuilderProcess.InstancesList.Count > 0)
                {
                    strategyBuilderProcess.CompletedBacktests = 0;

                    // Iterate over each Weka Tree from one Extraction
                    foreach (var tree in strategyBuilderProcess.InstancesList)
                    {
                        var validNodes = new List<REPTreeNodeVM>
                        {
                            tree.NodeOutput.Where(node => node.Winner).ToList()
                        };

                        strategyBuilderProcess.TotalStrategy = validNodes.Count;

                        // Iterate over each Node from one Weka Tree
                        var taskPool = new List<Task>();

                        foreach (var node in validNodes)
                        {
                            taskPool.Add(new Task(() =>
                            {
                                node.Node = new ObservableCollection<string>(node.Node.OrderByDescending(n => n).ToList());

                                if (!node.HasBacktest)
                                {
                                    TotalNodes.Add(node);

                                    node.HistoricalData = Configuration.HistoricalDataName;

                                    // Backtest

                                    var stb = _strategyBuilderService.BacktestBuild(
                                        node.Label,
                                        node.Node.ToList(),
                                        _mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(configuration),
                                        candles);

                                    node.HasBacktest = true;

                                    // ---------------------------------------------------------------------------------------

                                    UpdateTreeNodeVM(node, stb.IS, false);
                                    UpdateTreeNodeVM(node, stb.OS, true);

                                    node.WinningStrategy = stb.WinningStrategy;

                                    if (node.WinningStrategy)
                                    {
                                        if (node.Label.ToLower() == "up")
                                        {
                                            tree.TotalWinningStrategyUP += node.WinningStrategy ? 1 : 0;
                                        }

                                        if (node.Label.ToLower() == "down")
                                        {
                                            tree.TotalWinningStrategyDOWN += node.WinningStrategy ? 1 : 0;
                                        }

                                        tree.TotalWinningStrategy = tree.TotalWinningStrategyUP + tree.TotalWinningStrategyDOWN;
                                        tree.HasWinningStrategy = tree.TotalWinningStrategy > 0;
                                    }

                                    // Serialization

                                    if (node.WinningStrategy)
                                    {
                                        StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.IS, true);
                                        StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.OS, false);
                                    }

                                    // ---------------------------------------------------------------------------------------

                                    strategyBuilderProcess.CompletedBacktests++;
                                    tree.CounterProgressBar++;
                                }
                            }));
                        }

                        taskPool.ForEach(task => task.Start());
                        Task.WaitAll(taskPool.ToArray());

                        // Update Weka Tree when backtest of all nodes has been completed

                        strategyBuilderProcess.HasWinningStrategy = tree.TotalWinningStrategy > 0;
                        strategyBuilderProcess.TotalWinningStrategyUP += tree.TotalWinningStrategyUP;
                        strategyBuilderProcess.TotalWinningStrategyDOWN += tree.TotalWinningStrategyDOWN;
                        strategyBuilderProcess.TotalWinningStrategy = strategyBuilderProcess.TotalWinningStrategyUP + strategyBuilderProcess.TotalWinningStrategyDOWN;
                    }
                }

                var completed = StrategyBuilderStatusEnum.Completed.GetMetadata();
                strategyBuilderProcess.Status = completed.Name;
                strategyBuilderProcess.Message = $"{completed.Description}";

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                var error = StrategyBuilderStatusEnum.Error.GetMetadata();
                strategyBuilderProcess.Status = error.Name;
                strategyBuilderProcess.Message = ex.Message;
                return false;
            }
        }

        private bool DataMine(ConfigurationBaseVM configuration, StrategyBuilderProcessModel strategyBuilderProcess, IEnumerable<Candle> candles)
        {
            try
            {
                // Beginning

                var beginning = StrategyBuilderStatusEnum.Beginning.GetMetadata();
                strategyBuilderProcess.Status = beginning.Name;
                strategyBuilderProcess.Message = $"{beginning.Description}";

                // Weka

                var weka = StrategyBuilderStatusEnum.ExecutingWeka.GetMetadata();
                strategyBuilderProcess.Status = weka.Name;
                strategyBuilderProcess.Message = $"{weka.Description}";

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

                strategyBuilderProcess.Message = $"Pruning Tree";

                if (responseWeka.Any())
                {
                    strategyBuilderProcess.CompletedBacktests = 0;

                    AddItemToStrategyBuilderProcessList(
                        strategyBuilderProcess.TemplateName,
                        responseWeka.Select(_mapper.Map<REPTreeOutputModel, REPTreeOutputVM>).ToList());

                    if (strategyBuilderProcess.InstancesList.Count > 0)
                    {
                        foreach (var tree in strategyBuilderProcess.InstancesList)
                        {
                            var validNodes = new List<REPTreeNodeVM>
                            {
                                tree.NodeOutput.Where(node => node.Winner).ToList()
                            };

                            validNodes.ForEach(node =>
                            {
                                node.Node = new ObservableCollection<string>(node.Node.OrderByDescending(n => n).ToList());
                            });

                            strategyBuilderProcess.TotalStrategy = validNodes.Count;

                            for (var idx = 0; idx < validNodes.Count; idx++)
                            {
                                if (validNodes[idx].HasBacktest)
                                {
                                    continue;
                                }

                                TotalNodes.Add(validNodes[idx]);
                                strategyBuilderProcess.Message = $"Executing Backtest {idx + 1}";

                                validNodes[idx].HistoricalData = Configuration.HistoricalDataName;

                                // Backtest

                                var stb = _strategyBuilderService.BacktestBuild(
                                    validNodes[idx].Label,
                                    validNodes[idx].Node.ToList(),
                                    _mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(configuration),
                                    candles);

                                validNodes[idx].HasBacktest = true;

                                // ---------------------------------------------------------------------------------------

                                UpdateTreeNodeVM(validNodes[idx], stb.IS, false);
                                UpdateTreeNodeVM(validNodes[idx], stb.OS, true);

                                validNodes[idx].WinningStrategy = stb.WinningStrategy;

                                if (validNodes[idx].WinningStrategy)
                                {
                                    if (validNodes[idx].Label.ToLower() == "up")
                                    {
                                        tree.TotalWinningStrategyUP += validNodes[idx].WinningStrategy ? 1 : 0;
                                    }

                                    if (validNodes[idx].Label.ToLower() == "down")
                                    {
                                        tree.TotalWinningStrategyDOWN += validNodes[idx].WinningStrategy ? 1 : 0;
                                    }

                                    tree.TotalWinningStrategy = tree.TotalWinningStrategyUP + tree.TotalWinningStrategyDOWN;
                                    tree.HasWinningStrategy = tree.TotalWinningStrategy > 0;
                                }

                                // Serialization

                                if (validNodes[idx].WinningStrategy)
                                {
                                    StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.IS, true);
                                    StrategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.OS, false);
                                }

                                // ---------------------------------------------------------------------------------------

                                strategyBuilderProcess.CompletedBacktests++;
                                tree.CounterProgressBar++;
                            }

                            // Update Winning Strategy Variables

                            strategyBuilderProcess.HasWinningStrategy = tree.TotalWinningStrategy > 0;
                            strategyBuilderProcess.TotalWinningStrategyUP += tree.TotalWinningStrategyUP;
                            strategyBuilderProcess.TotalWinningStrategyDOWN += tree.TotalWinningStrategyDOWN;
                            strategyBuilderProcess.TotalWinningStrategy = strategyBuilderProcess.TotalWinningStrategyUP + strategyBuilderProcess.TotalWinningStrategyDOWN;
                        }
                    }
                }

                var completed = StrategyBuilderStatusEnum.Completed.GetMetadata();
                strategyBuilderProcess.Status = completed.Name;
                strategyBuilderProcess.Message = $"{completed.Description}";

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                var error = StrategyBuilderStatusEnum.Error.GetMetadata();
                strategyBuilderProcess.Status = error.Name;
                strategyBuilderProcess.Message = ex.Message;
                return false;
            }
        }

        private static void UpdateTreeNodeVM(REPTreeNodeVM node, BacktestModel backtest, bool IsOSample)
        {
            if (!IsOSample)
            {
                node.TotalOpportunityIs = backtest.TotalOpportunity;
                node.TotalTradesIs = backtest.TotalTrades;
                node.WinningTradesIs = backtest.WinningTrades;
                node.LosingTradesIs = backtest.LosingTrades;

                node.PercentSuccessIs = backtest.PercentSuccess;
                node.ProgressivenessIs = backtest.Progressiveness;
            }
            else
            {
                node.TotalOpportunityOs = backtest.TotalOpportunity;
                node.TotalTradesOs = backtest.TotalTrades;
                node.WinningTradesOs = backtest.WinningTrades;
                node.LosingTradesOs = backtest.LosingTrades;

                node.PercentSuccessOs = backtest.PercentSuccess;
                node.ProgressivenessOs = backtest.Progressiveness;
            }

            node.CorrelationPass = backtest.CorrelationPass;
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

                var WinningTradesOs = 0;
                var WinningTradesIs = 0;

                if ((upLast?.WinningTradesOs ?? 0) > 0 && (downLast?.WinningTradesOs ?? 0) > 0)
                {
                    WinningTradesOs = upLast.WinningTradesOs > downLast.WinningTradesOs ? downLast.WinningTradesOs : upLast.WinningTradesOs;
                    WinningTradesIs = upLast.WinningTradesIs > downLast.WinningTradesIs ? downLast.WinningTradesIs : upLast.WinningTradesIs;
                }

                if (WinningTradesOs == 0)
                {
                    WinningTradesOs = (upLast?.WinningTradesOs ?? 0) == 0 ? (downLast?.WinningTradesOs ?? 0) : (upLast?.WinningTradesOs ?? 0);
                }

                if (WinningTradesIs == 0)
                {
                    WinningTradesIs = (upLast?.WinningTradesIs ?? 0) == 0 ? (downLast?.WinningTradesIs ?? 0) : (upLast?.WinningTradesIs ?? 0);
                }

                autoAdjustConfig.MinTransactionCountOS = WinningTradesOs > 0 ? WinningTradesOs : autoAdjustConfig.MinTransactionCountOS;
                autoAdjustConfig.MinTransactionCountIS = WinningTradesIs > 0 ? WinningTradesIs : autoAdjustConfig.MinTransactionCountIS;

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

        // Refresh Command

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

                CorrelationModel = _strategyBuilderService.Correlation(ProcessArgs.ProjectName, Configuration.MaxPercentCorrelation, EntityTypeEnum.StrategyBuilder);
                IsCorrelationDetail = CorrelationModel != null;

                UpdateTotalCorrelation(CorrelationModel?.ISBacktestUP?.Count ?? 0, CorrelationModel?.ISBacktestDOWN?.Count ?? 0);

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

        private void PopulateStrategyBuilderProcessList(string templatePath)
        {
            var regionName = "WithoutSchedule";
            var regionType = 0;

            if (templatePath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America)))
            {
                regionType = (int)MarketRegionEnum.America;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America);
            }

            if (templatePath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe)))
            {
                regionType = (int)MarketRegionEnum.Europe;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe);
            }

            if (templatePath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia)))
            {
                regionType = (int)MarketRegionEnum.Asia;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia);
            }

            _projectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(file =>
            {
                StrategyBuilderProcessList.Add(new StrategyBuilderProcessModel
                {
                    Path = file.FullName,
                    TemplateName = file.Name,
                    RegionType = regionType,
                    RegionName = regionName,
                    Status = StrategyBuilderStatusEnum.NoStarted.GetMetadata().Name,
                    IsEnabled = false,
                    IsExpanded = false,
                    InstancesList = new ObservableCollection<REPTreeOutputVM>()
                });
            });
        }

        private void AddItemToStrategyBuilderProcessList(string templateName, List<REPTreeOutputVM> modelList)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var model = StrategyBuilderProcessList.FirstOrDefault(m => m.TemplateName == templateName);
                modelList.ForEach(vm =>
                {
                    vm.Message = vm.HasWinningStrategy == null ? CommonResources.Pending : vm.HasWinningStrategy.Value ? CommonResources.Winner : CommonResources.Loser;
                    vm.ValidNodes = vm.NodeOutput.Where(n => n.Winner).Count();
                });

                model.WinningTrees = modelList.Where(n => n.ValidNodes > 0).Count();
                model.TotalWinningStrategy = model.TotalWinningStrategyUP = model.TotalWinningStrategyDOWN = 0;
                model.InstancesList.Clear();
                model.InstancesList.AddRange(modelList);

                if (model.WinningTrees > 0)
                {
                    model.IsEnabled = true;
                }
            });
        }

        private void UpdateTotalCorrelation(int totalCorrelationUP, int totalCorrelationDOWN)
        {
            TotalCorrelationUP = TotalCorrelationDOWN = 0;

            Application.Current.Dispatcher.Invoke(() =>
            {
                TotalCorrelationUP += totalCorrelationUP;
                TotalCorrelationDOWN += totalCorrelationDOWN;
            });
        }

        private void ResetBuilder(bool resetConfigurations = false, bool cleanSerializedObjDirectory = false)
        {
            UpdateTotalCorrelation(0, 0);

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
                _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory(), "*.xml", SearchOption.AllDirectories, true);
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

        private ObservableCollection<REPTreeNodeVM> _totalNodes;

        public ObservableCollection<REPTreeNodeVM> TotalNodes
        {
            get => _totalNodes;
            set => SetProperty(ref _totalNodes, value);
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

        private CorrelationModel _correlationModel;

        public CorrelationModel CorrelationModel
        {
            get => _correlationModel;
            set => SetProperty(ref _correlationModel, value);
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