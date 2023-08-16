using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities.Configuration
{
    [Table(nameof(CrossingBuilderConfiguration))]
    public class CrossingBuilderConfiguration : WekaBuilderConfigurationBase
    {
        [Key]
        public int CrossingBuilderConfigurationId { get; set; }

        public int StrategyNodesUPTarget { get; set; }
        public int StrategyNodesDOWNTarget { get; set; }
        public int TotalTradesTarget { get; set; }

        public decimal MinSuccessRateImprovementIS { get; set; }
        public decimal MinSuccessRateImprovementOS { get; set; }

        public decimal MaxSuccessRateImprovementIS { get; set; }
        public decimal MaxSuccessRateImprovementOS { get; set; }
    }
}
