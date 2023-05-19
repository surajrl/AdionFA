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
using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.UI.Station.Project.Validators.AssembledBuilder;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
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
        public readonly IMapper Mapper;

        private readonly IAssembledBuilderService _assembledBuilderService;
        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _historicalDataService;
        private readonly IEventAggregator _eventAggregator;

        private ProjectVM Project;
        private AssembledBuilderModel AssembledBuilderModel;

        public AssembledBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
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

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                // Validator

                if (!Validate(new AssembledBuilderValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                // Historical Data

                var projectHistoricalData = await _historicalDataService.GetHistoricalData(Configuration.HistoricalDataId.Value, true);

                IEnumerable<Candle> candles = projectHistoricalData.HistoricalDataCandles.Select(
                        h => new Candle
                        {
                            Date = h.StartDate,
                            Time = h.StartTime,
                            Open = h.Open,
                            High = h.High,
                            Low = h.Low,
                            Close = h.Close,
                            Volume = h.Volume
                        }
                    ).OrderBy(d => d.Date).ThenBy(d => d.Time).ToList();

                await Task.Factory.StartNew(() =>
                {
                    var config = Mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(Configuration);

                    // Extractor

                    _assembledBuilderService.ExtractorExecute(ProcessArgs.ProjectName, AssembledBuilderModel, candles, config);

                    // Strategy

                    _assembledBuilderService.Build(ProcessArgs.ProjectName, config, candles);
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

        public async void PopulateViewModel()
        {
            try
            {
                Project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
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

        public NodeAssembledBindableModel MapToTreeObservableNode(
            NodeAssembledModel source,
            bool isAllExpanded = false,
            bool isStartExpanded = false,
            bool isBacktestExpanded = false)
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

        private AssembledBuilderBindableModel _model;

        public AssembledBuilderBindableModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
    }
}