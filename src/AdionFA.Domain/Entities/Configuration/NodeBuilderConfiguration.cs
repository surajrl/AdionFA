using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities.Configuration
{
    [Table(nameof(NodeBuilderConfiguration))]
    public class NodeBuilderConfiguration : WekaBuilderConfigurationBase
    {
        [Key]
        public int NodeBuilderConfigurationId { get; set; }

        public int NodesUPTarget { get; set; }
        public int NodesDOWNTarget { get; set; }
        public int TotalTradesTarget { get; set; }

        public int MinTotalTradesIS { get; set; }
        public decimal MinSuccessRatePercentIS { get; set; }

        public int MinTotalTradesOS { get; set; }
        public decimal MinSuccessRatePercentOS { get; set; }

        public decimal MaxSuccessRateVariation { get; set; }

    }
}
