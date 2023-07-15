using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface IMarketDataServiceAgent
    {
        // Historical Data

        Task<IList<HistoricalDataVM>> GetAllHistoricalDataAsync(bool includeGraph = false);

        Task<HistoricalDataVM> GetHistoricalDataAsync(int historicalDataId, bool includeGraph = false);

        Task<HistoricalDataVM> GetHistoricalDataAsync(int marketId = 0, int symbolId = 0, int timeframeId = 0);

        Task<ResponseVM> CreateHistoricalDataAsync(HistoricalDataVM vm);

        // Timeframe

        Task<IList<TimeframeVM>> GetAllTimeframeAsync(bool includeGraph = false);

        Task<TimeframeVM> GetTimeframeAsync(int timeframeId);

        // Symbol

        Task<IList<SymbolVM>> GetAllSymbolAsync(bool includeGraph = false);

        Task<SymbolVM> GetSymbolAsync(int symbolId);

        Task<SymbolVM> GetSymbolAsync(string symbolName);

        Task<ResponseVM> CreateSymbolAsync(SymbolVM symbol);
    }
}