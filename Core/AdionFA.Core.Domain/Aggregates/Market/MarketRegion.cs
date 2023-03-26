using AdionFA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Market
{
    [Table(nameof(MarketRegion))]
    public class MarketRegion : ReferenceDataBase
    {
        [Key]
        public int MarketRegionId { get; set; }
    }
}
