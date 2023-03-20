using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.ReferenceData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(CurrencyPair))]
    public class CurrencyPair : ReferenceDataBase
    {
        [Key]
        public int CurrencyPairId { get; set; }

        public int CurrencyFromId { get; set; }
        [ForeignKey(nameof(CurrencyFromId))]
        public Currency CurrencyFrom { get; set; }

        public int CurrencyToId { get; set; }
        [ForeignKey(nameof(CurrencyToId))]
        public Currency CurrencyTo { get; set; }
    }
}
