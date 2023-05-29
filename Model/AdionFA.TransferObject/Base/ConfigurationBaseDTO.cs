using AdionFA.TransferObject.MarketData;
using System;

namespace AdionFA.TransferObject.Base
{
    public class ConfigurationBaseDTO : TimeSensitiveBaseDTO
    {
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

        // Weka

        public int TotalInstanceWeka { get; set; }
        public int DepthWeka { get; set; }
        public int TotalDecimalWeka { get; set; }
        public int MinimalSeed { get; set; }
        public int MaximumSeed { get; set; }
        public decimal MaxRatioTree { get; set; }
        public decimal NTotalTree { get; set; }

        // Strategy Builder

        public int SBMinTransactionsIS { get; set; }
        public decimal SBMinPercentSuccessIS { get; set; }

        public int SBMinTransactionsOS { get; set; }
        public decimal SBMinPercentSuccessOS { get; set; }

        public decimal SBMaxTransactionsVariation { get; set; }

        public bool IsProgressiveness { get; set; }
        public decimal Progressiveness { get; set; }

        public decimal SBMaxPercentCorrelation { get; set; }

        public int SBWinningStrategyUPTarget { get; set; }
        public int SBWinningStrategyDOWNTarget { get; set; }
        public int SBTransactionsTarget { get; set; }

        // Assembled Builder

        public int ABTransactionsTarget { get; set; }
        public decimal ABMinImprovePercent { get; set; }
    }
}
