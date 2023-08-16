using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities.Configuration
{
    [Table(nameof(AssemblyBuilderConfiguration))]
    public class AssemblyBuilderConfiguration : WekaBuilderConfigurationBase
    {
        [Key]
        public int AssemblyBuilderConfigurationId { get; set; }

        public int AssemblyNodesUPTarget { get; set; }
        public int AssemblyNodesDOWNTarget { get; set; }
        public int TotalTradesTarget { get; set; }

        public int MinTotalTradesIS { get; set; }

        public decimal MinSuccessRateImprovementIS { get; set; }
        public decimal MinSuccessRateImprovementOS { get; set; }

        public decimal MaxSuccessRateImprovementIS { get; set; }
        public decimal MaxSuccessRateImprovementOS { get; set; }
    }
}
