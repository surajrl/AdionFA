using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Directories.Services;
using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Project.AutoMapper;
using Adion.FA.UI.Station.Project.Commands;
using Adion.FA.UI.Station.Project.EventAggregator;
using Adion.FA.UI.Station.Project.Features;
using Adion.FA.UI.Station.Project.Model.StrategyBuilder;
using Adion.FA.UI.Station.Project.Services;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.UI.Station.Project.Validators.StrategyBuilder;
using System.Threading;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.Infrastructure.Common.Weka.Services;
using Adion.FA.Infrastructure.Common.Weka.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Contracts;
using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using Adion.FA.TransferObject.Base;
using Adion.FA.Infrastructure.Common.Logger.Helpers;
using System.IO;
using Adion.FA.UI.Station.Infrastructure.Helpers;

namespace Adion.FA.UI.Station.Project.ViewModels
{
    public class StrategyBuilderViewModel : MenuItemViewModel
    {
        #region AutoMapper

        public readonly IMapper Mapper;

        #endregion

        #region Services

        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IStrategyBuilderService BuilderService;

        private readonly IAppProjectService AppProjectService;
        private readonly IMarketDataServiceAgent MarketDataService;

        private readonly IEventAggregator eventAggregator;

        #endregion

        private ProjectVM Project;
        
        #region Ctor

        public StrategyBuilderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            BuilderService = IoC.Get<IStrategyBuilderService>();

            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            MarketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);
            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        #endregion

        #region Command

        #region SelectItemHamburgerMenuCommand

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
        
        #endregion

