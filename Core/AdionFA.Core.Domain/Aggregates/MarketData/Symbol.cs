using AdionFA.Core.Domain.Aggregates.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdionFA.Core.Domain.Aggregates.MarketData
{
    [Table(nameof(Symbol))]
    public class Symbol : ReferenceDataBase
    {
        [Key]
        public int SymbolId { get; set; }
    }
}
