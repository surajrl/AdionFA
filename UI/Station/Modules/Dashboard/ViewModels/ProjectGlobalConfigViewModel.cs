using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Infrastructure.Common.Helpers;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Helpers;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Module.Dashboard.Services;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Module.Dashboard.ViewModels
{
    public class ProjectGlobalConfigViewModel : ViewModelBase
    {
        internal readonly string EntityDescription = EntityTypeEnum.ProjectGlobalConfiguration.GetMetadata().Description;

        #region Services

        public readonly ISettingService SettingService;

        #endregion

        #region Ctor

        public ProjectGlobalConfigViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            SettingService = settingService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        #endregion

        #region Commands

        #region FlyoutCommand

        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectGlobalConfig))
            {
                PopulateViewModel();
            }
        }

        #endregion

        #region WithoutSchedulesCommand

        public ICommand WithoutSchedulesCommand => new DelegateCommand(async () =>
        {
            var config = await SettingService.GetGlobalConfiguration();
            if (globalConfiguration.WithoutSchedule)
            {
                globalConfiguration.FromTimeInSecondsEurope = globalConfiguration.ToTimeInSecondsEurope =
                globalConfiguration.FromTimeInSecondsAmerica = globalConfiguration.ToTimeInSecondsAmerica =
                globalConfiguration.FromTimeInSecondsAsia = globalConfiguration.ToTimeInSecondsAsia = DateTime.UtcNow;

                globalConfiguration.FromTimeInSecondsEurope = config.FromTimeInSecondsEurope;
                globalConfiguration.ToTimeInSecondsEurope = config.ToTimeInSecondsEurope;

                globalConfiguration.FromTimeInSecondsAmerica = config.FromTimeInSecondsAmerica;
                globalConfiguration.ToTimeInSecondsAmerica = config.ToTimeInSecondsAmerica;

                globalConfiguration.FromTimeInSecondsAsia = config.FromTimeInSecondsAsia;
                globalConfiguration.ToTimeInSecondsAsia = config.ToTimeInSecondsAsia;
            }
        });

        #endregion

        #region SaveBtnCommand

        public ICommand SaveBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = globalConfiguration.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this, EntityDescription, validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;

                //update
                var result = await SettingService.UpdateGlobalConfiguration(globalConfiguration);

                IsTransactionActive = false;

                MessageHelper.ShowMessage(this, EntityDescription, 
                        result ? MessageResources.EntitySaveSuccess : MessageResources.EntityErrorTransaction);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        },() => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        #endregion

        #endregion

        public async void PopulateViewModel()
        {
            CurrencyPairs.Clear();
            CurrencyPairs.AddRange(EnumUtil.ToEnumerable<CurrencyPairEnum>(true));

            CurrencyPeriods.Clear();
            CurrencyPeriods.AddRange(EnumUtil.ToEnumerable<CurrencyPeriodEnum>(true));

            CurrencySpreads.Clear();
            CurrencySpreads.AddRange(EnumUtil.ToEnumerable<CurrencySpreadEnum>(true));

            GlobalConfiguration = await SettingService.GetGlobalConfiguration();
        }

        #region Bindable Model

        bool istransactionActive;
        public bool IsTransactionActive
        {
            get => istransactionActive; 
            set => SetProperty(ref istransactionActive, value);
        }

        ProjectGlobalConfigModel globalConfiguration;
        public ProjectGlobalConfigModel GlobalConfiguration
        {
            get => globalConfiguration;
            set => SetProperty(ref globalConfiguration, value);
        }

        public ObservableCollection<Metadata> CurrencyPairs { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencyPeriods { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> CurrencySpreads { get; } = new ObservableCollection<Metadata>();

        #endregion
    }
}
