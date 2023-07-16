using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.MarketData;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.EventAggregator;
using AdionFA.UI.ProjectStation.Features;
using AdionFA.UI.ProjectStation.Model.Configuration;
using AdionFA.UI.ProjectStation.Services;
using AutoMapper;
using Ninject;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class ProjectConfigurationViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;

        private readonly IProjectAppService _projectService;
        private readonly IMarketDataAppService _marketDataService;

        private readonly IAppProjectService _appProjectService;
        private readonly IEventAggregator _eventAggregator;

        public ProjectConfigurationViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectService = IoC.Kernel.Get<IProjectAppService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataAppService>();

            _appProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.ProjectConfigurationTrim)
            {
                ProjectConfiguration = _appProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);

                ProjectConfiguration.Symbol = _mapper.Map<SymbolDTO, SymbolVM>(_marketDataService.GetSymbol(ProjectConfiguration.SymbolId));
                ProjectConfiguration.Timeframe = _mapper.Map<TimeframeDTO, TimeframeVM>(_marketDataService.GetTimeframe(ProjectConfiguration.TimeframeId));
            }
        });

        public ICommand WithoutSchedulesCommand => new DelegateCommand(() =>
        {
            var config = _appProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);
            if (ProjectConfiguration.WithoutSchedule)
            {
                ProjectConfiguration.FromTimeInSecondsEurope = _projectConfiguration.ToTimeInSecondsEurope =
                ProjectConfiguration.FromTimeInSecondsAmerica = _projectConfiguration.ToTimeInSecondsAmerica =
                ProjectConfiguration.FromTimeInSecondsAsia = _projectConfiguration.ToTimeInSecondsAsia = DateTime.UtcNow;

                ProjectConfiguration.FromTimeInSecondsEurope = config.FromTimeInSecondsEurope;
                ProjectConfiguration.ToTimeInSecondsEurope = config.ToTimeInSecondsEurope;

                ProjectConfiguration.FromTimeInSecondsAmerica = config.FromTimeInSecondsAmerica;
                ProjectConfiguration.ToTimeInSecondsAmerica = config.ToTimeInSecondsAmerica;

                ProjectConfiguration.FromTimeInSecondsAsia = config.FromTimeInSecondsAsia;
                ProjectConfiguration.ToTimeInSecondsAsia = config.ToTimeInSecondsAsia;
            }
        });

        public ICommand SaveBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                var validator = _projectConfiguration.Validate();
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.StrategyBuilder.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                var response = _appProjectService.UpdateProjectConfiguration(ProjectConfiguration);

                IsTransactionActive = false;

                PropertyValidator(response, true);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);

                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand RestoreConfigurationBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                var result = _projectService.RestoreProjectConfiguration(ProcessArgs.ProjectId);

                if (result?.IsSuccess ?? false)
                {
                    ProjectConfiguration = _appProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);
                }

                IsTransactionActive = false;

                var msg = (result?.IsSuccess ?? false)
                ? Resources.SuccessEntitySave
                : result?.Message ?? Resources.FailEntitySave;

                MessageHelper.ShowMessage(this,
                    Resources.RestoreProjectConfiguration,
                    msg);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public void PropertyValidator(ResponseVM response, bool showDialogWithErrors = false, bool showMessageInControl = false)
        {
            var msg = (response?.IsSuccess ?? false)
                ? Resources.SuccessEntitySave
                : response?.Message ?? Resources.FailEntitySave;

            if (!response.IsSuccess)
            {
                ProjectConfiguration.SetError(nameof(ProjectConfiguration.Symbol.SymbolId), ShowMessageControl(msg, showMessageInControl));
                ProjectConfiguration.SetError(nameof(ProjectConfiguration.Timeframe.TimeframeId), ShowMessageControl(msg, showMessageInControl));
                ProjectConfiguration.SetError(nameof(ProjectConfiguration.HistoricalDataId), ShowMessageControl(msg, showMessageInControl));
            }

            if (showDialogWithErrors)
            {
                MessageHelper.ShowMessage(this,
                    Resources.ProjectConfiguration,
                    msg);

            }

            static string ShowMessageControl(string msg, bool showMessageInControl = false)
            {
                return showMessageInControl
                    ? msg
                    : string.Empty;
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

        private ProjectConfigurationModel _projectConfiguration;
        public ProjectConfigurationModel ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }
    }
}
