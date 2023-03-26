using AdionFA.Core.Application.Contracts.Markets;
using AdionFA.Core.Domain.Aggregates.Market;
using AdionFA.Core.Domain.Contracts.Markets;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdionFA.TransferObject.Market;
using AdionFA.TransferObject.Base;

namespace AdionFA.Core.Application.Services.Markets
{
    public class HistoricalDataAppService : AppServiceBase, IHistoricalDataAppService
    {
        [Inject]
        public IHistoricalDataDomainService HistoricalDataDomainService { get; set; }

        public HistoricalDataAppService() : base()
        {
        }

        // Historical Data

        public IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                IList<HistoricalData> configurations = HistoricalDataDomainService.GetAllHistoricalData(includeGraph);
                IList<HistoricalDataDTO> dto = Mapper.Map<IList<HistoricalDataDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                HistoricalData h = HistoricalDataDomainService.GetHistoricalData(marketDataId, includeGraph);
                HistoricalDataDTO dto = Mapper.Map<HistoricalDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId)
        {
            try
            {
                HistoricalData h = HistoricalDataDomainService.GetHistoricalData(marketId, symbolId, timeframeId);
                HistoricalDataDTO dto = Mapper.Map<HistoricalDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
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

                response.IsSuccess = HistoricalDataDomainService.CreateHistoricalData(entity) > 0;

                if (response.IsSuccess)
                    LogInfoCreate<HistoricalDataDTO>();

                return response;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Timeframe

        public IList<TimeframeDTO> GetAllTimeframe(bool includeGraph)
        {
            try
            {
                IList<Timeframe> configurations = HistoricalDataDomainService.GetAllTimeframe(includeGraph);
                IList<TimeframeDTO> dto = Mapper.Map<IList<TimeframeDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public TimeframeDTO GetTimeframe(int timeframeId)
        {
            try
            {
                Timeframe timeframe = HistoricalDataDomainService.GetTimeframe(timeframeId);
                TimeframeDTO timeframeDto = Mapper.Map<TimeframeDTO>(timeframe);

                return timeframeDto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
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
                response.IsSuccess = (HistoricalDataDomainService.CreateSymbol(symbolEntity) ?? 1) > 0;

                if (response.IsSuccess)
                    LogInfoCreate<HistoricalDataDTO>();

                return response;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public IList<SymbolDTO> GetAllSymbol(bool includeGraph = false)
        {
            try
            {
                IList<Symbol> configurations = HistoricalDataDomainService.GetAllSymbol(includeGraph);
                IList<SymbolDTO> dto = Mapper.Map<IList<SymbolDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(int symbolId)
        {
            try
            {
                Symbol symbol = HistoricalDataDomainService.GetSymbol(symbolId);
                SymbolDTO symbolDto = Mapper.Map<SymbolDTO>(symbol);

                return symbolDto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SymbolDTO GetSymbol(string symbolName)
        {
            try
            {
                Symbol symbol = HistoricalDataDomainService.GetSymbol(symbolName);
                SymbolDTO dto = Mapper.Map<SymbolDTO>(symbol);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<HistoricalDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
