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
using System.Windows.Input;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class UploadHistoricalDataViewModel : ViewModelBase
    {
        public readonly ISettingService _settingService;
        public readonly IMarketDataServiceAgent _historicalDataService;

        public UploadHistoricalDataViewModel(
            ISettingService settingService,
            IMarketDataServiceAgent historicalDataService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _historicalDataService = historicalDataService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        #region Commands

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

                var validator = _uploadHistoricalData.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.MarketData.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                if (CreateHistory())
                {
                    var result = await _settingService.CreateHistoricalData(_uploadHistoricalData);

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

        private bool CreateHistory()
        {
            try
            {
                List<Candle> result = CandleHelper.GetHistoryCandles(_uploadHistoricalData.FilePathHistoricalData).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(item =>
                    {
                        _uploadHistoricalData.HistoricalDataDetails.Add(new HistoricalDataDetailVM
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

                    string marketName = ((MarketEnum)UploadHistoricalData.MarketId).GetMetadata().Name;
                    string symbolName = UploadHistoricalData.Symbol.Name;
                    string timeframeCode = UploadHistoricalData.Timeframe.Code;
                    UploadHistoricalData.Description = $"{marketName}.{symbolName}.{timeframeCode}";

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

        #endregion Commands

        private async void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                UploadHistoricalData = new UploadHistoricalDataModel
                {
                    HistoricalDataDetails = Array.Empty<HistoricalDataDetailVM>().ToList()
                };
            }

            if (!Markets.Any())
                Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());

            if (!Timeframes.Any())
            {
                var timeframes = await _historicalDataService.GetAllTimeframe().ConfigureAwait(true);
                timeframes.ForEach(Timeframes.Add);
            }

            if (!Symbols.Any())
            {
                var symbols = await _historicalDataService.GetAllSymbol().ConfigureAwait(true);
                symbols.ForEach(Symbols.Add);
            }
        }

        #region Bindable Model

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

        #endregion Bindable Model
    }
}
