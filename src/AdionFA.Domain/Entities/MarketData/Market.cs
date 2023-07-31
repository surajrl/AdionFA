using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Market))]
    public class Market : ReferenceDataBase
    {
        [Key]
        public int MarketId { get; set; }
    }
}
