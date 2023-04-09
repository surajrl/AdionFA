using AdionFA.TransferObject.Base;
using System;

namespace AdionFA.TransferObject.MarketData
{
    public class HistoricalDataDetailDTO : EntityBaseDTO
    {
        public int HistoricalDataDetailId { get; set; }

        public int MarketDataId { get; set; }
        public HistoricalDataDTO HistoricalData { get; set; }

        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double OpenPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volume { get; set; }
    }
}
