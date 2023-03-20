using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Project
{
    [Table(nameof(ProjectStep))]
    public class ProjectStep : ReferenceDataBase
    {
        [Key]
        public int ProjectStepId { get; set; }
    }
}
