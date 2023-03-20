using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Organization
{

    [Table(nameof(LineOfBusiness))]
    public class LineOfBusiness : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineOfBusinessId { get; set; }
    }
}
