using System;

namespace AdionFA.TransferObject.Base
{
    public abstract class ConfigurationBaseDTO : EntityBaseDTO
    {
        // Period

        public DateTime? FromDateIS { get; set; }
        public DateTime? ToDateIS { get; set; }

        public DateTime? FromDateOS { get; set; }
        public DateTime? ToDateOS { get; set; }

        // Schedule

        public bool WithoutSchedule { get; set; }

        // Extractor

        public int ExtractorMinVariation { get; set; }

        // MetaTrader

        public string ExpertAdvisorHost { get; set; }
        public string ExpertAdvisorResponsePort { get; set; }
        public string ExpertAdvisorPublisherPort { get; set; }

        // Weka

        public int MinimalSeed { get; set; }
        public int MaximumSeed { get; set; }
        public int TotalDecimalWeka { get; set; }

        // Builder

        public bool IsProgressiveness { get; set; }
        public decimal MaxProgressivenessVariation { get; set; }

        public decimal MaxCorrelationPercent { get; set; }

        public NodeBuilderConfigurationBaseDTO NodeBuilderConfiguration { get; set; }

        public AssemblyBuilderConfigurationBaseDTO AssemblyBuilderConfiguration { get; set; }

        public CrossingBuilderConfigurationBaseDTO CrossingBuilderConfiguration { get; set; }
    }
}
