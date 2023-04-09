using AdionFA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.MarketData
{
    [Table(nameof(Market))]
    public class Market : ReferenceDataBase
    {
        [Key]
        public int MarketId { get; set; }
    }
}
