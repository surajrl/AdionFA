using Adion.FA.Core.Domain.Aggregates.Market;
using System.Collections.Generic;

namespace Adion.FA.Core.Domain.Contracts.Markets
{
    public interface IMarketDataDomainService
    {
        IList<MarketData> GetAllMarketData(bool includeGraph = false);
        MarketData GetMarketData(int marketDataId, bool includeGraph = false);
        MarketData GetMarketData(int marketId, int currencyPairId, int currencyPeriodId);
        int CreateMarketData(MarketData marketData);
    }
}
