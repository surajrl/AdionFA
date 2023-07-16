using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Symbol))]
    public class Symbol : ReferenceDataBase
    {
        [Key]
        public int SymbolId { get; set; }
    }
}
