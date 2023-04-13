using AdionFA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.MarketData
{
    [Table(nameof(CurrencySpread))]
    public class CurrencySpread : ReferenceDataBase
    {
        [Key]
        public int CurrencySpreadId { get; set; }
    }
}
