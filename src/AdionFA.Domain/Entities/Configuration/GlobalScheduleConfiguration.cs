using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(GlobalScheduleConfiguration))]
    public class GlobalScheduleConfiguration : EntityBase
    {
        [Key]
        public int GlobalScheduleConfigurationId { get; set; }

        public int GlobalConfigurationId { get; set; }
        [ForeignKey(nameof(GlobalConfigurationId))]
        public GlobalConfiguration GlobalConfiguration { get; set; }

        public int MarketRegionId { get; set; }
        [ForeignKey(nameof(MarketRegionId))]
        public MarketRegion MarketRegion { get; set; }

        public int FromTimeInSeconds { get; set; }
        public int ToTimeInSeconds { get; set; }
    }
}
