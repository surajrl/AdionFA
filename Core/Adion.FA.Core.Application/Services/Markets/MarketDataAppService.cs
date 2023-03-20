using Adion.FA.Core.Application.Contracts.Markets;
using Adion.FA.Core.Domain.Aggregates.Market;
using Adion.FA.Core.Domain.Contracts.Markets;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Adion.FA.TransferObject.Market;
using Adion.FA.TransferObject.Base;

namespace Adion.FA.Core.Application.Services.Markets
{
    public class MarketDataAppService : AppServiceBase, IMarketDataAppService
    {
        #region Services

        [Inject]
        public IMarketDataDomainService MarketDataDomainService { get; set; }

        #endregion

        #region Ctor

        public MarketDataAppService() : base()
        {
        }

        #endregion

        #region Market 

        public IList<MarketDataDTO> GetAllMarketData(bool includeGraph = false)
        {
            try
            {
                IList<MarketData> configurations = MarketDataDomainService.GetAllMarketData(includeGraph);
                IList<MarketDataDTO> dto = Mapper.Map<IList<MarketDataDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public MarketDataDTO GetMarketData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                MarketData h = MarketDataDomainService.GetMarketData(marketDataId, includeGraph);
                MarketDataDTO dto = Mapper.Map<MarketDataDTO>(h);
                
                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public MarketDataDTO GetMarketData(int marketId, int currencyPairId, int currencyPeriodId)
        {
            try
            {
                MarketData h = MarketDataDomainService.GetMarketData(marketId, currencyPairId, currencyPeriodId);
                MarketDataDTO dto = Mapper.Map<MarketDataDTO>(h);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateMarketData(MarketDataDTO market)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                MarketData entity = Mapper.Map<MarketData>(market);
                
                response.IsSuccess = MarketDataDomainService.CreateMarketData(entity) > 0;

                if (response.IsSuccess)
                {
                    LogInfoCreate<MarketDataDTO>();
                }
                
                return response;
            }
            catch (Exception ex)
            {
                LogException<MarketDataAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
