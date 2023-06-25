using AdionFA.Core.API.Contracts.MarketData;

using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Common.IofC;

using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Base;

using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.AutoMapper;

using AutoMapper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Services.AppServices
{
    public class MarketDataServiceAgent : IMarketDataServiceAgent
    {
        private readonly IMapper Mapper;

        public MarketDataServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        // Historical Data

        public async Task<IList<HistoricalDataVM>> GetAllHistoricalDataAsync(bool includeGraph = false)
        {
            try
            {
                IList<HistoricalDataDTO> all = Array.Empty<HistoricalDataDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<IMarketDataAPI>().GetAllHistoricalData(includeGraph);
                });

                return Mapper.Map<IList<HistoricalDataDTO>, IList<HistoricalDataVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<HistoricalDataVM> GetHistoricalDataAsync(int historicalDataId, bool includeGraph = false)
        {
            try
            {
                HistoricalDataDTO dto = null;

                await Task.Run(() =>
                {
                    dto = IoC.Get<IMarketDataAPI>().GetHistoricalData(historicalDataId, includeGraph);
                });

                return Mapper.Map<HistoricalDataDTO, HistoricalDataVM>(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<HistoricalDataVM> GetHistoricalDataAsync(int marketId = (int)MarketEnum.Forex, int symbolId = (int)CurrencyPairEnum.EURUSD, int timeframeId = (int)TimeframeEnum.H1)
        {
            try
            {
                HistoricalDataDTO dto = null;

                await Task.Run(() =>
                {
                    dto = IoC.Get<IMarketDataAPI>().GetHistoricalData(marketId, symbolId, timeframeId);
                });

                return Mapper.Map<HistoricalDataDTO, HistoricalDataVM>(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> CreateHistoricalDataAsync(HistoricalDataVM vm)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                var dto = Mapper.Map<HistoricalDataVM, HistoricalDataDTO>(vm);

                await Task.Run(() =>
                {
                    result = IoC.Get<IMarketDataAPI>().CreateHistoricalData(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public async Task<IList<TimeframeVM>> GetAllTimeframeAsync(bool includeGraph = false)
        {
            try
            {
                IList<TimeframeDTO> all = Array.Empty<TimeframeDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<IMarketDataAPI>().GetAllTimeframe(includeGraph);
                });

                return Mapper.Map<IList<TimeframeDTO>, IList<TimeframeVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<TimeframeVM> GetTimeframeAsync(int timeframeId)
        {
            try
            {
                var timeframeDTO = new TimeframeDTO();

                await Task.Run(() =>
                {
                    timeframeDTO = IoC.Get<IMarketDataAPI>().GetTimeframe(timeframeId);
                });

                return Mapper.Map<TimeframeDTO, TimeframeVM>(timeframeDTO);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Symbol

        public async Task<ResponseVM> CreateSymbolAsync(SymbolVM symbolVM)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };
                var dto = Mapper.Map<SymbolVM, SymbolDTO>(symbolVM);

                await Task.Run(() =>
                {
                    response = IoC.Get<IMarketDataAPI>().CreateSymbol(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(response);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<IList<SymbolVM>> GetAllSymbolAsync(bool includeGraph = false)
        {
            try
            {
                IList<SymbolDTO> all = Array.Empty<SymbolDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<IMarketDataAPI>().GetAllSymbol(includeGraph);
                });

                return Mapper.Map<IList<SymbolDTO>, IList<SymbolVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<SymbolVM> GetSymbolAsync(int symbolId)
        {
            try
            {
                var symbolDTO = new SymbolDTO();

                await Task.Run(() =>
                {
                    symbolDTO = IoC.Get<IMarketDataAPI>().GetSymbol(symbolId);
                });

                return Mapper.Map<SymbolDTO, SymbolVM>(symbolDTO);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<SymbolVM> GetSymbolAsync(string symbolName)
        {
            try
            {
                var symbolDTO = new SymbolDTO();

                await Task.Run(() =>
                {
                    symbolDTO = IoC.Get<IMarketDataAPI>().GetSymbol(symbolName);
                });

                return Mapper.Map<SymbolDTO, SymbolVM>(symbolDTO);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}