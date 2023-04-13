using AdionFA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.ReferenceData
{
    [Table(nameof(Currency))]
    public class Currency : ReferenceDataBase
    {
        [Key]
        public int CurrencyId { get; set; }
    }
}
