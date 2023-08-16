using AdionFA.TransferObject.Base;

namespace AdionFA.TransferObject.Configuration
{
    public class AssemblyBuilderConfigurationDTO : WekaBuilderConfigurationBaseDTO
    {
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
