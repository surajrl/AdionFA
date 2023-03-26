using AdionFA.Core.Domain.Aggregates.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Market
{
    [Table(nameof(HistoricalDataDetail))]
    public class HistoricalDataDetail : EntityBase
    {
        [Key]
        public int HistoricalDataDetailId { get; set; }
        
        public int HistoricalDataId { get; set; }
        [ForeignKey(nameof(HistoricalDataId))]
        public HistoricalData HistoricalData { get; set; }

        public DateTime StartDate { get; set; }
        public long StartTime { get; set; }
        public double OpenPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volume { get; set; }
    }
}
