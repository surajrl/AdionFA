using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Helpers;
using AdionFA.Domain.Model;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
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
using Ninject;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class UploadHistoricalDataViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        private readonly ISettingService _settingService;
        private readonly IMarketDataAppService _marketDataService;

        public UploadHistoricalDataViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;
            _marketDataService = IoC.Kernel.Get<IMarketDataAppService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutUploadHistoricalData))
            {
                PopulateViewModel();
            }
        }

        public DelegateCommand UploadBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                IsTransactionActive = true;

                var validator = UploadHistoricalData.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.HistoricalData.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                if (CreateHistory())
                {
                    var result = _settingService.CreateHistoricalData(UploadHistoricalData);

                    IsTransactionActive = false;

                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.HistoricalData.GetMetadata().Description,
                        result
                        ? Resources.SuccessEntitySave
                        : Resources.FailEntitySave);

                    PopulateViewModel();

                    if (result)
                    {
                        ContainerLocator.Current.Resolve<IApplicationCommands>().LoadHistoricalDataCommand.Execute(null);
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
                var result = CandleHelper.GetHistoryCandles(UploadHistoricalData.FilepathHistoricalData).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(candle =>
                    {
                        UploadHistoricalData.HistoricalDataCandles.Add(new HistoricalDataCandleVM
                        {
                            StartDate = candle.Date,
                            StartTime = candle.Time,
                            Open = candle.Open,
                            High = candle.High,
                            Low = candle.Low,
                            Close = candle.Close,
                            Volume = candle.Volume,
                            Spread = candle.Spread,
                        });
                    });

                    var marketName = ((MarketEnum)UploadHistoricalData.MarketId).GetMetadata().Name;
                    var symbol = _marketDataService.GetSymbol(UploadHistoricalData.SymbolId);
                    var timeframe = _marketDataService.GetTimeframe(UploadHistoricalData.TimeframeId);

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

        private void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                UploadHistoricalData = new UploadHistoricalDataModel
                {
                    HistoricalDataCandles = Array.Empty<HistoricalDataCandleVM>().ToList()
                };
            }

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

            if (!Symbols.Any())
            {
                var symbols = _marketDataService.GetAllSymbol();
                symbols.ToList().ForEach(symbol =>
                {
                    Symbols.Add(_mapper.Map<SymbolDTO, SymbolVM>(symbol));
                });
            }
        }

        // Bindable Model

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
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