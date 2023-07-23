using AdionFA.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(ProjectConfiguration))]
    public class ProjectConfiguration : ConfigurationBase
    {
        [Key]
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        // Navigation

        public ICollection<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }
    }
}
