using AdionFA.TransferObject.MarketData;
using System;

namespace AdionFA.TransferObject.Base
{
    public class ConfigurationBaseDTO : TimeSensitiveBaseDTO
    {
        // Period

        public DateTime? FromDateIS { get; set; }
        public DateTime? ToDateIS { get; set; }

        public DateTime? FromDateOS { get; set; }
        public DateTime? ToDateOS { get; set; }

        public bool WithoutSchedule { get; set; }

        // Historical Data Information

        public int SymbolId { get; set; }
        public SymbolDTO Symbol { get; set; }

        public int TimeframeId { get; set; }
        public TimeframeDTO Timeframe { get; set; }

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

        public int SBMinTransactionsIS { get; set; } // TODO: Change property name to SBMinWinningTradesIS for clarity
        public decimal SBMinSuccessRatePercentIS { get; set; }

        public int SBMinTransactionsOS { get; set; } // TODO: Change property name to SBMinWinningTradesOS for clarity
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
