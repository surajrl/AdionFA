using AdionFA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.MarketData
{
    [Table(nameof(Timeframe))]
    public class Timeframe : ReferenceDataBaseExtended
    {
        [Key]
        public int TimeframeId { get; set; }
    }
}
