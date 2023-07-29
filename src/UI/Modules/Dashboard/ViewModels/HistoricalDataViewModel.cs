using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Helpers;
using AdionFA.Domain.Model;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.MarketData;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Infrastructure.Services;
using AutoMapper;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class HistoricalDataViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        private readonly IMarketDataService _marketDataService;

        public HistoricalDataViewModel(IApplicationCommands applicationCommands)
        {
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.LoadHistoricalDataCommand.RegisterCommand(HistoricalDataFilterCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            Markets = new();
            Symbols = new();
            Timeframes = new();
            HistoricalDataCandles = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutHistoricalData))
            {
                try
                {
                    SelectedMarketId = 0;
                    SelectedTimeframeId = 0;
                    SelectedSymbolId = 0;

                    HistoricalDataCandles.Clear();

                    if (!Markets.Any())
                    {
                        Markets.AddRange(EnumUtil.ToEnumerable<MarketEnum>());
                    }

                    Symbols.Clear();
                    var symbols = _marketDataService.GetAllSymbol();
                    symbols.ToList().ForEach(symbol =>
                    {
                        Symbols.Add(new Metadata { Name = symbol.Name, Id = symbol.SymbolId });
                    });

                    Timeframes.Clear();
                    var timeframes = _marketDataService.GetAllTimeframe();
                    timeframes.ToList().ForEach(timeframe =>
                    {
                        Timeframes.Add(new Metadata { Name = timeframe.Name, Id = timeframe.TimeframeId });
                    });

                    LoadHistoricalData();
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }
            }
        });

        public ICommand HistoricalDataFilterCommand => new DelegateCommand(LoadHistoricalData);

        private async void LoadHistoricalData()
        {
            try
            {
                IsTransactionActive = true;

                if (SelectedMarketId <= 0 || SelectedSymbolId <= 0 || SelectedTimeframeId <= 0)
                {
                    return;
                }

                var historicalDataDTO = new HistoricalDataDTO();
                await Task.Run(() =>
                {
                    historicalDataDTO = _marketDataService.GetHistoricalData(SelectedMarketId, SelectedSymbolId, SelectedTimeframeId, true);
                });

                HistoricalDataCandles.Clear();
                if (historicalDataDTO != null)
                {
                    HistoricalDataCandles.AddRange(_mapper.Map<IList<HistoricalDataCandleDTO>, IList<HistoricalDataCandleVM>>(historicalDataDTO.HistoricalDataCandles));
                }
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

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private int _selectedMarketId;
        public int SelectedMarketId
        {
            get => _selectedMarketId;
            set => SetProperty(ref _selectedMarketId, value);
        }

        private int _selectedSymbolId;
        public int SelectedSymbolId
        {
            get => _selectedSymbolId;
            set => SetProperty(ref _selectedSymbolId, value);
        }

        private int _selectedTimeframeId;
        public int SelectedTimeframeId
        {
            get => _selectedTimeframeId;
            set => SetProperty(ref _selectedTimeframeId, value);
        }

        public ObservableCollection<HistoricalDataCandleVM> HistoricalDataCandles { get; }

        public ObservableCollection<Metadata> Markets { get; }
        public ObservableCollection<Metadata> Symbols { get; }
        public ObservableCollection<Metadata> Timeframes { get; }
    }
}