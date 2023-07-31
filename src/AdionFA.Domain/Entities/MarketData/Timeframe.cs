using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Timeframe))]
    public class Timeframe : ReferenceDataBase
    {
        [Key]
        public int TimeframeId { get; set; }
    }
}
