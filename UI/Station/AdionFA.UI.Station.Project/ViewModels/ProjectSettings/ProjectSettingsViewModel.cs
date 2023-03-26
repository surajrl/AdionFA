using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using AdionFA.UI.Station.Project.Model.Configuration;
using System.Linq;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.Infrastructure.I18n.Enums;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using DynamicData;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class ProjectSettingsViewModel : MenuItemViewModel
    {
        #region Services

        private readonly IAppProjectService _appProjectService;
        private readonly IHistoricalDataServiceAgent _historicalDataService;
        private readonly IProjectServiceAgent _projectService;
        private readonly IEventAggregator _eventAggregator;

        #endregion Services

        #region Ctor

        public ProjectSettingsViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _appProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            _historicalDataService = ContainerLocator.Current.Resolve<IHistoricalDataServiceAgent>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>()
                .SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
        }

        #endregion Ctor

        #region Commands

        #region SelectItemHamburgerMenuCommand

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            try
            {
                if (item == HamburgerMenuItems.Settings)
                {
                    PopulateViewModel(ProcessArgs.ProjectId);
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

        #region WithoutSchedulesCommand

        public ICommand WithoutSchedulesCommand => new DelegateCommand(async () =>
        {
            var config = await _appProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);
            if (projectConfiguration.WithoutSchedule)
            {
                projectConfiguration.FromTimeInSecondsEurope = projectConfiguration.ToTimeInSecondsEurope =
                projectConfiguration.FromTimeInSecondsAmerica = projectConfiguration.ToTimeInSecondsAmerica =
                projectConfiguration.FromTimeInSecondsAsia = projectConfiguration.ToTimeInSecondsAsia = DateTime.UtcNow;

                projectConfiguration.FromTimeInSecondsEurope = config.FromTimeInSecondsEurope;
                projectConfiguration.ToTimeInSecondsEurope = config.ToTimeInSecondsEurope;

                projectConfiguration.FromTimeInSecondsAmerica = config.FromTimeInSecondsAmerica;
                projectConfiguration.ToTimeInSecondsAmerica = config.ToTimeInSecondsAmerica;

                projectConfiguration.FromTimeInSecondsAsia = config.FromTimeInSecondsAsia;
                projectConfiguration.ToTimeInSecondsAsia = config.ToTimeInSecondsAsia;
            }
        });

        #endregion WithoutSchedulesCommand

        #region SaveBtnCommand

        public DelegateCommand SaveBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = projectConfiguration.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.StrategyBuilder.GetMetadata().Description,
                            validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResponseVM result = await _appProjectService.UpdateProjectConfiguration(projectConfiguration);
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
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        #endregion SaveBtnCommand

        #region RestoreConfigurationBtnCommand

        public DelegateCommand RestoreConfigurationBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResponseVM result = await _projectService.RestoreProjectConfiguration(ProcessArgs.ProjectId);
                if (result?.IsSuccess ?? false)
                {
                    PopulateViewModel(ProcessArgs.ProjectId);
                }
                IsTransactionActive = false;

                string msg = (result?.IsSuccess ?? false) ? MessageResources.EntitySaveSuccess
                    : result?.Message ?? MessageResources.EntityErrorTransaction;

                MessageHelper.ShowMessage(this, CommonResources.ProjectConfigurationRestore, msg);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        #endregion RestoreConfigurationBtnCommand

        #endregion Commands

        public async void PopulateViewModel(int projectId)
        {
            Symbols.Clear();
            var symbols = await _historicalDataService.GetAllSymbol().ConfigureAwait(true);
            Symbols.AddRange(symbols.Select(symbol => new Metadata
            {
                Id = symbol.SymbolId,
                Name = symbol.Name
            }));

            Timeframes.Clear();
            var timeframes = await _historicalDataService.GetAllTimeframe().ConfigureAwait(true);
            Timeframes.AddRange(timeframes.Select(timeframe => new Metadata
            {
                Id = timeframe.TimeframeId,
                Name = timeframe.Name
            }));

            CurrencySpreads.Clear();
            CurrencySpreads.AddRange(EnumUtil.ToEnumerable<CurrencySpreadEnum>(true));

            #region MarketDataList

            HistoricalDataList.Clear();
            HistoricalDataList.Insert(0, new Metadata
            {
                Id = 0,
                Name = CommonResources.SelectItem
            });
            HistoricalDataList.AddRange((await _historicalDataService.GetAllHistoricalData()).Select(md => new Metadata
            {
                Id = md.HistoricalDataId,
                Name = md.Description,
            }));

            #endregion MarketDataList

            ProjectConfiguration = await _appProjectService.GetProjectConfiguration(projectId, true);

            ProjectConfiguration.HistoricalDataId ??= 0;

            if (!HistoricalDataList.Any(md => md.Id == projectConfiguration.HistoricalDataId))
            {
                MessageHelper.ShowMessage(this, "Info", "The project is associated with outdated historical data, consider updating the Market Data field");
            }
        }

        public void PropertyValidator(ResponseVM response, bool showDialogWithErrors = false, bool showMessageInControl = false)
        {
            string msg = (response?.IsSuccess ?? false) ? MessageResources.EntitySaveSuccess
                    : response?.Message ?? MessageResources.EntityErrorTransaction;

            switch (response.MessageResource)
            {
                case (int)MessageResourceEnum.CurrencyPairAndCurrencyPeriodMustBeSame:
                    projectConfiguration.SetError(nameof(projectConfiguration.Symbol.SymbolId),
                        ShowMessageControl(msg, showMessageInControl));
                    projectConfiguration.SetError(nameof(projectConfiguration.Timeframe.TimeframeId),
                        ShowMessageControl(msg, showMessageInControl));
                    projectConfiguration.SetError(nameof(projectConfiguration.HistoricalDataId),
                        ShowMessageControl(msg, showMessageInControl));
                    break;
            }

            if (showDialogWithErrors)
                MessageHelper.ShowMessage(this, CommonResources.ProjectConfiguration, msg);

            #region ShowMessageControl

            string ShowMessageControl(string msg, bool showMessageInControl = false)
            {
                return showMessageInControl ? msg : string.Empty;
            }

            #endregion ShowMessageControl
        }

        #region Bindable Model

        private bool istransactionActive;

        public bool IsTransactionActive
        {
            get => istransactionActive;
            set => SetProperty(ref istransactionActive, value);
        }

        private bool canExecute = true;

        public bool CanExecute
        {
            get => canExecute;
            set => SetProperty(ref canExecute, value);
        }

        private ProjectSettingsModel projectConfiguration;

        public ProjectSettingsModel ProjectConfiguration
        {
            get => projectConfiguration;
            set => SetProperty(ref projectConfiguration, value);
        }

        public ObservableCollection<Metadata> Symbols { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> Timeframes { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencySpreads { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> HistoricalDataList { get; } = new ObservableCollection<Metadata>();

        #endregion Bindable Model
    }
}
