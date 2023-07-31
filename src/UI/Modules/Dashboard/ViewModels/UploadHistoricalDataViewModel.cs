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

        private readonly IMarketDataService _marketDataService;

        public UploadHistoricalDataViewModel(IApplicationCommands applicationCommands)
        {
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutUploadHistoricalData))
            {
                PopulateViewModel();
            }
        });


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
                        EntityTypeEnum.HistoricalData.GetMetadata().Name,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                if (CreateHistory())
                {
                    var result = _marketDataService.CreateHistoricalData(_mapper.Map<UploadHistoricalDataModel, HistoricalDataDTO>(UploadHistoricalData));

                    IsTransactionActive = false;

                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.HistoricalData.GetMetadata().Name,
                        result.IsSuccess
                        ? Resources.SuccessEntitySave
                        : Resources.FailEntitySave);

                    PopulateViewModel();

                    if (result.IsSuccess)
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
                var result = CandleHelper.GetHistoryCandles(UploadHistoricalData.FilePathHistoricalData).ToList();

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

                    var market = ((MarketEnum)UploadHistoricalData.MarketId).GetMetadata();
                    var symbol = _marketDataService.GetSymbol(UploadHistoricalData.SymbolId);
                    var timeframe = _marketDataService.GetTimeframe(UploadHistoricalData.TimeframeId);

                    var candlesOrdered = result.OrderByDescending(candle => candle.Date);
                    var firstCandleDate = candlesOrdered.LastOrDefault().Date;
                    var lastCandleDate = candlesOrdered.FirstOrDefault().Date;

                    UploadHistoricalData.Description =
                        $"{market.Code}." +
                        $"{symbol.Code}." +
                        $"{timeframe.Code}";

                    return true;
                }

                MessageHelper.ShowMessage(this,
                    EntityTypeEnum.HistoricalData.GetMetadata().Name,
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