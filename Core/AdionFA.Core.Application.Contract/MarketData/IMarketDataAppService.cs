using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System.Collections.Generic;

namespace AdionFA.Core.Application.Contracts.MarketData
{
    public interface IMarketDataAppService : IAppContractBase
    {
        // Historical Data

        IList<HistoricalDataDTO> GetAllHistoricalData(bool includeGraph = false);
        HistoricalDataDTO GetHistoricalData(int historicalDataId, bool includeGraph = false);
        HistoricalDataDTO GetHistoricalData(int marketId, int symbolId, int timeframeId);
        ResponseDTO CreateHistoricalData(HistoricalDataDTO historicalData);

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
