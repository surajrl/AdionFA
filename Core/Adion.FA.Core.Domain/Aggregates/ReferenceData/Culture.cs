using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.ReferenceData
{
    [Table(nameof(Culture))]
    public class Culture : ReferenceDataBase
    {
        [Key]
        public int CultureId { get; set; }
    }
}
