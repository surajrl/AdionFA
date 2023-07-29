using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectConfigurationDTO : ConfigurationBaseDTO
    {
        public ProjectConfigurationDTO()
        {
        }

        public ProjectConfigurationDTO(ConfigurationBaseDTO configurationBaseDTO)
        {
            // Period

            FromDateIS = configurationBaseDTO.FromDateIS;
            ToDateIS = configurationBaseDTO.ToDateIS;

            FromDateOS = configurationBaseDTO.FromDateOS;
            ToDateOS = configurationBaseDTO.ToDateOS;

            WithoutSchedule = configurationBaseDTO.WithoutSchedule;

            // Extractor

            ExtractorMinVariation = configurationBaseDTO.ExtractorMinVariation;

            // MetaTrader

            ExpertAdvisorHost = configurationBaseDTO.ExpertAdvisorHost;
            ExpertAdvisorPublisherPort = configurationBaseDTO.ExpertAdvisorPublisherPort;
            ExpertAdvisorResponsePort = configurationBaseDTO.ExpertAdvisorResponsePort;

            // Weka

            TotalInstanceWeka = configurationBaseDTO.TotalInstanceWeka;
            DepthWeka = configurationBaseDTO.DepthWeka;
            TotalDecimalWeka = configurationBaseDTO.TotalDecimalWeka;
            MinimalSeed = configurationBaseDTO.MinimalSeed;
            MaximumSeed = configurationBaseDTO.MaximumSeed;
            MaxRatioTree = configurationBaseDTO.MaxRatioTree;
            NTotalTree = configurationBaseDTO.NTotalTree;

            // Strategy Builder

            SBMinTotalTradesIS = configurationBaseDTO.SBMinTotalTradesIS;
            SBMinSuccessRatePercentIS = configurationBaseDTO.SBMinSuccessRatePercentIS;

            SBMinTotalTradesOS = configurationBaseDTO.SBMinTotalTradesOS;
            SBMinSuccessRatePercentOS = configurationBaseDTO.SBMinSuccessRatePercentOS;

            SBMaxSuccessRateVariation = configurationBaseDTO.SBMaxSuccessRateVariation;

            IsProgressiveness = configurationBaseDTO.IsProgressiveness;
            MaxProgressivenessVariation = configurationBaseDTO.MaxProgressivenessVariation;

            SBMaxCorrelationPercent = configurationBaseDTO.SBMaxCorrelationPercent;

            SBWinningStrategyDOWNTarget = configurationBaseDTO.SBWinningStrategyDOWNTarget;
            SBWinningStrategyUPTarget = configurationBaseDTO.SBWinningStrategyUPTarget;
            SBTotalTradesTarget = configurationBaseDTO.SBTotalTradesTarget;

            // Assembly Builder

            ABMinTotalTradesIS = configurationBaseDTO.ABMinTotalTradesIS;
            ABMinImprovePercent = configurationBaseDTO.ABMinImprovePercent;
            ABWekaMaxRatioTree = configurationBaseDTO.ABWekaMaxRatioTree;
            ABWekaNTotalTree = configurationBaseDTO.ABWekaNTotalTree;
        }

        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }

        public IList<ProjectScheduleConfigurationDTO> ProjectScheduleConfigurations { get; set; }
    }
}
