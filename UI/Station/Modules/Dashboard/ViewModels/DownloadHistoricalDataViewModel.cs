using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Infrastructure.EventAggregator;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.UI.Station.Modules.Trader.Infrastructure;
using Prism.Ioc;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using System.Collections.Generic;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.I18n.Resources;
using System.Diagnostics;
using AdionFA.UI.Station.Module.Dashboard.Services;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using NetMQ.Sockets;
using NetMQ;
using Newtonsoft.Json;
using AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Model;
using DynamicData;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class DownloadHistoricalDataViewModel : ViewModelBase
    {
        private readonly ISharedServiceAgent _sharedService;
        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly ISettingService _settingService;

        private ICommand FlyoutCommand { get; }
        public ICommand DownloadBtnCommand { get; }
        public ICommand RefreshBtnCommand { get; }

        public DownloadHistoricalDataViewModel(
            IApplicationCommands applicationCommands,
            ISettingService settingService,
            IMarketDataServiceAgent marketDataService,
            ISharedServiceAgent sharedService)
        {
            _sharedService = sharedService;
            _settingService = settingService;
            _marketDataService = marketDataService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            DownloadBtnCommand = new DelegateCommand(OnDownloadAsync);
            RefreshBtnCommand = new DelegateCommand(OnRefreshAsync);

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutDownloadHistoricalData))
            {
                PopulateViewModel();
            }
        }

        public async void OnDownloadAsync()
        {
            IsTransactionActive = true;

            try
            {
                var validator = DownloadHistoricalDataModel.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        "Historical Data Download Configuration - Error",
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                DateTime modifiedStart = new(
                    DownloadHistoricalDataModel.StartDate.Year - 3,
                    DownloadHistoricalDataModel.StartDate.Month,
                    DownloadHistoricalDataModel.StartDate.Day,
                    DownloadHistoricalDataModel.StartDate.Hour,
                    DownloadHistoricalDataModel.StartDate.Minute,
                    DownloadHistoricalDataModel.StartDate.Second);

                var symbol = await _marketDataService.GetSymbol(DownloadHistoricalDataModel.SymbolId).ConfigureAwait(true);
                var timeframe = await _marketDataService.GetTimeframe(DownloadHistoricalDataModel.TimeframeId).ConfigureAwait(true);

                var host = await _sharedService.GetSettingAsync((int)SettingEnum.Host).ConfigureAwait(true);
                var port = await _sharedService.GetSettingAsync((int)SettingEnum.Port).ConfigureAwait(true);

                using var requester = new RequestSocket($">tcp://{host.Value}:{port.Value}");

                // Send request-----------------------------------------------------------------------------------------
                var request = JsonConvert.SerializeObject(new ZmqDownloadHistoricalDataRequest
                {
                    Symbol = symbol.Name,
                    Timeframe = (TimeframeEnum)int.Parse(timeframe.Value),
                    Start = modifiedStart,
                    End = DownloadHistoricalDataModel.EndDate,
                });
                requester.SendFrame(request);
                Debug.WriteLine($"RequestSocket-Send:{request}");
                // -----------------------------------------------------------------------------------------------------

                // Receive response-------------------------------------------------------------------------------------
                var timeout = new TimeSpan(0, 0, 5);
                if (!requester.TryReceiveFrameString(timeout, out var response))
                {
                    throw new Exception($"Response not received from MetaTrader after {timeout.ToString("hh:mm:ss")}");
                }

                Debug.WriteLine($"RequestSocket-Receive:{response}");
                // -----------------------------------------------------------------------------------------------------

                var json = JsonConvert.DeserializeObject<ZmqResponse>(response);
                DownloadHistoricalDataModel.FilePathHistoricalData = json.Data;

                if (await CreateHistory().ConfigureAwait(true))
                {
                    var result = await _settingService.CreateHistoricalData(DownloadHistoricalDataModel).ConfigureAwait(true);

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

                IsTransactionActive = false;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowMessage(this,
                    "Error",
                    $"{ex.Message}");

                IsTransactionActive = false;
            }
        }

        private async void OnRefreshAsync()
        {
            if (!IsTransactionActive)
            {
                IsTransactionActive = true;

                Symbols.Clear();

                try
                {
                    var host = await _sharedService.GetSettingAsync((int)SettingEnum.Host).ConfigureAwait(true);
                    var port = await _sharedService.GetSettingAsync((int)SettingEnum.Port).ConfigureAwait(true);

                    using var requester = new RequestSocket($">tcp://{host.Value}:{port.Value}");

                    // Send request-----------------------------------------------------------------------------------------
                    requester.SendFrame(JsonConvert.SerializeObject(new ZmqLoadSymbolListRequest()));
                    Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(new ZmqLoadSymbolListRequest())}");
                    // -----------------------------------------------------------------------------------------------------

                    // Receive response-------------------------------------------------------------------------------------

                    var timeout = new TimeSpan(0, 0, 5);
                    if (!requester.TryReceiveFrameString(timeout, out var response))
                    {
                        throw new Exception($"Response not received from MetaTrader");
                    }

                    Debug.WriteLine($"RequestSocket-Receive:{response}");
                    // -----------------------------------------------------------------------------------------------------

                    var json = JsonConvert.DeserializeObject<ZmqResponse>(response);
                    var symbols = json.Data.Split(',').ToList();

                    symbols.ForEach(symbol =>
                    {
                        _marketDataService.CreateSymbol(new SymbolVM { Name = symbol, Code = symbol });
                    });

                    IsTransactionActive = false;

                    PopulateViewModel();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowMessage(this,
                        "Error",
                        $"{ex.Message}");

                    IsTransactionActive = false;
                }
            }
        }

        private async void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                DownloadHistoricalDataModel = new DownloadHistoricalDataModel()
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

        private async Task<bool> CreateHistory()
        {
            try
            {
                var result = CandleHelper.GetHistoryCandles(DownloadHistoricalDataModel.FilePathHistoricalData).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(item =>
                    {
                        DownloadHistoricalDataModel.HistoricalDataCandles.Add(new HistoricalDataCandleVM
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

                    string marketName = ((MarketEnum)DownloadHistoricalDataModel.MarketId).GetMetadata().Name;
                    var symbol = await _marketDataService.GetSymbol(DownloadHistoricalDataModel.SymbolId).ConfigureAwait(true);
                    var timeframe = await _marketDataService.GetTimeframe(DownloadHistoricalDataModel.TimeframeId).ConfigureAwait(true);

                    var candlesOrdered = result.OrderByDescending(candle => candle.Date);
                    var firstCandleDate = candlesOrdered.LastOrDefault().Date;
                    var lastCandleDate = candlesOrdered.FirstOrDefault().Date;

                    DownloadHistoricalDataModel.EndDate = null;

                    DownloadHistoricalDataModel.Description =
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

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private DownloadHistoricalDataModel _downloadHistoricalDataModel;
        public DownloadHistoricalDataModel DownloadHistoricalDataModel
        {
            get => _downloadHistoricalDataModel;
            set => SetProperty(ref _downloadHistoricalDataModel, value);
        }

        public ObservableCollection<Metadata> Markets { get; } = new ObservableCollection<Metadata>();
        public ObservableCollection<SymbolVM> Symbols { get; } = new ObservableCollection<SymbolVM>();
        public ObservableCollection<TimeframeVM> Timeframes { get; } = new ObservableCollection<TimeframeVM>();
    }
}
