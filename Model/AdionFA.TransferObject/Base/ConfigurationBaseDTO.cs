using AdionFA.TransferObject.Market;
using System;

namespace AdionFA.TransferObject.Base
{
    public class ConfigurationBaseDTO : TimeSensitiveBaseDTO
    {
        // Extractor

        public int Variation { get; set; }

        // Period

        public DateTime? FromDateIS { get; set; }
        public DateTime? ToDateIS { get; set; }

        public DateTime? FromDateOS { get; set; }
        public DateTime? ToDateOS { get; set; }

        // Schedule

        public bool WithoutSchedule { get; set; }

        // Historical Data Information

        public int SymbolId { get; set; }
        public SymbolDTO Symbol { get; set; }

        public int TimeframeId { get; set; }
        public TimeframeDTO Timeframe { get; set; }

        public int CurrencySpreadId { get; set; }
        public CurrencySpreadDTO CurrencySpread { get; set; }

        // Weka

        public int TotalInstanceWeka { get; set; }

        public int DepthWeka { get; set; }
        public int MinAdjustDepthWeka { get; set; }

        public int TotalDecimalWeka { get; set; }
        public int MinimalSeed { get; set; }
        public int MaximumSeed { get; set; }

        public decimal MaxRatioTree { get; set; }
        public decimal MinAdjustMaxRatioTree { get; set; }

        public decimal NTotalTree { get; set; }
        public decimal MinAdjustNTotalTree { get; set; }

        // Strategy Builder

        public int MinTransactionCountIS { get; set; }
        public int MinAdjustMinTransactionCountIS { get; set; }
        public decimal MinPercentSuccessIS { get; set; }
        public decimal MinAdjustMinPercentSuccessIS { get; set; }

        public int MinTransactionCountOS { get; set; }
        public int MinAdjustMinTransactionCountOS { get; set; }
        public decimal MinPercentSuccessOS { get; set; }
        public decimal MinAdjustMinPercentSuccessOS { get; set; }

        public decimal VariationTransaction { get; set; }
        public decimal MinAdjustVariationTransaction { get; set; }

        public decimal Progressiveness { get; set; }
        public decimal MinAdjustProgressiveness { get; set; }
        public bool IsProgressiveness { get; set; }

        public decimal MaxPercentCorrelation { get; set; }

        public int WinningStrategyTotalUP { get; set; }
        public int WinningStrategyTotalDOWN { get; set; }

        public bool AutoAdjustConfig { get; set; }
        public int MaxAdjustConfig { get; set; }
        public bool AsynchronousMode { get; set; }

        // Assembled Builder

        public int TransactionTarget { get; set; }
        public decimal MinAssemblyPercent { get; set; }
        public int TotalAssemblyIterations { get; set; }
    }
}