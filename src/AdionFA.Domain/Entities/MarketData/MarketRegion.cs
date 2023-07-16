using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(MarketRegion))]
    public class MarketRegion : ReferenceDataBase
    {
        [Key]
        public int MarketRegionId { get; set; }
    }
}
