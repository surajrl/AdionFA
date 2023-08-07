namespace AdionFA.TransferObject.Base
{
    public class CrossingBuilderConfigurationBaseDTO : WekaBuilderConfigurationBaseDTO
    {
        public decimal MinSuccessRateImprovementIS { get; set; }
        public decimal MinSuccessRateImprovementOS { get; set; }

        public decimal MaxSuccessRateImprovementIS { get; set; }
        public decimal MaxSuccessRateImprovementOS { get; set; }
    }
}
