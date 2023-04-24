using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.UI.Station.Module.Dashboard.Services;
using AdionFA.UI.Station.Modules.Trader.Infrastructure;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class UploadHistoricalDataViewModel : ViewModelBase
    {
        private readonly ISettingService _settingService;
        private readonly IMarketDataServiceAgent _marketDataService;

        public UploadHistoricalDataViewModel(
            ISettingService settingService,
            IMarketDataServiceAgent marketDataService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _marketDataService = marketDataService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutUploadHistoricalData))
            {
                PopulateViewModel();
            }
        }

        public DelegateCommand UploadBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;

                var validator = UploadHistoricalData.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.MarketData.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                if (await CreateHistory())
                {
                    var result = await _settingService.CreateHistoricalData(UploadHistoricalData);

                    IsTransactionActive = false;

                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.MarketData.GetMetadata().Description,
                        result ? MessageResources.EntitySaveSuccess : MessageResources.EntityErrorTransaction);

                    PopulateViewModel();

                    if (result)
                    {
                        ContainerLocator.Current.Resolve<IApplicationCommands>().LoadHistoricalData.Execute(null);
                    }
                }
                else
                {
                    IsTransactionActive = false;
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private async Task<bool> CreateHistory()
        {
            try
            {
                var result = CandleHelper.GetHistoryCandles(UploadHistoricalData.FilePathHistoricalData).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(item =>
                    {
                        UploadHistoricalData.HistoricalDataCandles.Add(new HistoricalDataCandleVM
                        {
                            StartDate = item.Date,
                            StartTime = item.Time,
                            Open = item.Open,
                            High = item.High,
                            Low = item.Low,
                            Close = item.Close,
                            Volume = item.Volume
                        });
                    });

                    string marketName = ((MarketEnum)UploadHistoricalData.MarketId).GetMetadata().Name;
                    var symbol = await _marketDataService.GetSymbol(UploadHistoricalData.SymbolId);
                    var timeframe = await _marketDataService.GetTimeframe(UploadHistoricalData.TimeframeId);

                    var candlesOrdered = result.OrderByDescending(candle => candle.Date);
                    var firstCandleDate = candlesOrdered.LastOrDefault().Date;
                    var lastCandleDate = candlesOrdered.FirstOrDefault().Date;

                    UploadHistoricalData.Description =
                        $"{marketName}." +
                        $"{symbol.Name}." +
                        $"{timeframe.Code}." +
                        $"{firstCandleDate:dd-MM-yyyy}." +
                        $"{lastCandleDate:dd-MM-yyyy}";

                    return true;
                }

                MessageHelper.ShowMessage(this,
                EntityTypeEnum.MarketData.GetMetadata().Description,
                MessageResources.MarketDataFileEmpty);

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private async void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                UploadHistoricalData = new UploadHistoricalDataModel
                {
                    HistoricalDataCandles = Array.Empty<HistoricalDataCandleVM>().ToList()
                };
            }

            if (!Markets.Any())
                Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());

            if (!Timeframes.Any())
            {
                var timeframes = await _marketDataService.GetAllTimeframe().ConfigureAwait(true);
                timeframes.ForEach(Timeframes.Add);
            }

            if (!Symbols.Any())
            {
                var symbols = await _marketDataService.GetAllSymbol().ConfigureAwait(true);
                symbols.ForEach(Symbols.Add);
            }
        }

        // Bindable Model

        private bool istransactionActive;
        public bool IsTransactionActive
        {
            get => istransactionActive;
            set => SetProperty(ref istransactionActive, value);
        }

        private UploadHistoricalDataModel _uploadHistoricalData;
        public UploadHistoricalDataModel UploadHistoricalData
        {
            get => _uploadHistoricalData;
            set => SetProperty(ref _uploadHistoricalData, value);
        }

        public ObservableCollection<Metadata> Markets { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<SymbolVM> Symbols { get; } = new ObservableCollection<SymbolVM>();
        public ObservableCollection<TimeframeVM> Timeframes { get; } = new ObservableCollection<TimeframeVM>();
    }
}
