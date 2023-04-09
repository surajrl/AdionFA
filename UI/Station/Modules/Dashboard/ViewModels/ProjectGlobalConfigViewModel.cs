using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Modules.Trader.Infrastructure;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class ProjectGlobalConfigViewModel : ViewModelBase
    {
        internal readonly string EntityDescription = EntityTypeEnum.ProjectGlobalConfiguration.GetMetadata().Description;

        private readonly ISettingService _settingService;
        private readonly IMarketDataServiceAgent _historicalDataService;

        public ProjectGlobalConfigViewModel(
            ISettingService settingService,
            IMarketDataServiceAgent historicalDataService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _historicalDataService = historicalDataService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectGlobalConfig))
            {
                PopulateViewModel();
            }
        }

        public ICommand WithoutSchedulesCommand => new DelegateCommand(async () =>
        {
            var config = await _settingService.GetGlobalConfiguration();
            if (_projectGlobalConfiguration.WithoutSchedule)
            {
                _projectGlobalConfiguration.FromTimeInSecondsEurope = _projectGlobalConfiguration.ToTimeInSecondsEurope =
                _projectGlobalConfiguration.FromTimeInSecondsAmerica = _projectGlobalConfiguration.ToTimeInSecondsAmerica =
                _projectGlobalConfiguration.FromTimeInSecondsAsia = _projectGlobalConfiguration.ToTimeInSecondsAsia = DateTime.UtcNow;

                _projectGlobalConfiguration.FromTimeInSecondsEurope = config.FromTimeInSecondsEurope;
                _projectGlobalConfiguration.ToTimeInSecondsEurope = config.ToTimeInSecondsEurope;

                _projectGlobalConfiguration.FromTimeInSecondsAmerica = config.FromTimeInSecondsAmerica;
                _projectGlobalConfiguration.ToTimeInSecondsAmerica = config.ToTimeInSecondsAmerica;

                _projectGlobalConfiguration.FromTimeInSecondsAsia = config.FromTimeInSecondsAsia;
                _projectGlobalConfiguration.ToTimeInSecondsAsia = config.ToTimeInSecondsAsia;
            }
        });

        public ICommand SaveBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                _projectGlobalConfiguration.SymbolId = _projectGlobalConfiguration.Symbol.SymbolId;
                _projectGlobalConfiguration.TimeframeId = _projectGlobalConfiguration.Timeframe.TimeframeId;

                var validator = _projectGlobalConfiguration.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this, EntityDescription, validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;

                // Update
                var result = await _settingService.UpdateGlobalConfiguration(_projectGlobalConfiguration);

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
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            Symbols.Clear();
            var symbols = await _historicalDataService.GetAllSymbol();
            symbols.ForEach(Symbols.Add);

            Timeframes.Clear();
            var timeframes = await _historicalDataService.GetAllTimeframe();
            timeframes.ForEach(Timeframes.Add);

            CurrencySpreads.Clear();
            CurrencySpreads.AddRange(EnumUtil.ToEnumerable<CurrencySpreadEnum>(true));

            ProjectGlobalConfiguration = await _settingService.GetGlobalConfiguration();
        }

        #region Bindable Model

        private bool istransactionActive;

        public bool IsTransactionActive
        {
            get => istransactionActive;
            set => SetProperty(ref istransactionActive, value);
        }

        private ProjectGlobalConfigModel _projectGlobalConfiguration;

        public ProjectGlobalConfigModel ProjectGlobalConfiguration
        {
            get => _projectGlobalConfiguration;
            set => SetProperty(ref _projectGlobalConfiguration, value);
        }

        public ObservableCollection<SymbolVM> Symbols { get; } = new ObservableCollection<SymbolVM>();
        public ObservableCollection<TimeframeVM> Timeframes { get; } = new ObservableCollection<TimeframeVM>();
        public ObservableCollection<Metadata> CurrencySpreads { get; } = new ObservableCollection<Metadata>();

        #endregion Bindable Model
    }
}
