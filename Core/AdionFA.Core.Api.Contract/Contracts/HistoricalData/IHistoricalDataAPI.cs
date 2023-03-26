using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Market;
using System.Collections.Generic;

namespace AdionFA.Core.API.Contracts.Markets
{
    public interface IHistoricalDataAPI
    {
        // Historical Data

        IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false);
        HistoricalDataDTO GetHistoricalData(int marketDataId, bool includeGraph = false);
        HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int currencyPeriodId);
        ResponseDTO CreateHistoricalData(HistoricalDataDTO market);

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
