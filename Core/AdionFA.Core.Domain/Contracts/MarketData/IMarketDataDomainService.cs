using AdionFA.Core.Domain.Aggregates.MarketData;

using System.Collections.Generic;

namespace AdionFA.Core.Domain.Contracts.MarketData
{
    public interface IMarketDataDomainService
    {
        // Historical Data

        IList<HistoricalData> GetAllHistoricalData(bool includeGraph = false);
        HistoricalData GetHistoricalData(int historicalDataId, bool includeGraph = false);
        HistoricalData GetHistoricalData(int marketId, int currencyPairId, int timeframeId);
        int CreateHistoricalData(HistoricalData historicalData);
        int UpdateHistoricalData(HistoricalData historicalData);

        // Timeframe

        IList<Timeframe> GetAllTimeframe(bool includeGraph = false);
        Timeframe GetTimeframe(int timeframeId);

        // Symbol

        IList<Symbol> GetAllSymbol(bool includeGraph = false);
        Symbol GetSymbol(int symbolId);
        Symbol GetSymbol(string symbolName);
        int? CreateSymbol(Symbol symbol);
    }
}
