namespace AdionFA.TransferObject.Base
{
    public class NodeBuilderConfigurationBaseDTO : WekaBuilderConfigurationBaseDTO
    {
        public int MinTotalTradesIS { get; set; }
        public decimal MinSuccessRatePercentIS { get; set; }

        public int MinTotalTradesOS { get; set; }
        public decimal MinSuccessRatePercentOS { get; set; }

        public decimal MaxSuccessRateVariation { get; set; }

        public int WinningNodesUPTarget { get; set; }
        public int WinningNodesDOWNTarget { get; set; }
        public int TotalTradesTarget { get; set; }

    }
}
