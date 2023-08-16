using AdionFA.Domain.Entities.Base;
using AdionFA.Domain.Entities.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(ProjectConfiguration))]
    public class ProjectConfiguration : ConfigurationBase
    {
        [Key]
        public int ProjectConfigurationId { get; set; }

        // Project

        public int ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        // Builder configuration

        public int NodeBuilderConfigurationId { get; set; }
        [ForeignKey(nameof(NodeBuilderConfigurationId))]
        public NodeBuilderConfiguration NodeBuilderConfiguration { get; set; }

        public int AssemblyBuilderConfigurationId { get; set; }
        [ForeignKey(nameof(AssemblyBuilderConfigurationId))]
        public AssemblyBuilderConfiguration AssemblyBuilderConfiguration { get; set; }

        public int CrossingBuilderConfigurationId { get; set; }
        [ForeignKey(nameof(CrossingBuilderConfigurationId))]
        public CrossingBuilderConfiguration CrossingBuilderConfiguration { get; set; }

        // Navigation

        public ICollection<ProjectScheduleConfiguration> ProjectScheduleConfigurations { get; set; }

        public void RestoreProjectConfiguration()
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

            NodeBuilderConfiguration = new NodeBuilderConfiguration
            {
                NodesUPTarget = 6,
                NodesDOWNTarget = 6,
                TotalTradesTarget = 8000,

                MinTotalTradesIS = 200,
                MinSuccessRatePercentIS = (decimal)40.0,

                MinTotalTradesOS = 100,
                MinSuccessRatePercentOS = (decimal)40.0,

                MaxSuccessRateVariation = (decimal)5.0,

                WekaNTotal = 300,
                WekaStartDepth = 4,
                WekaEndDepth = 12,
                WekaMaxRatio = (decimal)1.5,
            };

            // Assembly builder

            AssemblyBuilderConfiguration = new AssemblyBuilderConfiguration
            {
                AssemblyNodesUPTarget = 6,
                AssemblyNodesDOWNTarget = 6,
                TotalTradesTarget = 8000,

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

            CrossingBuilderConfiguration = new CrossingBuilderConfiguration
            {
                StrategyNodesUPTarget = 6,
                StrategyNodesDOWNTarget = 6,
                TotalTradesTarget = 8000,

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
