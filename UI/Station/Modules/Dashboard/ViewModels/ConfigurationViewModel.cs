using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.UI.Station.Module.Dashboard.Services;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly string EntityDescription = EntityTypeEnum.Configuration.GetMetadata().Description;

        private readonly ISettingService _settingService;
        private readonly IMarketDataServiceAgent _marketDataService;

        public ConfigurationViewModel(
            ISettingService settingService,
            IMarketDataServiceAgent marketDataService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _marketDataService = marketDataService;

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectGlobalConfig))
            {
                PopulateViewModel();
            }
        });

        public ICommand WithoutSchedulesCommand => new DelegateCommand(async () =>
        {
            var config = await _settingService.GetConfiguration();
            if (Configuration.WithoutSchedule)
            {
                Configuration.FromTimeInSecondsEurope = _Configuration.ToTimeInSecondsEurope =
                Configuration.FromTimeInSecondsAmerica = _Configuration.ToTimeInSecondsAmerica =
                Configuration.FromTimeInSecondsAsia = _Configuration.ToTimeInSecondsAsia = DateTime.UtcNow;

                Configuration.FromTimeInSecondsEurope = config.FromTimeInSecondsEurope;
                Configuration.ToTimeInSecondsEurope = config.ToTimeInSecondsEurope;

                Configuration.FromTimeInSecondsAmerica = config.FromTimeInSecondsAmerica;
                Configuration.ToTimeInSecondsAmerica = config.ToTimeInSecondsAmerica;

                Configuration.FromTimeInSecondsAsia = config.FromTimeInSecondsAsia;
                Configuration.ToTimeInSecondsAsia = config.ToTimeInSecondsAsia;
            }
        });

        public ICommand SaveBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = Configuration.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityDescription,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;

                // Update
                var result = await _settingService.UpdateConfiguration(Configuration);

                IsTransactionActive = false;

                MessageHelper.ShowMessage(this,
                    EntityDescription,
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
            var symbols = await _marketDataService.GetAllSymbol().ConfigureAwait(true);
            Symbols.AddRange(symbols.Select(symbol => new Metadata
            {
                Id = symbol.SymbolId,
                Name = symbol.Name
            }));

            Timeframes.Clear();
            var timeframes = await _marketDataService.GetAllTimeframe().ConfigureAwait(true);
            Timeframes.AddRange(timeframes.Select(timeframe => new Metadata
            {
                Id = timeframe.TimeframeId,
                Name = timeframe.Name
            }));

            Configuration = await _settingService.GetConfiguration();
        }

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private ConfigurationModel _Configuration;
        public ConfigurationModel Configuration
        {
            get => _Configuration;
            set => SetProperty(ref _Configuration, value);
        }

        public ObservableCollection<Metadata> Symbols { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> Timeframes { get; } = new ObservableCollection<Metadata>();
    }
}