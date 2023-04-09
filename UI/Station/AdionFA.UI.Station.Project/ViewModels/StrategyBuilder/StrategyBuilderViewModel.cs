using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
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
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.UI.Station.Infrastructure.Helpers;

using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.TransferObject.Base;

using DynamicData;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class StrategyBuilderViewModel : MenuItemViewModel
    {
        public readonly IMapper Mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private ProjectVM Project;

        public StrategyBuilderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            Mapper = new MapperConfiguration(mc =>
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
        }, (s) => true); //item => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                // Validator

                if (!Validate(new StrategyBuilderValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResetBuilder(true, true);

                // Historical Data

                HistoricalDataVM projectHistoricalData = await _marketDataService.GetHistoricalData(Configuration.HistoricalDataId.Value, true);
                IEnumerable<Candle> candles = projectHistoricalData.HistoricalDataDetails.Select(
                        h => new Candle
                        {
                            Date = h.StartDate,
                            Time = h.StartTime,
                            Open = h.OpenPrice,
                            High = h.MaxPrice,
                            Low = h.MinPrice,
                            Close = h.ClosePrice,
                            Volume = h.Volume
                        }
                    ).OrderBy(d => d.Date)
                    .ThenBy(d => d.Time).ToList();

                // Data Mine

                await Task.Factory.StartNew(() =>
                {
                    int length = Configurations.Count;
                    int idx = 0;
                    while (idx < length)
                    {
                        ResetBuilder(cleanSerializedObjDirectory: true);

                        AutoAdjustConfigModel c = Configurations[idx];

                        // Async - Data Mine

                        if (Configuration.AsynchronousMode)
                        {
                            var pool = AsynchronousMode(c, candles);
                            pool.ForEach(t => t.Start());
                            Task.WaitAll(pool.ToArray());
                        }

                        // Sync - Data Mine
                        else
                        {
                            var sync = SynchronousMode(c, candles);
                            sync.Start();
                            sync.Wait();
                        }

                        // AutoAdjustConfig

                        if (Configuration.AutoAdjustConfig && Configuration.MaxAdjustConfig > length &&
                            (Configuration.WinningStrategyTotalUP > WinningStrategyTotalUP
                                || Configuration.WinningStrategyTotalDOWN > WinningStrategyTotalDOWN))
                        {
                            List<REPTreeNodeVM> nodes = StrategyBuilderProcessList.SelectMany(
                                st => st.InstancesList.SelectMany(i => i.NodeOutput.Where(n => !n.WinningStrategy).Select(n => n))
                            ).ToList();

                            AutoAdjustConfigModel autoAdjustConfig = Mapper.Map<ConfigurationBaseVM, AutoAdjustConfigModel>(
                                    Configurations.Any() ? Configurations.LastOrDefault() : Configuration);

                            if (!c.IsDefault)
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

                UpdateWinnerStrategyTotal(
                    CorrelationModel?.ISBacktestUP?.Count ?? 0,
                    CorrelationModel?.ISBacktestDOWN?.Count ?? 0,
                    true);

                IsTransactionActive = false;

                // Results Message

                bool result = !StrategyBuilderProcessList.Any(e => e.Status != StrategyBuilderStatusEnum.Completed.GetMetadata().Name);
                string msg = result ? MessageResources.StrategyBuilderCompleted : MessageResources.EntityErrorTransaction;

                if (result && !IsCorrelationDetail)
                {
                    msg = $"{MessageResources.CorrelationRunWithNoResults}";
                }

                MessageHelper.ShowMessage(this, CommonResources.StrategyBuilder, msg);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private List<Task> AsynchronousMode(ConfigurationBaseVM config, IEnumerable<Candle> candles)
        {
            return StrategyBuilderProcessList.Select(model => new Task(() =>
            {
                DataMine(config, model, candles);
            }, TaskCreationOptions.AttachedToParent)).ToList();
        }

        private Task SynchronousMode(ConfigurationBaseVM config, IEnumerable<Candle> candles)
        {
            return new Task(() =>
            {
                foreach (var model in StrategyBuilderProcessList)
                {
                    DataMine(config, model, candles);
                }
            }, TaskCreationOptions.AttachedToParent);
        }

        private bool DataMine(ConfigurationBaseVM config, StrategyBuilderProcessModel model, IEnumerable<Candle> candles)
        {
            try
            {
                // Beginning

                var beginning = StrategyBuilderStatusEnum.Beginning.GetMetadata();
                model.Status = beginning.Name;
                model.Message = $"{beginning.Description}";

                // Weka

                var weka = StrategyBuilderStatusEnum.ExecutingWeka.GetMetadata();
                model.Status = weka.Name;
                model.Message = $"{weka.Description}";

                var wekaApi = new WekaApiClient();
                IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                    model.Path,
                    config.DepthWeka,
                    config.TotalDecimalWeka,
                    config.MinimalSeed,
                    config.MaximumSeed,
                    config.TotalInstanceWeka,
                    (double)config.MaxRatioTree,
                    (double)config.NTotalTree
                );
                model.Message = $"Pruning Tree";

                if (responseWeka.Any())
                {
                    AddItemToStrategyBuilderProcessList(
                        model.TemplateName,
                        responseWeka.Select(Mapper.Map<REPTreeOutputModel, REPTreeOutputVM>)
                        .ToList());

                    if (model.InstancesList.Count > 0)
                    {
                        foreach (var instance in model.InstancesList)
                        {
                            List<REPTreeNodeVM> winningNodes = instance.NodeOutput.Where(m => m.Winner).ToList();
                            winningNodes.ForEach(m =>
                            {
                                m.Node = new ObservableCollection<string>(m.Node.OrderByDescending(n => n).ToList());
                            });

                            foreach (var node in winningNodes)
                            {
                                instance.CounterProgressBar++;

                                node.HistoricalData = Configuration.HistoricalDataName;

                                // Backtest

                                StrategyBuilderModel stb = _strategyBuilderService.BacktestBuild(
                                    node.Label,
                                    node.Node.ToList(),
                                    Mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(config),
                                    candles);

                                // IS

                                UpdateTreeNodeVM(node, stb.IS, false);

                                // OS

                                UpdateTreeNodeVM(node, stb.OS, true);

                                // Strategy

                                node.WinningStrategy = stb.WinningStrategy;

                                if (node.WinningStrategy)
                                {
                                    if (node.Label.ToLower() == "up")
                                    {
                                        instance.TotalWinningStrategyUP += node.WinningStrategy ? 1 : 0;
                                    }
                                    if (node.Label.ToLower() == "down")
                                    {
                                        instance.TotalWinningStrategyDOWN += node.WinningStrategy ? 1 : 0;
                                    }
                                    instance.TotalWinningStrategy = instance.TotalWinningStrategyUP + instance.TotalWinningStrategyDOWN;
                                    instance.WinningStrategy = instance.TotalWinningStrategy > 0;
                                }

                                // Serialization

                                if (node.WinningStrategy)
                                {
                                    _strategyBuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb);
                                }
                            }

                            // Update Strategy Vars

                            model.WinningStrategy = instance.TotalWinningStrategy > 0;
                            model.TotalWinningStrategyUp += instance.TotalWinningStrategyUP;
                            model.TotalWinningStrategyDown += instance.TotalWinningStrategyDOWN;
                            model.TotalWinningStrategy = model.TotalWinningStrategyUp + model.TotalWinningStrategyDown;
                        }
                    }
                }

                var completed = StrategyBuilderStatusEnum.Completed.GetMetadata();
                model.Status = completed.Name;
                model.Message = $"{completed.Description}";

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                var error = StrategyBuilderStatusEnum.Error.GetMetadata();
                model.Status = error.Name;
                model.Message = ex.Message;
                return false;
            }
        }

        private void UpdateTreeNodeVM(REPTreeNodeVM node, BacktestModel bkm, bool IsOSample)
        {
            if (!IsOSample)
            {
                node.TotalOpportunityIs = bkm.TotalOpportunity;
                node.TotalTradesIs = bkm.TotalTrades;
                node.WinningTradesIs = bkm.WinningTrades;
                node.LosingTradesIs = bkm.LosingTrades;

                node.PercentSuccessIs = bkm.PercentSuccess;
                node.ProgressivenessIs = bkm.Progressiveness;
            }
            else
            {
                node.TotalOpportunityOs = bkm.TotalOpportunity;
                node.TotalTradesOs = bkm.TotalTrades;
                node.WinningTradesOs = bkm.WinningTrades;
                node.LosingTradesOs = bkm.LosingTrades;

                node.PercentSuccessOs = bkm.PercentSuccess;
                node.ProgressivenessOs = bkm.Progressiveness;
            }
        }

        private void AdjustConfigurationBuilder(List<REPTreeNodeVM> nodes, AutoAdjustConfigModel autoAdjustConfig)
        {
            try
            {
                int TargetUp = Configuration.WinningStrategyTotalUP > WinningStrategyTotalUP ?
                    Configuration.WinningStrategyTotalUP - WinningStrategyTotalUP : 0;

                int TargetDown = Configuration.WinningStrategyTotalDOWN > WinningStrategyTotalDOWN ?
                    Configuration.WinningStrategyTotalDOWN - WinningStrategyTotalDOWN : 0;

                List<REPTreeNodeVM> filteredNodes = nodes.Where(
                    n => n.PercentSuccessIs >= (double)autoAdjustConfig.MinPercentSuccessIS &&
                         n.PercentSuccessOs >= (double)autoAdjustConfig.MinPercentSuccessOS &&
                         n.VariationPercent <= (double)autoAdjustConfig.VariationTransaction &&
                         (!Configuration.IsProgressiveness || n.Progressiveness <= (double)autoAdjustConfig.Progressiveness)
                ).ToList().OrderByDescending(n => n.WinningTradesIs).ThenByDescending(n => n.PercentSuccessIs).ToList();

                List<REPTreeNodeVM> nodesUp = filteredNodes.Where(n => n.Label.ToLower() == "up").ToList();
                if (nodesUp.Count > 0)
                    nodesUp = nodesUp.GetRange(0, nodesUp.Count > TargetUp ? TargetUp : nodesUp.Count);

                List<REPTreeNodeVM> nodesDown = filteredNodes.Where(n => n.Label.ToLower() == "down").ToList();
                if (nodesDown.Count > 0)
                    nodesDown = nodesDown.GetRange(0, nodesDown.Count > TargetDown ? TargetDown : nodesDown.Count);

                REPTreeNodeVM upLast = nodesUp.LastOrDefault();
                REPTreeNodeVM downLast = nodesDown.LastOrDefault();

                int WinningTradesOs = 0;
                int WinningTradesIs = 0;
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
                int lookingForTargetUp = Configuration.WinningStrategyTotalUP > WinningStrategyTotalUP ?
                    Configuration.WinningStrategyTotalUP - WinningStrategyTotalUP : 0;

                int lookingForTargetDown = Configuration.WinningStrategyTotalDOWN > WinningStrategyTotalDOWN ?
                    Configuration.WinningStrategyTotalDOWN - WinningStrategyTotalDOWN : 0;

                int ntotalesUp = 0;
                int ntotalesDown = 0;

                List<REPTreeNodeVM> nodesGraterThanRationMax = nodes.Where(n => !n.Winner && n.RatioMax >= (double)autoAdjustConfig.MaxRatioTree).ToList();
                List<REPTreeNodeVM> resultNodes = new();

                if (nodesGraterThanRationMax.Count > 0)
                {
                    // UP

                    List<REPTreeNodeVM> nodesOrderByTotalUpDesc = nodesGraterThanRationMax.Where(n => n.Label.ToLower() == "up").OrderByDescending(n => n.TotalUP).ToList();
                    if (nodesOrderByTotalUpDesc.Count > 0)
                        nodesOrderByTotalUpDesc = nodesOrderByTotalUpDesc.GetRange(0, nodesOrderByTotalUpDesc.Count > lookingForTargetUp ? lookingForTargetUp : nodesOrderByTotalUpDesc.Count - 1);

                    resultNodes.AddRange(nodesOrderByTotalUpDesc);
                    var totalUp = nodesOrderByTotalUpDesc.Count;

                    if (totalUp > 0)
                    {
                        ntotalesUp = (int)(resultNodes.Where(_n => _n.Label.ToLower() == "up").OrderBy(w => w.TotalUP).FirstOrDefault()?.TotalUP ?? 50);
                    }

                    // DOWN

                    List<REPTreeNodeVM> nodesOrderByTotalDownDesc = nodesGraterThanRationMax.Where(n => n.Label.ToLower() == "down").OrderByDescending(n => n.TotalDOWN).ToList();
                    if (nodesOrderByTotalDownDesc.Count > 0)
                        nodesOrderByTotalDownDesc = nodesOrderByTotalDownDesc.GetRange(0, nodesOrderByTotalDownDesc.Count > lookingForTargetDown ? lookingForTargetDown : nodesOrderByTotalDownDesc.Count - 1);
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

                int result = ntotalesUp == 0 ? ntotalesDown : ntotalesUp;

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

        // ReloadBtnCommand

        public ICommand ReloadBtnCommand => new DelegateCommand(() =>
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

        public async void PopulateViewModel(bool realod = false)
        {
            try
            {
                Project = await _projectService.GetProject(ProcessArgs.ProjectId, true);
                Configuration = Project?.ProjectConfigurations.FirstOrDefault();

                if ((WinningStrategyTotalUP == 0 && WinningStrategyTotalDOWN == 0) || realod)
                {
                    StrategyBuilderProcessList = new ObservableCollection<StrategyBuilderProcessModel>();
                    ResetBuilder(true);

                    if (!IsTransactionActive)
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

                void PopulateStrategyBuilderProcessList(string templatePath)
                {
                    string regionName = "WithoutSchedule";
                    int regionType = 0;
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

                    _projectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(fi =>
                    {
                        StrategyBuilderProcessList.Add(new StrategyBuilderProcessModel
                        {
                            Path = fi.FullName,
                            TemplateName = fi.Name,
                            RegionType = regionType,
                            RegionName = regionName,
                            Status = StrategyBuilderStatusEnum.NoStarted.GetMetadata().Name,
                            IsEnabled = false,
                            IsExpanded = false,
                            InstancesList = new ObservableCollection<REPTreeOutputVM>()
                        });
                    });
                }

                CorrelationModel = _strategyBuilderService.Correlation(ProcessArgs.ProjectName, Configuration.MaxPercentCorrelation, EntityTypeEnum.StrategyBuilder);
                IsCorrelationDetail = CorrelationModel != null;
                UpdateWinnerStrategyTotal(CorrelationModel?.ISBacktestUP?.Count ?? 0, CorrelationModel?.ISBacktestDOWN?.Count ?? 0, true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private void AddItemToStrategyBuilderProcessList(string templateName, List<REPTreeOutputVM> modelList)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var model = StrategyBuilderProcessList.FirstOrDefault(m => m.TemplateName == templateName);
                modelList.ForEach(vm =>
                {
                    vm.Message = vm.WinningStrategy == null ? CommonResources.Pending : vm.WinningStrategy.Value ? CommonResources.Winner : CommonResources.Loser;
                    vm.WinningNodes = vm.NodeOutput.Where(n => n.Winner).Count();
                });

                model.WinningTrees = modelList.Where(n => n.WinningNodes > 0).Count();
                model.TotalWinningStrategy = model.TotalWinningStrategyUp = model.TotalWinningStrategyDown = 0;
                model.InstancesList.Clear();
                model.InstancesList.AddRange(modelList);

                if (model.WinningTrees > 0)
                {
                    model.IsEnabled = true;
                }
            });
        }

        private void UpdateWinnerStrategyTotal(int winningStrategyTotalUP, int winningStrategyTotalDOWN, bool isClean = false)
        {
            if (isClean)
            {
                WinningStrategyTotalUP = WinningStrategyTotalDOWN = 0;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                WinningStrategyTotalUP += winningStrategyTotalUP;
                WinningStrategyTotalDOWN += winningStrategyTotalDOWN;
            });
        }

        private void ResetBuilder(bool resetConfigurations = false, bool cleanSerializedObjDirectory = false)
        {
            UpdateWinnerStrategyTotal(0, 0, true);
            foreach (var model in StrategyBuilderProcessList)
            {
                model.Reset();
            }

            if (resetConfigurations)
            {
                Configurations = new ObservableCollection<AutoAdjustConfigModel>
                {
                    Mapper.Map<ConfigurationBaseVM, AutoAdjustConfigModel>(Configuration)
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

        private bool _istransactionActive;
        public bool IsTransactionActive
        {
            get => _istransactionActive;
            set => SetProperty(ref _istransactionActive, value);
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private ProjectConfigurationVM _configuration;
        public ProjectConfigurationVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private int _winningStrategyTotalUP;
        public int WinningStrategyTotalUP
        {
            get => _winningStrategyTotalUP;
            set => SetProperty(ref _winningStrategyTotalUP, value);
        }

        private int _winningStrategyTotalDOWN;
        public int WinningStrategyTotalDOWN
        {
            get => _winningStrategyTotalDOWN;
            set => SetProperty(ref _winningStrategyTotalDOWN, value);
        }

        private int _numberOfTransactionsFound;
        public int NumberOfTransactionsFound
        {
            get => _numberOfTransactionsFound;
            set => SetProperty(ref _numberOfTransactionsFound, value);
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
