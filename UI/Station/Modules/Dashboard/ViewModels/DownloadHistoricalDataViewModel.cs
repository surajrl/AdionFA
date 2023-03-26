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

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class DownloadHistoricalDataViewModel : ViewModelBase
    {
        private readonly IMetaTraderService _metaTraderService;
        private readonly IHistoricalDataServiceAgent _historicalDataService;
        private readonly ISettingService _settingService;
        private readonly IEventAggregator _eventAggregator;

        private ICommand FlyoutCommand { get; }
        public ICommand DownloadBtnCommand { get; }
        public ICommand RefreshBtnCommand { get; }

        public DownloadHistoricalDataViewModel(
            IApplicationCommands applicationCommands,
            IMetaTraderService metaTraderService,
            ISettingService settingService,
            IHistoricalDataServiceAgent historicalDataService)
        {
            _metaTraderService = metaTraderService;
            _settingService = settingService;
            _historicalDataService = historicalDataService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            DownloadBtnCommand = new DelegateCommand(OnDownloadAsync);
            RefreshBtnCommand = new DelegateCommand(OnRefreshAsync);

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<MetaTraderConnectedEvent>().Subscribe(p => IsMetaTraderConnected = p);

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

            var validator = DownloadHistoricalDataModel.Validate();
            if (!validator.IsValid)
            {
                IsTransactionActive = false;

                MessageHelper.ShowMessages(this,
                    "Historical Data Download Configuration - Error",
                    validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                return;
            }

            var symbol = await _historicalDataService.GetSymbol(DownloadHistoricalDataModel.Symbol.Name);
            DownloadHistoricalDataModel.Symbol = symbol;

            DateTime modifiedStart = new(
                DownloadHistoricalDataModel.StartDate.Year - 3,
                DownloadHistoricalDataModel.StartDate.Month,
                DownloadHistoricalDataModel.StartDate.Day,
                DownloadHistoricalDataModel.StartDate.Hour,
                DownloadHistoricalDataModel.StartDate.Minute,
                DownloadHistoricalDataModel.StartDate.Second);
            try
            {
                var filepath = await _metaTraderService.DownloadHistoricalDataAsync(
                DownloadHistoricalDataModel.Symbol.Name,
                int.Parse(DownloadHistoricalDataModel.Timeframe.Value),
                DownloadHistoricalDataModel.StartDate,
                DownloadHistoricalDataModel.EndDate).ConfigureAwait(true);

                DownloadHistoricalDataModel.FilePathHistoricalData = filepath.Replace("\r\0", string.Empty);

                if (CreateHistory())
                {
                    var result = await _settingService.CreateHistoricalData(DownloadHistoricalDataModel);

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
            catch (SocketException ex)
            {
                MessageHelper.ShowMessage(this,
                    "MetaTrader 5 Server",
                    $"{ex.Message}"
                    );

                IsTransactionActive = false;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowMessage(this,
                    "Error",
                    $"{ex.Message}"
                    );

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
                    var symbols = await _metaTraderService.LoadSymbolListAsync().ConfigureAwait(true);
                    symbols.ForEach(symbol =>
                    {
                        symbol = symbol.Replace("\0", string.Empty);

                        var newSymbol = new SymbolVM
                        {
                            Code = symbol,
                            Name = symbol
                        };

                        _historicalDataService.CreateSymbol(newSymbol);
                        Symbols.Add(newSymbol);
                    });

                    IsTransactionActive = false;

                    PopulateViewModel();
                }
                catch (SocketException ex)
                {
                    MessageHelper.ShowMessage(this,
                        "MetaTrader 5 Server",
                        $"{ex.Message}"
                        );

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
                    HistoricalDataDetails = Array.Empty<HistoricalDataDetailVM>().ToList()
                };
            }

            // TODO: Load the markets from the database
            if (!Markets.Any())
                Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());

            if (!Timeframes.Any())
            {
                var timeframes = await _historicalDataService.GetAllTimeframe().ConfigureAwait(true);
                timeframes.ForEach(Timeframes.Add);
            }
        }

        private bool CreateHistory()
        {
            try
            {
                List<Candle> result = CandleHelper.GetHistoryCandles(DownloadHistoricalDataModel.FilePathHistoricalData).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(item =>
                    {
                        DownloadHistoricalDataModel.HistoricalDataDetails.Add(new HistoricalDataDetailVM
                        {
                            StartDate = item.Date,
                            StartTime = item.Time,
                            OpenPrice = item.Open,
                            MaxPrice = item.High,
                            MinPrice = item.Low,
                            ClosePrice = item.Close,
                            Volume = item.Volume
                        });
                    });

                    string marketName = ((MarketEnum)DownloadHistoricalDataModel.MarketId).GetMetadata().Name;
                    string symbolName = DownloadHistoricalDataModel.Symbol.Name;
                    string timeframeName = DownloadHistoricalDataModel.Timeframe.Code;
                    DownloadHistoricalDataModel.Description = $"{marketName}.{symbolName}.{timeframeName}";

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

        private bool _isMetaTraderConnected;

        public bool IsMetaTraderConnected
        {
            get => _isMetaTraderConnected;
            set => SetProperty(ref _isMetaTraderConnected, value);
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
