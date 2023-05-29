using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Aggregates.MarketData;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Common
{
    [Table(nameof(ScheduleConfiguration))]
    public class ScheduleConfiguration : TimeSensitiveBase
    {
        [Key]
        public int ScheduleConfigurationId { get; set; }

        public int ConfigurationId { get; set; }
        [ForeignKey(nameof(ConfigurationId))]
        public Configuration Configuration { get; set; }

        public int? MarketRegionId { get; set; }
        [ForeignKey(nameof(MarketRegionId))]
        public MarketRegion MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