        #region ProcessBtnCommand

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                #region Validator
                if (!Validate(new StrategyBuilderValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }
                #endregion

                IsTransactionActive = true;
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResetBuilder(true, true);

                #region Market Data

                MarketDataVM projectMarketHistoryData = await MarketDataService.GetMarketData(Configuration.MarketDataId.Value, true);
                IEnumerable<Candle> candles = projectMarketHistoryData.MarketDataDetails.Select(
                        h => new Candle
                        {
                            date = h.StartDate,
                            time = h.StartTime,
                            open = h.OpenPrice,
                            max = h.MaxPrice,
                            min = h.MinPrice,
                            close = h.ClosePrice,
                            volumen = h.Volumen
                        }
                    ).OrderBy(d => d.date).ThenBy(d => d.time).ToList();

                #endregion

                #region DataMine

                await Task.Factory.StartNew(() => 
                {
                    int length = Configurations.Count;
                    int idx = 0;
                    while (idx < length)
                    {
                        ResetBuilder(cleanSerializedObjDirectory: true);

                        AutoAdjustConfigModel c = Configurations[idx];

                        #region Async - DataMine
                        if (Configuration.AsynchronousMode)
                        {
                            var pool = AsynchronousMode(c, candles);
                            pool.ForEach(t => t.Start());
                            Task.WaitAll(pool.ToArray());
                        }
                        #endregion

                        #region Sync - DataMine
                        else
                        {
                            var sync = SynchronousMode(c, candles);
                            sync.Start();
                            sync.Wait();
                        }
                        #endregion

                        #region AutoAdjustConfig
                        if (Configuration.AutoAdjustConfig && Configuration.MaxAdjustConfig > length &&
                            (Configuration.WinningStrategyTotalUP > WinningStrategyTotalUP 
                                || Configuration.WinningStrategyTotalDOWN > WinningStrategyTotalDOWN))
                        {
                            List<REPTreeNodeModelVM> nodes = StrategyBuilderProcessList.SelectMany(
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
                        #endregion

                        idx++;
                    }
                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
                
                #endregion

                #region Correlation

                await Task.Run(() =>
                {
                    CorrelationModel = BuilderService.Correlation(ProcessArgs.ProjectName, Configuration.MaxPercentCorrelation, EntityTypeEnum.StrategyBuilder);
                    IsCorrelationDetail = CorrelationModel.Success;
                });
                
                #endregion

                UpdateWinnerVars(CorrelationModel?.BacktestUP?.Count ?? 0, CorrelationModel?.BacktestDOWN?.Count ?? 0, true);

                IsTransactionActive = false;
                bool result = !StrategyBuilderProcessList.Any(e => e.Status != StrategyBuilderStatusEnum.Completed.GetMetadata().Name);

                #region Msg

                string msg = result ? MessageResources.StrategyBuilderCompleted : MessageResources.EntityErrorTransaction;

                if (result && !IsCorrelationDetail)
                {
                    msg = $"{MessageResources.CorrelationRunWithNoResults}";
                }
                
                MessageHelper.ShowMessage(this, CommonResources.StrategyBuilder, msg);
                
                #endregion
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
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
                #region Beginning

                var beginning = StrategyBuilderStatusEnum.Beginning.GetMetadata();
                model.Status = beginning.Name;
                model.Message = $"{beginning.Description}";

                #endregion

                #region Weka

                var weka = StrategyBuilderStatusEnum.ExecutingWeka.GetMetadata();
                model.Status = weka.Name;
                model.Message = $"{weka.Description}";

                var wekaApi = new WekaApiClient();
                IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                    model.Path,
                    config.DepthWeka,
                    config.TotalDecimalWeka,
                    config.MinimalSeed, config.MaximumSeed,
                    config.TotalInstanceWeka,
                    (double)config.MaxRatioTree,
                    (double)config.NTotalTree
                );
                model.Message = $"Pruning Tree";

                #endregion

                if (responseWeka.Any())
                {
                    AddItemToStrategyBuilderProcessList(model.TemplateName, responseWeka.Select(
                        m => Mapper.Map<REPTreeOutputModel, REPTreeOutputModelVM>(m)
                    ).ToList());

                    if (model.InstancesList.Count > 0)
                    {
                        foreach (var instance in model.InstancesList)
                        {
                            List<REPTreeNodeModelVM> winningNodes = instance.NodeOutput.Where(m => m.Winner).ToList();
                            winningNodes.ForEach(m => {
                                m.Node = new ObservableCollection<string>(m.Node.OrderByDescending(n => n).ToList());
                            });

                            foreach (var node in winningNodes)
                            {
                                instance.CounterProgressBar++;

                                node.MarketData = Configuration.MarketDataName;

                                StrategyBuilderModel stb = BuilderService.BacktestBuild(node.Label, node.Node.ToList(),
                                    Mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(config), candles);

                                #region IS
                                UpdateREPTreeNodeModelVM(node, stb.IS, false);
                                #endregion

                                #region OS
                                UpdateREPTreeNodeModelVM(node, stb.OS, true);
                                #endregion

                                #region Strategy
                                node.WinningStrategy = stb.WinningStrategy;

                                if (node.WinningStrategy)
                                {
                                    if (node.Label.ToLower() == "up")
                                    {
                                        instance.TotalWinningStrategyUp += node.WinningStrategy ? 1 : 0;
                                    }
                                    if (node.Label.ToLower() == "down")
                                    {
                                        instance.TotalWinningStrategyDown += node.WinningStrategy ? 1 : 0;
                                    }
                                    instance.TotalWinningStrategy = instance.TotalWinningStrategyUp + instance.TotalWinningStrategyDown;
                                    instance.WinningStrategy = instance.TotalWinningStrategy > 0 ? true : false;
                                }
                                #endregion

                                #region Serialization

                                if (node.WinningStrategy)
                                {
                                    BuilderService.BacktestSerialize(ProcessArgs.ProjectName, stb.IS);
                                }

                                #endregion
                            }

                            #region Update Strategy Vars  
                            
                            model.WinningStrategy = instance.TotalWinningStrategy > 0 ? true : false;
                            model.TotalWinningStrategyUp += instance.TotalWinningStrategyUp;
                            model.TotalWinningStrategyDown += instance.TotalWinningStrategyDown;
                            model.TotalWinningStrategy = model.TotalWinningStrategyUp + model.TotalWinningStrategyDown;

                            #endregion
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

        private void UpdateREPTreeNodeModelVM(REPTreeNodeModelVM node, BacktestModel bkm, bool IsOSample)
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

        private void AdjustConfigurationBuilder(
            List<REPTreeNodeModelVM> nodes, AutoAdjustConfigModel autoAdjustConfig)
        {
            try
            {
                int TargetUp = Configuration.WinningStrategyTotalUP > WinningStrategyTotalUP ? 
                    Configuration.WinningStrategyTotalUP - WinningStrategyTotalUP : 0;

                int TargetDown = Configuration.WinningStrategyTotalDOWN > WinningStrategyTotalDOWN ? 
                    Configuration.WinningStrategyTotalDOWN - WinningStrategyTotalDOWN : 0;

                List<REPTreeNodeModelVM> filteredNodes = nodes.Where(
                    n => n.PercentSuccessIs >= (double)autoAdjustConfig.MinPercentSuccessIS && 
                         n.PercentSuccessOs >= (double)autoAdjustConfig.MinPercentSuccessOS &&
                         n.VariationPercent <= (double)autoAdjustConfig.VariationTransaction &&
                         (!Configuration.IsProgressiveness || n.Progressiveness <= (double)autoAdjustConfig.Progressiveness)
                ).ToList().OrderByDescending(n => n.WinningTradesIs).ThenByDescending(n => n.PercentSuccessIs).ToList();

                List<REPTreeNodeModelVM> nodesUp = filteredNodes.Where(n => n.Label.ToLower() == "up").ToList();
                if(nodesUp.Count > 0)
                    nodesUp = nodesUp.GetRange(0, nodesUp.Count > TargetUp ? TargetUp : nodesUp.Count);
                
                List<REPTreeNodeModelVM> nodesDown = filteredNodes.Where(n => n.Label.ToLower() == "down").ToList();
                if(nodesDown.Count > 0)
                    nodesDown = nodesDown.GetRange(0, nodesDown.Count > TargetDown ? TargetDown : nodesDown.Count);
                
                REPTreeNodeModelVM upLast = nodesUp.LastOrDefault();
                REPTreeNodeModelVM downLast = nodesDown.LastOrDefault();

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

        private void AdjustConfigurationWeka(
            List<REPTreeNodeModelVM> nodes, AutoAdjustConfigModel autoAdjustConfig)
        {
            try
            {
                int lookingForTargetUp = Configuration.WinningStrategyTotalUP > WinningStrategyTotalUP ? 
                    Configuration.WinningStrategyTotalUP - WinningStrategyTotalUP : 0;

                int lookingForTargetDown = Configuration.WinningStrategyTotalDOWN > WinningStrategyTotalDOWN ? 
                    Configuration.WinningStrategyTotalDOWN - WinningStrategyTotalDOWN : 0;

                int ntotalesUp = 0;
                int ntotalesDown = 0;

                List<REPTreeNodeModelVM> nodesGraterThanRationMax = nodes.Where(n => !n.Winner && n.RatioMax >= (double)autoAdjustConfig.MaxRatioTree).ToList();
                List<REPTreeNodeModelVM> resultNodes = new List<REPTreeNodeModelVM>();

                if (nodesGraterThanRationMax.Count > 0)
                {
                    #region Up
                    List<REPTreeNodeModelVM> nodesOrderByTotalUpDesc = nodesGraterThanRationMax.Where(n => n.Label.ToLower() == "up").OrderByDescending(n => n.TotalUP).ToList();
                    if(nodesOrderByTotalUpDesc.Count > 0)
                        nodesOrderByTotalUpDesc = nodesOrderByTotalUpDesc.GetRange(0, nodesOrderByTotalUpDesc.Count > lookingForTargetUp ? lookingForTargetUp : nodesOrderByTotalUpDesc.Count -1);
                    
                    resultNodes.AddRange(nodesOrderByTotalUpDesc);
                    var totalUp = nodesOrderByTotalUpDesc.Count();
                    
                    if (totalUp > 0)
                    { 
                        ntotalesUp = (int)(resultNodes.Where(_n => _n.Label.ToLower() == "up").OrderBy(w => w.TotalUP).FirstOrDefault()?.TotalUP ?? 50);
                    }
                    #endregion

                    #region Down
                    List<REPTreeNodeModelVM> nodesOrderByTotalDownDesc = nodesGraterThanRationMax.Where(n => n.Label.ToLower() == "down").OrderByDescending(n => n.TotalDOWN).ToList();
                    if(nodesOrderByTotalDownDesc.Count > 0)
                        nodesOrderByTotalDownDesc = nodesOrderByTotalDownDesc.GetRange(0, nodesOrderByTotalDownDesc.Count > lookingForTargetDown ? lookingForTargetDown : nodesOrderByTotalDownDesc.Count -1);
                    resultNodes.AddRange(nodesOrderByTotalDownDesc);
                    var totalDown = nodesOrderByTotalDownDesc.Count();
                    
                    if (totalDown > 0)
                    { 
                        ntotalesDown = (int)(resultNodes.Where(_n => _n.Label.ToLower() == "down").OrderBy(w => w.TotalDOWN).FirstOrDefault()?.TotalDOWN ?? 50);
                    }
                    #endregion
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
        
        #endregion

        #region ReloadBtnCommand

        public DelegateCommand ReloadBtnCommand => new DelegateCommand(() => 
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
        
        #endregion

        #endregion

        public async void PopulateViewModel(bool realod = false)
        {
            try
            {
                Project = await AppProjectService.GetProject(ProcessArgs.ProjectId, true);
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

                    ProjectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(fi =>
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
                            InstancesList = new ObservableCollection<REPTreeOutputModelVM>()
                        });
                    });
                }

                CorrelationModel = BuilderService.Correlation(ProcessArgs.ProjectName, Configuration.MaxPercentCorrelation, EntityTypeEnum.StrategyBuilder);
                IsCorrelationDetail = CorrelationModel != null;
                UpdateWinnerVars(CorrelationModel?.BacktestUP?.Count ?? 0, CorrelationModel?.BacktestDOWN?.Count ?? 0, true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private void AddItemToStrategyBuilderProcessList(string templateName, List<REPTreeOutputModelVM> modelList)
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

                if(model.WinningTrees > 0)
                {
                    model.IsEnabled = true;
                }
            });
        }

        private void UpdateWinnerVars(int winningStrategyTotalUP, int winningStrategyTotalDOWN, bool isClean = false)
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
            UpdateWinnerVars(0, 0, true);
            foreach (var model in StrategyBuilderProcessList)
            {
                model.Reset();
            }

            if (resetConfigurations)
            {
                Configurations = new ObservableCollection<AutoAdjustConfigModel>();
                Configurations.Add(Mapper.Map<ConfigurationBaseVM, AutoAdjustConfigModel>(Configuration));
                CurrentConfiguration = 1;
            }

            if (cleanSerializedObjDirectory)
            {
                ProjectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory(), "*.xml", SearchOption.AllDirectories, true);
                IsCorrelationDetail = false;
            }
        }

        #region Bindable Model
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


        private int winningStrategyTotalUP;
        public int WinningStrategyTotalUP 
        {
            get => winningStrategyTotalUP;
            set => SetProperty(ref winningStrategyTotalUP, value);
        }
        

        private int winningStrategyTotalDOWN;
        public int WinningStrategyTotalDOWN
        {
            get => winningStrategyTotalDOWN;
            set => SetProperty(ref winningStrategyTotalDOWN, value);
        }


        private int numberOfTransactionsFound;
        public int NumberOfTransactionsFound
        {
            get => numberOfTransactionsFound;
            set => SetProperty(ref numberOfTransactionsFound, value);
        }


        private bool isCorrelationDetail;
        public bool IsCorrelationDetail
        {
            get => isCorrelationDetail;
            set => SetProperty(ref isCorrelationDetail, value);
        }

        private CorrelationModel correlationModel;
        public CorrelationModel CorrelationModel 
        { 
            get => correlationModel;
            set => SetProperty(ref correlationModel, value);
        }

        private int currentConfiguration;
        public int CurrentConfiguration
        {
            get => currentConfiguration; 
            set => SetProperty(ref currentConfiguration, value);
        }


        private ObservableCollection<AutoAdjustConfigModel> configurations;
        public ObservableCollection<AutoAdjustConfigModel> Configurations
        {
            get => configurations; 
            set => SetProperty(ref configurations, value);
        }


        private ObservableCollection<StrategyBuilderProcessModel> strategyBuilderProcessList;
        public ObservableCollection<StrategyBuilderProcessModel> StrategyBuilderProcessList
        {
            get => strategyBuilderProcessList;
            set => SetProperty(ref strategyBuilderProcessList, value);
        }
        #endregion
    }
}
