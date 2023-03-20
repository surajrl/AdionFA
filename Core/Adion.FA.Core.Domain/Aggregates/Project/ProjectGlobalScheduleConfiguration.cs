using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Market;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Project
{
    [Table(nameof(ProjectGlobalScheduleConfiguration))]
    public class ProjectGlobalScheduleConfiguration : TimeSensitiveBase
    {
        #region Properties

        [Key]
        public int ProjectGlobalScheduleConfigurationId { get; set; }

        public int ProjectGlobalConfigurationId { get; set; }
        [ForeignKey(nameof(ProjectGlobalConfigurationId))]
        public ProjectGlobalConfiguration ProjectGlobalConfiguration { get; set; }

        public int? MarketRegionId { get; set; }
        [ForeignKey(nameof(MarketRegionId))]
        public MarketRegion MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }

        #endregion
    }
}
