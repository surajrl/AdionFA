using AdionFA.Core.Domain.Aggregates.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.MarketData
{
    [Table(nameof(Symbol))]
    public class Symbol : ReferenceDataBase
    {
        [Key]
        public int SymbolId { get; set; }
    }
}
