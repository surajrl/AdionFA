using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Services;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.UI.Station.Project.Model.AssembledBuilder;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.UI.Station.Project.Validators.AssembledBuilder;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.Infrastructure.Common.Extractor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Station.Infrastructure.Helpers;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class AssembledBuilderViewModel : MenuItemViewModel
    {
        #region AutoMapper

        public readonly IMapper Mapper;

        #endregion AutoMapper

        #region Services

        private readonly IAssembledBuilderService _assembledBuilderService;
        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _historicalDataService;
        private readonly IEventAggregator _eventAggregator;

        #endregion Services

        private ProjectVM Project;
        private AssembledBuilderModel AssembledBuilderModel;

        #region Ctor

        public AssembledBuilderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _assembledBuilderService = IoC.Get<IAssembledBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>()
                .SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            Model = new AssembledBuilderBindableModel();
        }

        #endregion Ctor

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

        #endregion SelectItemHamburgerMenuCommand

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

                #endregion Validator

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                #region Market Data

                HistoricalDataVM projectMarketData = await _historicalDataService.GetHistoricalData(Configuration.HistoricalDataId.Value, true);

                IEnumerable<Candle> candles = projectMarketData.HistoricalDataDetails.Select(
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
                    ).OrderBy(d => d.Date).ThenBy(d => d.Time).ToList();

                #endregion Market Data

                await Task.Factory.StartNew(() =>
                {
                    var config = Mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(Configuration);

                    #region Extractor

                    _assembledBuilderService.ExtractorExecute(ProcessArgs.ProjectName, AssembledBuilderModel, candles, config);

                    #endregion Extractor

                    #region Strategy

                    _assembledBuilderService.Build(ProcessArgs.ProjectName, config, candles);

                    #endregion Strategy
                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);

                IsTransactionActive = false;
                bool result = true;

                #region Msg

                string msg = result ? MessageResources.AssembledBuilderCompleted : MessageResources.EntityErrorTransaction;

                MessageHelper.ShowMessage(this, CommonResources.AssembledBuilder, msg);

                #endregion Msg
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        #endregion ProcessBtnCommand

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

        #endregion ReloadBtnCommand

        #region Tree Collapse/Expand All

        public ICommand TreeCollapseExpandAllBtnCommand => new DelegateCommand<string>(label =>
        {
            try
            {
                if (Model != null && !string.IsNullOrEmpty(label?.Trim()))
                {
                    if (label == "up")
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

        #endregion Tree Collapse/Expand All

        #endregion Command

        public async void PopulateViewModel()
        {
            try
            {
                Project = await _projectService.GetProject(ProcessArgs.ProjectId, true);
                Configuration = Project?.ProjectConfigurations.FirstOrDefault();

                if (!IsTransactionActive)
                {
                    AssembledBuilderModel model = _assembledBuilderService.LoadStrategyModel(ProcessArgs.ProjectName);

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

        #endregion Bindable Model
    }
}
