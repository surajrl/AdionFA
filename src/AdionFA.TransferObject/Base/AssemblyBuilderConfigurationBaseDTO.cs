namespace AdionFA.TransferObject.Base
{
    public class AssemblyBuilderConfigurationBaseDTO : WekaBuilderConfigurationBaseDTO
    {
        public int MinTotalTradesIS { get; set; }

        public decimal MinSuccessRateImprovementIS { get; set; }
        public decimal MinSuccessRateImprovementOS { get; set; }

        public decimal MaxSuccessRateImprovementIS { get; set; }
        public decimal MaxSuccessRateImprovementOS { get; set; }
    }
}
