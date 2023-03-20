using Adion.FA.Infrastructure.Common.Helpers;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Project.Commands;
using Adion.FA.UI.Station.Project.EventAggregator;
using Adion.FA.UI.Station.Project.Features;
using Adion.FA.UI.Station.Project.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Adion.FA.UI.Station.Project.Model.Configuration;
using System.Linq;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.Infrastructure.I18n.Enums;
using Adion.FA.UI.Station.Infrastructure.Helpers;

namespace Adion.FA.UI.Station.Project.ViewModels
{
    public class ProjectSettingsViewModel : MenuItemViewModel
    {
        #region Services

        private readonly IAppProjectService AppProjectService;

        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Ctor

        public ProjectSettingsViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            
            eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);
            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
        }

        #endregion

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

        #endregion

        #region WithoutSchedulesCommand

        public ICommand WithoutSchedulesCommand => new DelegateCommand(async () =>
        {
            var config = await AppProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);
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

        #endregion

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
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResponseVM result = await AppProjectService.UpdateProjectConfiguration(projectConfiguration);
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
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        #endregion

        #region RestoreConfigurationBtnCommand
        public DelegateCommand RestoreConfigurationBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ResponseVM result = await AppProjectService.RestoreProjectConfiguration(ProcessArgs.ProjectId);
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
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #endregion

        public async void PopulateViewModel(int projectId)
        {
            CurrencyPairs.Clear();
            CurrencyPairs.AddRange(EnumUtil.ToEnumerable<CurrencyPairEnum>(true));

            CurrencyPeriods.Clear();
            CurrencyPeriods.AddRange(EnumUtil.ToEnumerable<CurrencyPeriodEnum>(true));

            CurrencySpreads.Clear();
            CurrencySpreads.AddRange(EnumUtil.ToEnumerable<CurrencySpreadEnum>(true));

            #region MarketDataList

            MarketDataList.Clear();
            MarketDataList.Insert(0, new Metadata
            {
                Id = 0,
                Name = CommonResources.SelectItem
            });
            MarketDataList.AddRange((await AppProjectService.GetAllMarketData()).Select(md => new Metadata
            {
                Id = md.MarketDataId,
                Name = md.Description,
            }));

            #endregion

            ProjectConfiguration = await AppProjectService.GetProjectConfiguration(projectId, true);

            ProjectConfiguration.MarketDataId ??= 0;

            if (!MarketDataList.Any(md => md.Id == projectConfiguration.MarketDataId))
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
                    projectConfiguration.SetError(nameof(projectConfiguration.CurrencyPairId),
                        ShowMessageControl(msg, showMessageInControl));
                    projectConfiguration.SetError(nameof(projectConfiguration.CurrencyPeriodId),
                        ShowMessageControl(msg, showMessageInControl));
                    projectConfiguration.SetError(nameof(projectConfiguration.MarketDataId),
                        ShowMessageControl(msg, showMessageInControl));
                    break;
            }

            if(showDialogWithErrors)
                MessageHelper.ShowMessage(this, CommonResources.ProjectConfiguration, msg);


            #region ShowMessageControl
            string ShowMessageControl(string msg, bool showMessageInControl = false)
            {
                return showMessageInControl ? msg : string.Empty;
            }
            #endregion
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

        private ProjectConfigurationSettingModel projectConfiguration;
        public ProjectConfigurationSettingModel ProjectConfiguration
        {
            get { return projectConfiguration; }
            set { SetProperty(ref projectConfiguration, value); }
        }

        public ObservableCollection<Metadata> CurrencyPairs { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencyPeriods { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencySpreads { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> MarketDataList { get; } = new ObservableCollection<Metadata>();

        #endregion
    }
}
