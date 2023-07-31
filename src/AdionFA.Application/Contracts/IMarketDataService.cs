using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System.Collections.Generic;

namespace AdionFA.Application.Contracts
{
    public interface IMarketDataService
    {
        // Historical Data

        IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph);
        HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph);
        HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId, bool includeGraph);
        ResponseDTO CreateHistoricalData(HistoricalDataDTO historicalDataDTO);

        // Timeframe

        IList<TimeframeDTO> GetAllTimeframe();
        TimeframeDTO GetTimeframe(int timeframeId);

        // Symbol

        IList<SymbolDTO> GetAllSymbol();
        SymbolDTO GetSymbol(int symbolId);
        SymbolDTO GetSymbol(string symbolName);
        ResponseDTO CreateSymbol(SymbolDTO symbol);
    }
}
