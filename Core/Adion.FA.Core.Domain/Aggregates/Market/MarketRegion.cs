using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(MarketRegion))]
    public class MarketRegion : ReferenceDataBase
    {
        [Key]
        public int MarketRegionId { get; set; }
    }
}
