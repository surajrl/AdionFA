using Adion.FA.Core.API.Contracts.Markets;
using Adion.FA.Core.Application.Contracts.Markets;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Market;
using System.Collections.Generic;

namespace Adion.FA.Core.API.Markets
{
    public class MarketDataAPI : IMarketDataAPI
    {
        #region Market Data
        
        public IList<MarketDataDTO> GetAllMarketData(bool includeGraph = false)
        {
            using (var service = IoC.Get<IMarketDataAppService>())
                return service.GetAllMarketData(includeGraph);
        }

        public MarketDataDTO GetMarketData(int marketDataId, bool includeGraph = false)
        {
            using (var service = IoC.Get<IMarketDataAppService>())
                return service.GetMarketData(marketDataId, includeGraph);
        }

        public MarketDataDTO GetMarketData(int marketId, int currencyPairId, int currencyPeriodId)
        {
            using (var service = IoC.Get<IMarketDataAppService>())
                return service.GetMarketData(marketId, currencyPairId, currencyPeriodId);
        }

        public ResponseDTO CreateMarketData(MarketDataDTO market)
        {
            using (var service = IoC.Get<IMarketDataAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateMarketData(market);
            }
        }

        #endregion
    }
}
