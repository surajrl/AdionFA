using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Market;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Project
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

        public int? MarketDataId { get; set; }
        [ForeignKey(nameof(MarketDataId))]
        public MarketData MarketData { get; set; }

        public bool IsFavorite { get; set; }
        public string WorkspacePath { get; set; }

        #endregion

        #region Navegation Properties

        public ICollection<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }
        
        #endregion
    }
}
