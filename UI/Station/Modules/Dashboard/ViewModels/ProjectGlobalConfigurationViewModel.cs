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
    public class ProjectGlobalConfigurationViewModel : ViewModelBase
    {
        internal readonly string EntityDescription = EntityTypeEnum.ProjectGlobalConfiguration.GetMetadata().Description;

        private readonly ISettingService _settingService;
        private readonly IMarketDataServiceAgent _marketDataService;

        public ProjectGlobalConfigurationViewModel(
            ISettingService settingService,
            IMarketDataServiceAgent historicalDataService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _marketDataService = historicalDataService;

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
            if (ProjectGlobalConfiguration.WithoutSchedule)
            {
                ProjectGlobalConfiguration.FromTimeInSecondsEurope = _projectGlobalConfiguration.ToTimeInSecondsEurope =
                ProjectGlobalConfiguration.FromTimeInSecondsAmerica = _projectGlobalConfiguration.ToTimeInSecondsAmerica =
                ProjectGlobalConfiguration.FromTimeInSecondsAsia = _projectGlobalConfiguration.ToTimeInSecondsAsia = DateTime.UtcNow;

                ProjectGlobalConfiguration.FromTimeInSecondsEurope = config.FromTimeInSecondsEurope;
                ProjectGlobalConfiguration.ToTimeInSecondsEurope = config.ToTimeInSecondsEurope;

                ProjectGlobalConfiguration.FromTimeInSecondsAmerica = config.FromTimeInSecondsAmerica;
                ProjectGlobalConfiguration.ToTimeInSecondsAmerica = config.ToTimeInSecondsAmerica;

                ProjectGlobalConfiguration.FromTimeInSecondsAsia = config.FromTimeInSecondsAsia;
                ProjectGlobalConfiguration.ToTimeInSecondsAsia = config.ToTimeInSecondsAsia;
            }
        });

        public ICommand SaveBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = ProjectGlobalConfiguration.Validate();
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
                var result = await _settingService.UpdateGlobalConfiguration(ProjectGlobalConfiguration);

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

            ProjectGlobalConfiguration = await _settingService.GetGlobalConfiguration();
        }

        // View Bindings

        private bool _isTransactionActive;

        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private ProjectGlobalConfigurationModel _projectGlobalConfiguration;
        public ProjectGlobalConfigurationModel ProjectGlobalConfiguration
        {
            get => _projectGlobalConfiguration;
            set => SetProperty(ref _projectGlobalConfiguration, value);
        }

        public ObservableCollection<Metadata> Symbols { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<Metadata> Timeframes { get; } = new ObservableCollection<Metadata>();
    }
}