using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Market;
using System.Collections.Generic;

namespace Adion.FA.Core.API.Contracts.Markets
{
    public interface IMarketDataAPI
    {
        #region Market Data

        IList<MarketDataDTO> GetAllMarketData(bool includeGraph = false);
        MarketDataDTO GetMarketData(int marketDataId, bool includeGraph = false);
        MarketDataDTO GetMarketData(int marketId, int currencyPairId, int currencyPeriodId);
        ResponseDTO CreateMarketData(MarketDataDTO market);

        #endregion
    }
}
