using Adion.FA.Core.Domain.Aggregates.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Project
{
    [Table(nameof(ProjectGlobalConfiguration))]
    public class ProjectGlobalConfiguration : ConfigurationBase
    {
        #region Properties

        [Key]
        public int ProjectGlobalConfigurationId { get; set; }

        public ICollection<ProjectGlobalScheduleConfiguration> ProjectGlobalScheduleConfigurations { get; set; }

        #endregion
    }
}
