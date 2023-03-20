using Adion.FA.TransferObject.Base;
using System;

namespace Adion.FA.TransferObject.Market
{
    public class MarketDataDetailDTO : EntityBaseDTO
    {
        public int MarketDataDetailId { get; set; }

        public int MarketDataId { get; set; }
        public MarketDataDTO MarketData { get; set; }

        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double OpenPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volumen { get; set; }
    }
}
