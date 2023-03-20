using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Market;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Project
{
    [Table(nameof(ProjectScheduleConfiguration))]
    public class ProjectScheduleConfiguration : TimeSensitiveBase
    {
        [Key]
        public int ProjectScheduleConfigurationId { get; set; }

        public int ProjectConfigurationId { get; set; }
        [ForeignKey(nameof(ProjectConfigurationId))]
        public ProjectConfiguration ProjectConfiguration { get; set; }

        public int? MarketRegionId { get; set; }
        [ForeignKey(nameof(MarketRegionId))]
        public MarketRegion MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
