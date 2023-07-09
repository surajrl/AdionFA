using AdionFA.Core.Application.Contracts.MarketData;
using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Domain.Contracts.MarketData;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdionFA.Core.Application.Services.MarketData
{
    public class MarketDataAppService : AppServiceBase, IMarketDataAppService
    {
        [Inject]
        public IMarketDataDomainService MarketDataDomainService { get; set; }

        public MarketDataAppService() : base()
        {
        }

        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                var configurations = MarketDataDomainService.GetAllHistoricalData(includeGraph);
                var dto = Mapper.Map<IList<HistoricalDataDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                var h = MarketDataDomainService.GetHistoricalData(marketDataId, includeGraph);
                var dto = Mapper.Map<HistoricalDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId)
        {
            try
            {
                var h = MarketDataDomainService.GetHistoricalData(marketId, symbolId, timeframeId);
                var dto = Mapper.Map<HistoricalDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateHistoricalData(HistoricalDataDTO market)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var entity = Mapper.Map<HistoricalData>(market);

                response.IsSuccess = MarketDataDomainService.CreateHistoricalData(entity) > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateHistoricalData(HistoricalDataDTO historicalData)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var entity = Mapper.Map<HistoricalData>(historicalData);

                response.IsSuccess = MarketDataDomainService.UpdateHistoricalData(entity) > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe(bool includeGraph)
        {
            try
            {
                var configurations = MarketDataDomainService.GetAllTimeframe(includeGraph);
                var dto = Mapper.Map<IList<TimeframeDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            try
            {
                var timeframe = MarketDataDomainService.GetTimeframe(timeframeId);
                var timeframeDto = Mapper.Map<TimeframeDTO>(timeframe);

                return timeframeDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Symbol

        public ResponseDTO CreateSymbol(SymbolDTO symbol)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false,
                };

                var symbolEntity = Mapper.Map<Symbol>(symbol);
                response.IsSuccess = (MarketDataDomainService.CreateSymbol(symbolEntity) ?? 1) > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public IList<SymbolDTO> GetAllSymbol(bool includeGraph = false)
        {
            try
            {
                var configurations = MarketDataDomainService.GetAllSymbol(includeGraph);
                var dto = Mapper.Map<IList<SymbolDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            try
            {
                var symbol = MarketDataDomainService.GetSymbol(symbolId);
                var symbolDto = Mapper.Map<SymbolDTO>(symbol);

                return symbolDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            try
            {
                var symbol = MarketDataDomainService.GetSymbol(symbolName);
                var dto = Mapper.Map<SymbolDTO>(symbol);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
