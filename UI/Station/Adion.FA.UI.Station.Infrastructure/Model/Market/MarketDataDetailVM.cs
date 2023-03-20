using Adion.FA.UI.Station.Infrastructure.Model.Base;
using System;

namespace Adion.FA.UI.Station.Infrastructure.Model.Market
{
    public class MarketDataDetailVM : EntityBaseVM
    {
        public int MarketDataDetailId { get; set; }

        public int MarketDataId { get; set; }
        public MarketDataVM MarketData { get; set; }

        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double OpenPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volumen { get; set; }
    }
}
