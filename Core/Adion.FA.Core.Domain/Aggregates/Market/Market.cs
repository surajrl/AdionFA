using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(Market))]
    public class Market : ReferenceDataBase
    {
        [Key]
        public int MarketId { get; set; }
    }
}
