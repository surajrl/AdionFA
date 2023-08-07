using System;

namespace AdionFA.Domain.Entities.Base
{
    public abstract class ConfigurationBase : EntityBase
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

        // Builder general

        public bool IsProgressiveness { get; set; }
        public decimal MaxProgressivenessVariation { get; set; }

        public decimal MaxCorrelationPercent { get; set; }

        // Node builder

        public NodeBuilderConfigurationBase NodeBuilderConfiguration { get; set; }

        // Assembly builder

        public AssemblyBuilderConfigurationBase AssemblyBuilderConfiguration { get; set; }

        // Crossing builder

        public CrossingBuilderConfigurationBase CrossingBuilderConfiguration { get; set; }

        public void RestoreConfiguration()
        {
            // Period

            FromDateIS = null;
            ToDateIS = null;

            FromDateOS = null;
            ToDateOS = null;

            // Schedule 

            WithoutSchedule = true;

            // MetaTrader

            ExpertAdvisorHost = "192.168.1.35";
            ExpertAdvisorPublisherPort = "5551";
            ExpertAdvisorResponsePort = "5550";

            // Extractor

            ExtractorMinVariation = 50;

            // MetaTrader

            ExpertAdvisorHost = null;
            ExpertAdvisorPublisherPort = null;
            ExpertAdvisorResponsePort = null;

            // Weka

            TotalDecimalWeka = 5;
            MinimalSeed = 100;
            MaximumSeed = 1000000;

            // Builder general

            IsProgressiveness = false;
            MaxProgressivenessVariation = (decimal)2.0;

            MaxCorrelationPercent = (decimal)2.0;

            // Node builder

            NodeBuilderConfiguration = new NodeBuilderConfigurationBase
            {
                MinTotalTradesIS = 200,
                MinSuccessRatePercentIS = (decimal)40.0,

                MinTotalTradesOS = 100,
                MinSuccessRatePercentOS = (decimal)40.0,

                MaxSuccessRateVariation = (decimal)5.0,

                WinningNodesUPTarget = 6,
                WinningNodesDOWNTarget = 6,
                TotalTradesTarget = 100,

                WekaNTotal = 300,
                WekaStartDepth = 4,
                WekaEndDepth = 12,
                WekaMaxRatio = (decimal)1.5,
            };

            // Assembly builder

            AssemblyBuilderConfiguration = new AssemblyBuilderConfigurationBase
            {
                MinTotalTradesIS = 100,

                MinSuccessRateImprovementIS = (decimal)2.0,
                MinSuccessRateImprovementOS = (decimal)2.0,

                MaxSuccessRateImprovementIS = (decimal)4.0,
                MaxSuccessRateImprovementOS = (decimal)4.0,

                WekaNTotal = 300,
                WekaStartDepth = 1,
                WekaEndDepth = 6,
                WekaMaxRatio = (decimal)1.5,
            };

            // Crossing builder

            CrossingBuilderConfiguration = new CrossingBuilderConfigurationBase
            {
                MinSuccessRateImprovementIS = (decimal)2.0,
                MinSuccessRateImprovementOS = (decimal)2.0,

                MaxSuccessRateImprovementIS = (decimal)4.0,
                MaxSuccessRateImprovementOS = (decimal)4.0,

                WekaNTotal = 300,
                WekaStartDepth = 1,
                WekaEndDepth = 6,
                WekaMaxRatio = (decimal)1.5,
            };
        }
    }
}
