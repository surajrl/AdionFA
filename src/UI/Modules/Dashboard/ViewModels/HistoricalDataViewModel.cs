using AdionFA.Domain.Enums;
using AdionFA.Domain.Helpers;
using AdionFA.Domain.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
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
    public class HistoricalDataViewModel : ViewModelBase
    {
        private readonly ISettingService _settingService;
        private readonly IMarketDataServiceAgent _historicalDataService;

        public HistoricalDataViewModel(
            ISettingService settingService,
            IMarketDataServiceAgent historicalDataService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _historicalDataService = historicalDataService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            applicationCommands.LoadHistoricalDataCommand.RegisterCommand(HistoricalDataFilterCommand);
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutHistoricalData))
            {
                PopulateViewModel();
            }
        }

        public ICommand HistoricalDataFilterCommand => new DelegateCommand(LoadHistoricalData);

        private async void PopulateViewModel()
        {
            try
            {
                Symbol ??= new();
                Timeframe ??= new();

                if (!Markets.Any())
                {
                    Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());
                }

                Symbols?.Clear();
                var symbols = await _historicalDataService.GetAllSymbolAsync().ConfigureAwait(true);
                symbols.ToList().ForEach(Symbols.Add);

                Timeframes?.Clear();
                var timeframes = await _historicalDataService.GetAllTimeframeAsync().ConfigureAwait(true);
                timeframes.ToList().ForEach(Timeframes.Add);

                LoadHistoricalData();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private async void LoadHistoricalData()
        {
            try
            {
                IsTransactionActive = true;
                HistoricalDataDetails?.Clear();

                if (Symbol == null || Timeframe == null)
                {
                    return;
                }

                var vm = await _settingService.GetHistoricalData(
                                    MarketId ?? 0,
                                    Symbol.SymbolId,
                                    Timeframe.TimeframeId);

                HistoricalDataDetails = new ObservableCollection<HistoricalDataCandleSettingVM>(vm.HistoricalDataCandleSettings);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
            }
        }

        // Bindable Model

        private bool _istransactionActive;

        public bool IsTransactionActive
        {
            get => _istransactionActive;
            set => SetProperty(ref _istransactionActive, value);
        }

        private int? _marketId;

        public int? MarketId
        {
            get => _marketId;
            set => SetProperty(ref _marketId, value);
        }

        private SymbolVM _symbol;

        public SymbolVM Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        private TimeframeVM _timeframe;

        public TimeframeVM Timeframe
        {
            get => _timeframe;
            set => SetProperty(ref _timeframe, value);
        }

        private ObservableCollection<HistoricalDataCandleSettingVM> _historicalDataDetails;

        public ObservableCollection<HistoricalDataCandleSettingVM> HistoricalDataDetails
        {
            get => _historicalDataDetails;
            set => SetProperty(ref _historicalDataDetails, value);
        }

        public ObservableCollection<Metadata> Markets { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<SymbolVM> Symbols { get; } = new ObservableCollection<SymbolVM>();
        public ObservableCollection<TimeframeVM> Timeframes { get; } = new ObservableCollection<TimeframeVM>();
    }
}