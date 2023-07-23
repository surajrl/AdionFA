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

        public int TotalInstanceWeka { get; set; }
        public int DepthWeka { get; set; }
        public int TotalDecimalWeka { get; set; }
        public int MinimalSeed { get; set; }
        public int MaximumSeed { get; set; }
        public decimal MaxRatioTree { get; set; }
        public decimal NTotalTree { get; set; }

        // Strategy Builder

        public int SBMinTotalTradesIS { get; set; }
        public decimal SBMinSuccessRatePercentIS { get; set; }

        public int SBMinTotalTradesOS { get; set; }
        public decimal SBMinSuccessRatePercentOS { get; set; }

        public decimal SBMaxSuccessRateVariation { get; set; }

        public bool IsProgressiveness { get; set; }
        public decimal MaxProgressivenessVariation { get; set; }

        public decimal SBMaxCorrelationPercent { get; set; }

        public int SBWinningStrategyUPTarget { get; set; }
        public int SBWinningStrategyDOWNTarget { get; set; }
        public int SBTotalTradesTarget { get; set; }

        // Assembly Builder

        public int ABMinTotalTradesIS { get; set; }
        public decimal ABMinImprovePercent { get; set; }
        public decimal ABWekaMaxRatioTree { get; set; }
        public decimal ABWekaNTotalTree { get; set; }

        public void RestoreConfiguration()
        {
            // Period

            FromDateIS = null;
            ToDateIS = null;

            FromDateOS = null;
            ToDateOS = null;

            // Schedule 

            WithoutSchedule = true;

            // Extractor

            ExtractorMinVariation = 50;

            // MetaTrader

            ExpertAdvisorHost = null;
            ExpertAdvisorPublisherPort = null;
            ExpertAdvisorResponsePort = null;

            // Weka

            TotalInstanceWeka = 1;
            DepthWeka = 6;
            TotalDecimalWeka = 5;
            MinimalSeed = 100;
            MaximumSeed = 1000000;
            MaxRatioTree = (decimal)1.5;
            NTotalTree = 300;

            // Strategy Builder

            SBMinTotalTradesIS = 300;
            SBMinSuccessRatePercentIS = 55;

            SBMinTotalTradesOS = 100;
            SBMinSuccessRatePercentOS = 55;

            SBMaxSuccessRateVariation = 5;

            MaxProgressivenessVariation = 2;
            IsProgressiveness = false;

            SBMaxCorrelationPercent = 2;

            SBWinningStrategyDOWNTarget = 6;
            SBWinningStrategyUPTarget = 6;
            SBTotalTradesTarget = 300;

            // Assembly Builder

            ABMinTotalTradesIS = 300;
            ABMinImprovePercent = 5;
            ABWekaMaxRatioTree = (decimal)1.5;
            ABWekaNTotalTree = 300;
        }
    }
}
