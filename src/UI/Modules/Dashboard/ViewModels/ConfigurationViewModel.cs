using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Model;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.Module.Dashboard.Model;
using AdionFA.UI.Module.Dashboard.Services;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly string EntityDescription = EntityTypeEnum.Configuration.GetMetadata().Description;

        private readonly ISettingService _settingService;
        private readonly IMarketDataAppService _marketDataService;

        public ConfigurationViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _marketDataService = IoC.Kernel.Get<IMarketDataAppService>();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutConfiguration))
            {
                PopulateViewModel();
            }
        });

        public ICommand WithoutSchedulesCommand => new DelegateCommand(() =>
        {
            var config = _settingService.GetConfiguration();
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

        public ICommand SaveBtnCommand => new DelegateCommand(() =>
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

                var result = _settingService.UpdateConfiguration(Configuration);

                IsTransactionActive = false;

                MessageHelper.ShowMessage(this,
                    EntityDescription,
                    result
                    ? Resources.SuccessEntitySave
                    : Resources.FailEntitySave);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);

                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public void PopulateViewModel()
        {
            Symbols.Clear();
            var symbols = _marketDataService.GetAllSymbol();
            Symbols.AddRange(symbols.Select(symbol => new Metadata
            {
                Id = symbol.SymbolId,
                Name = symbol.Name
            }));

            Timeframes.Clear();
            var timeframes = _marketDataService.GetAllTimeframe();
            Timeframes.AddRange(timeframes.Select(timeframe => new Metadata
            {
                Id = timeframe.TimeframeId,
                Name = timeframe.Name
            }));

            Configuration = _settingService.GetConfiguration();
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