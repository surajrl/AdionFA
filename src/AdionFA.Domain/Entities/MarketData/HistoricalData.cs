using AdionFA.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(HistoricalData))]
    public class HistoricalData : TimeSensitiveBase
    {
        [Key]
        public int HistoricalDataId { get; set; }

        public int MarketId { get; set; }
        [ForeignKey(nameof(MarketId))]
        public Market Market { get; set; }

        public int SymbolId { get; set; }
        [ForeignKey(nameof(SymbolId))]
        public Symbol Symbol { get; set; }

        public int TimeframeId { get; set; }
        [ForeignKey(nameof(TimeframeId))]
        public Timeframe Timeframe { get; set; }

        public ICollection<HistoricalDataCandle> HistoricalDataCandles { get; set; }
    }
}
