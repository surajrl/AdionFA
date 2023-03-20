using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.ReferenceData
{
    [Table(nameof(TimeZone))]
    public class TimeZone : ReferenceDataBase
    {
        [Key]
        public int TimeZoneId { get; set; }
    }
}
