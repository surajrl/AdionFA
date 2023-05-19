using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface IMarketDataServiceAgent
    {
        // Historical Data

        Task<IList<HistoricalDataVM>> GetAllHistoricalData(bool includeGraph = false);

        Task<HistoricalDataVM> GetHistoricalData(int historicalDataId, bool includeGraph = false);

        Task<HistoricalDataVM> GetHistoricalData(int marketId = 0, int symbolId = 0, int timeframeId = 0);

        Task<ResponseVM> CreateHistoricalData(HistoricalDataVM vm);

        // Timeframe

        Task<IList<TimeframeVM>> GetAllTimeframe(bool includeGraph = false);

        Task<TimeframeVM> GetTimeframe(int timeframeId);

        // Symbol

        Task<IList<SymbolVM>> GetAllSymbol(bool includeGraph = false);

        Task<SymbolVM> GetSymbol(int symbolId);

        Task<SymbolVM> GetSymbol(string symbolName);

        Task<ResponseVM> CreateSymbol(SymbolVM symbol);
    }
}