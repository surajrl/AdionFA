using AdionFA.UI.Station.Infrastructure.Model.Base;
using System;

namespace AdionFA.UI.Station.Infrastructure.Model.Market
{
    public class HistoricalDataDetailVM : EntityBaseVM
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public int HistoricalDataDetailId { get; set; }

        /// <summary>
        /// Market Data ID and Market Data that these details belong to
        /// </summary>
        public int MarketDataId { get; set; }
        public HistoricalDataVM HistoricalData { get; set; }

        /// <summary>
        /// Candle details
        /// </summary>
        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double OpenPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volume { get; set; }
    }
}
