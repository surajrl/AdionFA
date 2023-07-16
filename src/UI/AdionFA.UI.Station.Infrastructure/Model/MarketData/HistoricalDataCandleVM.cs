using AdionFA.UI.Infrastructure.Model.Base;
using System;

namespace AdionFA.UI.Infrastructure.Model.MarketData
{
    public class HistoricalDataCandleVM : EntityBaseVM
    {
        public int HistoricalDataCandleId { get; set; }

        public int HistoricalDataId { get; set; }
        public HistoricalDataVM HistoricalData { get; set; }

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