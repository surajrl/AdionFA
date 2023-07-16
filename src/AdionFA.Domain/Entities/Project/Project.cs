using AdionFA.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Project))]
    public class Project : EntityBase
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        // Navigation

        public ICollection<ProjectConfiguration> ProjectConfigurations { get; set; }
    }
}
