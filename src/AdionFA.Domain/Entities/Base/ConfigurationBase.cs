using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities.Base
{
    public class ConfigurationBase : TimeSensitiveBase
    {
        // Period

        public DateTime? FromDateIS { get; set; }
        public DateTime? ToDateIS { get; set; }

        public DateTime? FromDateOS { get; set; }
        public DateTime? ToDateOS { get; set; }

        public bool WithoutSchedule { get; set; }

        // Historical Data Information

        public int SymbolId { get; set; }

        [ForeignKey(nameof(SymbolId))]
        public Symbol Symbol { get; set; }

        public int TimeframeId { get; set; }

        [ForeignKey(nameof(TimeframeId))]
        public Timeframe Timeframe { get; set; }

        // Extractor

        public int ExtractorMinVariation { get; set; }

        // Weka

        public int TotalInstanceWeka { get; set; }
        public int DepthWeka { get; set; }
        public int TotalDecimalWeka { get; set; }
        public int MinimalSeed { get; set; }
        public int MaximumSeed { get; set; }
        public decimal MaxRatioTree { get; set; }
        public decimal NTotalTree { get; set; }

        // MetaTrader

        public string ExpertAdvisorHost { get; set; }
        public string ExpertAdvisorResponsePort { get; set; }
        public string ExpertAdvisorPublisherPort { get; set; }

        // Strategy Builder

        public int SBMinTransactionsIS { get; set; }
        public decimal SBMinSuccessRatePercentIS { get; set; }

        public int SBMinTransactionsOS { get; set; }
        public decimal SBMinSuccessRatePercentOS { get; set; }

        public decimal SBMaxSuccessRateVariation { get; set; }

        public bool IsProgressiveness { get; set; }
        public decimal MaxProgressivenessVariation { get; set; }

        public decimal SBMaxCorrelationPercent { get; set; }

        public int SBWinningStrategyUPTarget { get; set; }
        public int SBWinningStrategyDOWNTarget { get; set; }
        public int SBTransactionsTarget { get; set; }

        // Assembly Builder

        public int ABTransactionsTarget { get; set; }
        public decimal ABMinImprovePercent { get; set; }
        public decimal ABWekaMaxRatioTree { get; set; }
        public decimal ABWekaNTotalTree { get; set; }
    }
}
