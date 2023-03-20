using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(CurrencyPeriod))]
    public class CurrencyPeriod : ReferenceDataBaseExtended
    {
        [Key]
        public int CurrencyPeriodId { get; set; }
    }
}
