using AdionFA.UI.Station.Infrastructure.Model.Base;
using System;

namespace AdionFA.UI.Station.Infrastructure.Model.Market
{
    public class HistoricalDataCandleVM : EntityBaseVM
    {
        public int HistoricalDataDetailId { get; set; }

        public int MarketDataId { get; set; }
        public HistoricalDataVM HistoricalData { get; set; }
        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
    }
}
