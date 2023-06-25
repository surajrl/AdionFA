using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Enums;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Configuration;
using AdionFA.UI.Station.Project.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class ProjectConfigurationViewModel : MenuItemViewModel
    {
        private readonly IAppProjectService _appProjectService;
        private readonly IMarketDataServiceAgent _historicalDataService;
        private readonly IProjectServiceAgent _projectService;
        private readonly IEventAggregator _eventAggregator;

        public ProjectConfigurationViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _appProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.ProjectConfiguration.Replace(" ", string.Empty))
            {
                PopulateViewModel(ProcessArgs.ProjectId);
            }
        });

        public ICommand WithoutSchedulesCommand => new DelegateCommand(async () =>
        {
            var config = await _appProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);
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

        public ICommand CrossingBuilderCommand => new DelegateCommand<HistoricalDataVM>(ProjectConfiguration.CrossingHistoricalData.Add);

        public ICommand SaveBtnCommand => new DelegateCommand(async () =>
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

                var result = await _appProjectService.UpdateProjectConfiguration(ProjectConfiguration);

                IsTransactionActive = false;

                PropertyValidator(result, true);
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

        public ICommand RestoreConfigurationBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                var result = await _projectService.RestoreProjectConfigurationAsync(ProcessArgs.ProjectId);
                if (result?.IsSuccess ?? false)
                {
                    PopulateViewModel(ProcessArgs.ProjectId);
                }

                IsTransactionActive = false;

                var msg = (result?.IsSuccess ?? false)
                ? MessageResources.EntitySaveSuccess
                : result?.Message ?? MessageResources.EntityErrorTransaction;

                MessageHelper.ShowMessage(this,
                    CommonResources.ProjectConfigurationRestore,
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

        public async void PopulateViewModel(int projectId)
        {
            ProjectConfiguration = await _appProjectService.GetProjectConfiguration(projectId, true);

            ProjectConfiguration.Timeframe = await _historicalDataService.GetTimeframeAsync(ProjectConfiguration.TimeframeId).ConfigureAwait(true);
            ProjectConfiguration.Symbol = await _historicalDataService.GetSymbolAsync(ProjectConfiguration.SymbolId).ConfigureAwait(true);
        }

        public void PropertyValidator(ResponseVM response, bool showDialogWithErrors = false, bool showMessageInControl = false)
        {
            var msg = (response?.IsSuccess ?? false) ? MessageResources.EntitySaveSuccess
                    : response?.Message ?? MessageResources.EntityErrorTransaction;

            switch (response.MessageResource)
            {
                case (int)MessageResourceEnum.CurrencyPairAndCurrencyPeriodMustBeSame:
                    ProjectConfiguration.SetError(nameof(ProjectConfiguration.Symbol.SymbolId),
                        ShowMessageControl(msg, showMessageInControl));
                    ProjectConfiguration.SetError(nameof(ProjectConfiguration.Timeframe.TimeframeId),
                        ShowMessageControl(msg, showMessageInControl));
                    ProjectConfiguration.SetError(nameof(ProjectConfiguration.HistoricalDataId),
                        ShowMessageControl(msg, showMessageInControl));
                    break;
            }

            if (showDialogWithErrors)
                MessageHelper.ShowMessage(this, CommonResources.ProjectConfiguration, msg);

            string ShowMessageControl(string msg, bool showMessageInControl = false)
            {
                return showMessageInControl ? msg : string.Empty;
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
