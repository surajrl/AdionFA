using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Helpers;
using AdionFA.Domain.Model;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.MetaTrader.Model;
using AdionFA.TransferObject.MarketData;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.Module.Dashboard.Model;
using AdionFA.UI.Module.Dashboard.Services;
using AutoMapper;
using DynamicData;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using Ninject;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class DownloadHistoricalDataViewModel : ViewModelBase
    {
        private readonly IAppSettingAppService _appSettingService;
        private readonly IMarketDataAppService _marketDataService;
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;


        public DownloadHistoricalDataViewModel(
            IApplicationCommands applicationCommands,
            ISettingService settingService)
        {
            _settingService = settingService;

            _marketDataService = IoC.Kernel.Get<IMarketDataAppService>();
            _appSettingService = IoC.Kernel.Get<IAppSettingAppService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutDownloadHistoricalData))
            {
                Markets.Clear();
                Timeframes.Clear();
                Symbols.Clear();

                DownloadHistoricalDataModel = new DownloadHistoricalDataModel()
                {
                    HistoricalDataCandles = Array.Empty<HistoricalDataCandleVM>().ToList()
                };
            }
        });

        public ICommand DownloadBtnCommand => new DelegateCommand(async () =>
        {
            IsTransactionActive = true;

            try
            {
                DownloadHistoricalDataModel.SymbolId = DownloadHistoricalDataModel.Symbol.SymbolId;
                DownloadHistoricalDataModel.TimeframeId = DownloadHistoricalDataModel.Timeframe.TimeframeId;

                var validator = DownloadHistoricalDataModel.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        "Download Historical Data",
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                DateTime modifiedStart = new(
                    DownloadHistoricalDataModel.Start.Value.Year - 3,
                    DownloadHistoricalDataModel.Start.Value.Month,
                    DownloadHistoricalDataModel.Start.Value.Day,
                    DownloadHistoricalDataModel.Start.Value.Hour,
                    DownloadHistoricalDataModel.Start.Value.Minute,
                    DownloadHistoricalDataModel.Start.Value.Second);

                var host = _appSettingService.GetSetting((int)SettingEnum.Host);
                var port = _appSettingService.GetSetting((int)SettingEnum.Port);

                using var requester = new RequestSocket($">tcp://{host.Value}:{port.Value}");

                // Send request-----------------------------------------------------------------------------------------
                var request = JsonConvert.SerializeObject(new ZmqDownloadHistoricalDataRequest
                {
                    Symbol = DownloadHistoricalDataModel.Symbol.Name,
                    Timeframe = (TimeframeEnum)int.Parse(DownloadHistoricalDataModel.Timeframe.Value),
                    Start = modifiedStart,
                    End = DownloadHistoricalDataModel.End.Value,
                });

                requester.SendFrame(request);
                Debug.WriteLine($"RequestSocket-Send:{request}");
                // -----------------------------------------------------------------------------------------------------

                // Receive response-------------------------------------------------------------------------------------
                var response = string.Empty;
                var timeout = new TimeSpan(0, 0, 5);
                if (await Task.Run(() => !requester.TryReceiveFrameString(timeout, out response)).ConfigureAwait(true))
                {
                    throw new Exception($"Response not received from MetaTrader");
                }

                Debug.WriteLine($"RequestSocket-Receive:{response}");
                // -----------------------------------------------------------------------------------------------------

                var json = JsonConvert.DeserializeObject<ZmqResponse>(response);

                if (json.Status == 0)
                {
                    throw new Exception(json.Message);
                }

                DownloadHistoricalDataModel.FilepathHistoricalData = json.Data;

                if (CreateHistory())
                {
                    var result = _settingService.CreateHistoricalData(DownloadHistoricalDataModel);

                    IsTransactionActive = false;

                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.HistoricalData.GetMetadata().Description,
                        result
                        ? Resources.SuccessEntitySave
                        : Resources.FailEntitySave);

                    if (result)
                    {
                        ContainerLocator.Current.Resolve<IApplicationCommands>().LoadHistoricalDataCommand.Execute(null);
                    }
                }

                IsTransactionActive = false;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowMessage(this,
                    "Download Historical Data",
                    $"{ex.Message}");

                IsTransactionActive = false;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);


        public ICommand RefreshBtnCommand => new DelegateCommand(async () =>
        {
            if (!IsTransactionActive)
            {
                IsTransactionActive = true;

                try
                {
                    Symbols.Clear();

                    var host = _appSettingService.GetSetting((int)SettingEnum.Host);
                    var port = _appSettingService.GetSetting((int)SettingEnum.Port);

                    using var requester = new RequestSocket($">tcp://{host.Value}:{port.Value}");

                    // Send request-----------------------------------------------------------------------------------------
                    requester.SendFrame(JsonConvert.SerializeObject(new ZmqLoadSymbolListRequest()));
                    Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(new ZmqLoadSymbolListRequest())}");
                    // -----------------------------------------------------------------------------------------------------

                    // Receive response-------------------------------------------------------------------------------------
                    var response = string.Empty;
                    var timeout = new TimeSpan(0, 0, 5);
                    if (await Task.Run(() => !requester.TryReceiveFrameString(timeout, out response)).ConfigureAwait(true))
                    {
                        throw new Exception($"Response not received from MetaTrader");
                    }

                    Debug.WriteLine($"RequestSocket-Receive:{response}");
                    // -----------------------------------------------------------------------------------------------------

                    var json = JsonConvert.DeserializeObject<ZmqResponse>(response);
                    var symbolsReceived = json.Data.Split(',').ToList();

                    symbolsReceived.ForEach(symbol =>
                    {
                        _marketDataService.CreateSymbol(new SymbolDTO { Name = symbol, Code = symbol });
                        Symbols.Add(_mapper.Map<SymbolDTO, SymbolVM>(_marketDataService.GetSymbol(symbol)));
                    });

                    if (!Markets.Any())
                    {
                        Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());
                    }

                    if (!Timeframes.Any())
                    {
                        var timeframes = _marketDataService.GetAllTimeframe();
                        timeframes.ToList().ForEach(timeframe =>
                        {
                            Timeframes.Add(_mapper.Map<TimeframeDTO, TimeframeVM>(timeframe));
                        });
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
        });

        private bool CreateHistory()
        {
            try
            {
                var result = CandleHelper.GetHistoryCandles(DownloadHistoricalDataModel.FilepathHistoricalData).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(candle =>
                    {
                        DownloadHistoricalDataModel.HistoricalDataCandles.Add(new HistoricalDataCandleVM
                        {
                            StartDate = candle.Date,
                            StartTime = candle.Time,
                            Open = candle.Open,
                            High = candle.High,
                            Low = candle.Low,
                            Close = candle.Close,
                            Volume = candle.Volume,
                            Spread = candle.Spread
                        });
                    });

                    var marketName = ((MarketEnum)DownloadHistoricalDataModel.MarketId).GetMetadata().Name;
                    var symbol = _marketDataService.GetSymbol(DownloadHistoricalDataModel.SymbolId);
                    var timeframe = _marketDataService.GetTimeframe(DownloadHistoricalDataModel.TimeframeId);

                    var candlesOrdered = result.OrderByDescending(candle => candle.Date);
                    var firstCandleDate = candlesOrdered.LastOrDefault().Date;
                    var lastCandleDate = candlesOrdered.FirstOrDefault().Date;

                    DownloadHistoricalDataModel.Description =
                        $"{marketName}." +
                        $"{symbol.Name}." +
                        $"{timeframe.Code}." +
                        $"{firstCandleDate:dd-MM-yyyy}." +
                        $"{lastCandleDate:dd-MM-yyyy}";

                    return true;
                }

                MessageHelper.ShowMessage(this,
                    EntityTypeEnum.HistoricalData.GetMetadata().Description,
                    Resources.HistoricalDataEmpty);

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