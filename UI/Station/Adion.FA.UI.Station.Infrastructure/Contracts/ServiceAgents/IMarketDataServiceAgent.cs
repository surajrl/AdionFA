using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface IMarketDataServiceAgent
    {
        Task<IList<MarketDataVM>> GetAllMarketData(bool includeGraph = false);

        Task<MarketDataVM> GetMarketData(int marketDataId, bool includeGraph = false);

        Task<MarketDataVM> GetMarketData(int marketId = 0, int currencyPairId = 0, int currencyPeriodId = 0);

        Task<ResponseVM> CreateMarketData(MarketDataVM vm);
    }
}
