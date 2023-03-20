using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.UI.Station.Project.AutoMapper;
using Adion.FA.UI.Station.Project.Commands;
using Adion.FA.UI.Station.Project.EventAggregator;
using Adion.FA.UI.Station.Project.Features;
using Adion.FA.UI.Station.Project.Services;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.UI.Station.Project.Model.AssembledBuilder;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Contracts;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using Adion.FA.Infrastructure.Common.Logger.Helpers;
using Adion.FA.UI.Station.Project.Validators.AssembledBuilder;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.Infrastructure.Common.Extractor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Adion.FA.TransferObject.Project;
using Adion.FA.UI.Station.Infrastructure.Helpers;

namespace Adion.FA.UI.Station.Project.ViewModels
{
    public class AssembledBuilderViewModel : MenuItemViewModel
    {
        #region AutoMapper

        public readonly IMapper Mapper;

        #endregion

        #region Services

        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IAssembledBuilderService AssembledBuilderService;
        private readonly IAppProjectService AppProjectService;
        private readonly IMarketDataServiceAgent MarketDataService;
        private readonly IEventAggregator eventAggregator;

        #endregion

        private ProjectVM Project;
        private AssembledBuilderModel AssembledBuilderModel;

        #region Ctor

        public AssembledBuilderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            AssembledBuilderService = IoC.Get<IAssembledBuilderService>();

            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            MarketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            
            eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);
            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            Model = new AssembledBuilderBindableModel();
        }

        #endregion

        #region Command

        #region SelectItemHamburgerMenuCommand
        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            try
            {
                if (item == HamburgerMenuItems.AssembledBuilder.Replace(" ", string.Empty))
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

                if (!Validate(new AssembledBuilderValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                #endregion

                IsTransactionActive = true;
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                #region Market Data

                MarketDataVM projectMarketData = await MarketDataService.GetMarketData(Configuration.MarketDataId.Value, true);

                IEnumerable<Candle> candles = projectMarketData.MarketDataDetails.Select(
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

                await Task.Factory.StartNew(() =>
                {
                    var config = Mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(Configuration);

                    #region Extractor

                    AssembledBuilderService.ExtractorExecute(ProcessArgs.ProjectName, AssembledBuilderModel, candles, config);
                    
                    #endregion

                    #region Strategy

                    AssembledBuilderService.Build(ProcessArgs.ProjectName, config, candles);

                    #endregion

                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);

                IsTransactionActive = false;
                bool result = true;

                #region Msg

                string msg = result ? MessageResources.AssembledBuilderCompleted : MessageResources.EntityErrorTransaction;

                MessageHelper.ShowMessage(this, CommonResources.AssembledBuilder, msg);

                #endregion
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #region ReloadBtnCommand
        public DelegateCommand ReloadBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                PopulateViewModel();
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #region Tree Collapse/Expand All
        public ICommand TreeCollapseExpandAllBtnCommand => new DelegateCommand<string>(label =>
        {
            try
            {
                if (Model != null && !string.IsNullOrEmpty(label?.Trim()))
                {
                    if(label == "up")
                        Model.ChangeExpandedAll(Model.UPNodes);
                    if (label == "down")
                        Model.ChangeExpandedAll(Model.DOWNNodes);
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        }, (item) => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #endregion

        public async void PopulateViewModel()
        {
            try
            {
                Project = await AppProjectService.GetProject(ProcessArgs.ProjectId, true);
                Configuration = Project?.ProjectConfigurations.FirstOrDefault();

                if (!IsTransactionActive)
                {
                    AssembledBuilderModel model = AssembledBuilderService.LoadStrategyModel(ProcessArgs.ProjectName);

                    Model.UPNodes.Clear();
                    Model.UPNodes.Add(MapToTreeObservableNode(model.UPNode, true, isBacktestExpanded: false));

                    Model.DOWNNodes.Clear();
                    Model.DOWNNodes.Add(MapToTreeObservableNode(model.DOWNNode, true, isBacktestExpanded: false));

                    AssembledBuilderModel = model;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
        }

        public NodeAssembledBindableModel MapToTreeObservableNode(NodeAssembledModel source
            , bool isAllExpanded = false, bool isStartExpanded = false, bool isBacktestExpanded = false)
        {
            NodeAssembledBindableModel node = Recursive(source);
            return node;

            NodeAssembledBindableModel Recursive(NodeAssembledModel source, int level = 0)
            {
                NodeAssembledBindableModel node = null;
                if (source != null)
                {
                    if (source is StartNodeAssembledModel)
                    {
                        node = new StartNodeAssembledBindableModel
                        {
                            IsExpanded = isStartExpanded ? isStartExpanded : isAllExpanded,
                            Level = level,
                            Label = source.Label,
                            Name = source.Name,
                            Type = source.Type,
                        };
                    }

                    if (source is EndNodeAssembledModel)
                    {
                        node = new EndNodeAssembledBindableModel
                        {
                            IsExpanded = false,
                            Level = level,
                            Label = source.Label,
                            Name = source.Name,
                            Type = source.Type,
                        };
                    }

                    if (source is BacktestNodeAssembledModel)
                    {
                        node = new BacktestNodeAssembledBindableModel
                        {
                            IsExpanded = isBacktestExpanded ? isBacktestExpanded : isAllExpanded,
                            Level = level,
                            Label = source.Label,
                            Name = source.Name,
                            Type = source.Type,
                        };
                    }

                    level++;
                    int branch = 0;
                    while (branch < source.Nodes.Count)
                    {
                        node?.Nodes.Add(Recursive(source.Nodes[branch], level));
                        branch++;
                    }
                }

                return node;
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


        private AssembledBuilderBindableModel _model;
        public AssembledBuilderBindableModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        #endregion
    }
}
