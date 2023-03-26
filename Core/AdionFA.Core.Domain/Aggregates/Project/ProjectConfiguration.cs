using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Aggregates.Market;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Project
{
    [Table(nameof(ProjectConfiguration))]
    public class ProjectConfiguration : ConfigurationBase
    {
        #region Properties

        [Key]
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public int? HistoricalDataId { get; set; }
        [ForeignKey(nameof(HistoricalDataId))]
        public HistoricalData HistoricalData { get; set; }

        public bool IsFavorite { get; set; }
        public string WorkspacePath { get; set; }

        #endregion

        #region Navegation Properties

        public ICollection<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }
        
        #endregion
    }
}
