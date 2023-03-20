using Adion.FA.Core.Domain.Aggregates.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(MarketDataDetail))]
    public class MarketDataDetail : EntityBase
    {
        #region Properties

        [Key]
        public int MarketDataDetailId { get; set; }

        public int MarketDataId { get; set; }
        [ForeignKey(nameof(MarketDataId))]
        public MarketData MarketData { get; set; }

        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double OpenPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volumen { get; set; }

        #endregion
    }
}
