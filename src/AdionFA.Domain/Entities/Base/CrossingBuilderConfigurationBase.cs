namespace AdionFA.Domain.Entities.Base
{
    public class CrossingBuilderConfigurationBase : WekaBuilderConfigurationBase
    {
        public decimal MinSuccessRateImprovementIS { get; set; }
        public decimal MinSuccessRateImprovementOS { get; set; }

        public decimal MaxSuccessRateImprovementIS { get; set; }
        public decimal MaxSuccessRateImprovementOS { get; set; }
    }
}
