using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Core.Application.Contracts.MarketData;
using AdionFA.Core.Domain.Contracts.MarketData;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Base;

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
                IList<HistoricalData> configurations = MarketDataDomainService.GetAllHistoricalData(includeGraph);
                IList<HistoricalDataDTO> dto = Mapper.Map<IList<HistoricalDataDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                HistoricalData h = MarketDataDomainService.GetHistoricalData(marketDataId, includeGraph);
                HistoricalDataDTO dto = Mapper.Map<HistoricalDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId)
        {
            try
            {
                HistoricalData h = MarketDataDomainService.GetHistoricalData(marketId, symbolId, timeframeId);
                HistoricalDataDTO dto = Mapper.Map<HistoricalDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateHistoricalData(HistoricalDataDTO market)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                HistoricalData entity = Mapper.Map<HistoricalData>(market);

                response.IsSuccess = MarketDataDomainService.CreateHistoricalData(entity) > 0;

                if (response.IsSuccess)
                    LogInfoCreate<HistoricalDataDTO>();

                return response;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateHistoricalData(HistoricalDataDTO historicalData)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                HistoricalData entity = Mapper.Map<HistoricalData>(historicalData);

                response.IsSuccess = MarketDataDomainService.UpdateHistoricalData(entity) > 0;

                if (response.IsSuccess)
                    LogInfoCreate<HistoricalDataDTO>();

                return response;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe(bool includeGraph)
        {
            try
            {
                IList<Timeframe> configurations = MarketDataDomainService.GetAllTimeframe(includeGraph);
                IList<TimeframeDTO> dto = Mapper.Map<IList<TimeframeDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            try
            {
                Timeframe timeframe = MarketDataDomainService.GetTimeframe(timeframeId);
                TimeframeDTO timeframeDto = Mapper.Map<TimeframeDTO>(timeframe);

                return timeframeDto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
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

                Symbol symbolEntity = Mapper.Map<Symbol>(symbol);
                response.IsSuccess = (MarketDataDomainService.CreateSymbol(symbolEntity) ?? 1) > 0;

                if (response.IsSuccess)
                    LogInfoCreate<HistoricalDataDTO>();

                return response;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public IList<SymbolDTO> GetAllSymbol(bool includeGraph = false)
        {
            try
            {
                IList<Symbol> configurations = MarketDataDomainService.GetAllSymbol(includeGraph);
                IList<SymbolDTO> dto = Mapper.Map<IList<SymbolDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            try
            {
                Symbol symbol = MarketDataDomainService.GetSymbol(symbolId);
                SymbolDTO symbolDto = Mapper.Map<SymbolDTO>(symbol);

                return symbolDto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            try
            {
                Symbol symbol = MarketDataDomainService.GetSymbol(symbolName);
                SymbolDTO dto = Mapper.Map<SymbolDTO>(symbol);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
