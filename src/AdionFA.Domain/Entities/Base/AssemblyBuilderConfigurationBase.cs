namespace AdionFA.Domain.Entities.Base
{
    public class AssemblyBuilderConfigurationBase : WekaBuilderConfigurationBase
    {
        public int MinTotalTradesIS { get; set; }

        public decimal MinSuccessRateImprovementIS { get; set; }
        public decimal MinSuccessRateImprovementOS { get; set; }

        public decimal MaxSuccessRateImprovementIS { get; set; }
        public decimal MaxSuccessRateImprovementOS { get; set; }
    }
}
