using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System.Collections.Generic;

namespace AdionFA.API.Contracts
{
    public interface IMarketDataAPI
    {
        // Historical Data

        IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false);
        HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph = false);
        HistoricalDataDTO GetHistoricalData(int historicalDataId, int symbolId, int timeframeId);
        ResponseDTO CreateHistoricalData(HistoricalDataDTO historicalData);
        ResponseDTO UpdateHistoricalData(HistoricalDataDTO historicalData);

        // Timeframe

        IList<TimeframeDTO> GetAllTimeframe(bool includeGraph = false);
        TimeframeDTO GetTimeframe(int timeframeId);

        // Symbol

        IList<SymbolDTO> GetAllSymbol(bool includeGraph = false);
        SymbolDTO GetSymbol(int symbolId);
        SymbolDTO GetSymbol(string symbolName);
        ResponseDTO CreateSymbol(SymbolDTO symbol);
    }
}
