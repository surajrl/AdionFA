using AdionFA.TransferObject.Base;

namespace AdionFA.TransferObject.Configuration
{
    public class CrossingBuilderConfigurationDTO : WekaBuilderConfigurationBaseDTO
    {
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
