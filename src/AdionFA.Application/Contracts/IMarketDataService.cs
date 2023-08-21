using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System.Collections.Generic;

namespace AdionFA.Application.Contracts
{
    public interface IMarketDataService
    {
        // Historical data

        IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph);
        HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph);
        HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId, bool includeGraph);
        ResponseDTO CreateHistoricalData(HistoricalDataDTO historicalDataDTO);


        // Historical data candles

        IList<HistoricalDataCandleDTO> GetHistoricalDataCandles(int historicalDataId);

        // Timeframe

        IList<TimeframeDTO> GetAllTimeframe();
        TimeframeDTO GetTimeframe(int timeframeId);

        // Symbol

        IList<SymbolDTO> GetAllSymbol();
        SymbolDTO GetSymbol(int symbolId);
        ResponseDTO CreateSymbol(SymbolDTO symbol);
    }
}
