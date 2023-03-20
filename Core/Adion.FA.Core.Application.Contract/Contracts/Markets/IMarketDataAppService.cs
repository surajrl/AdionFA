using Adion.FA.Core.Application.Contract.Contracts;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Market;
using System.Collections.Generic;

namespace Adion.FA.Core.Application.Contracts.Markets
{
    public interface IMarketDataAppService : IAppContractBase
    {
        IList<MarketDataDTO> GetAllMarketData(bool includeGraph = false);
        MarketDataDTO GetMarketData(int marketDataId, bool includeGraph = false);
        MarketDataDTO GetMarketData(int marketId, int currencyPairId, int currencyPeriodId);
        ResponseDTO CreateMarketData(MarketDataDTO market);
    }
}
