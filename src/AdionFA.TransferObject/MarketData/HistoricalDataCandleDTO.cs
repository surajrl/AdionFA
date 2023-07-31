using AdionFA.TransferObject.Base;
using System;

namespace AdionFA.TransferObject.MarketData
{
    public class HistoricalDataCandleDTO : EntityBaseDTO
    {
        public int HistoricalDataCandlelId { get; set; }

        public int HistoricalDataId { get; set; }
        public HistoricalDataDTO HistoricalData { get; set; }

        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double Spread { get; set; }
    }
}