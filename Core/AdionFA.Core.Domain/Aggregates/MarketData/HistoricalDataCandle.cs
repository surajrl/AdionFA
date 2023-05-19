using AdionFA.Core.Domain.Aggregates.Base;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.MarketData
{
    [Table(nameof(HistoricalDataCandle))]
    public class HistoricalDataCandle : EntityBase
    {
        [Key]
        public int HistoricalDataCandleId { get; set; }

        public int HistoricalDataId { get; set; }

        [ForeignKey(nameof(HistoricalDataId))]
        public HistoricalData HistoricalData { get; set; }

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