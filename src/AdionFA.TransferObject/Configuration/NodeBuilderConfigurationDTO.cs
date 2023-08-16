using AdionFA.TransferObject.Base;

namespace AdionFA.TransferObject.Configuration
{
    public class NodeBuilderConfigurationDTO : WekaBuilderConfigurationBaseDTO
    {
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
