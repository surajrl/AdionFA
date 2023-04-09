using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.MarketData
{
    public class HistoricalDataDTO : TimeSensitiveBaseDTO
    {
        public int HistoricalDataId { get; set; }

        public int MarketId { get; set; }
        public MarketDTO Market { get; set; }

        public int SymbolId { get; set; }
        public SymbolDTO Symbol { get; set; }

        public int TimeframeId { get; set; }
        public TimeframeDTO Timeframe { get; set; }

        public IList<HistoricalDataDetailDTO> HistoricalDataDetails { get; set; }
    }
}