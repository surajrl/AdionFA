using AdionFA.Core.Domain.Aggregates.Market;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Base
{
    public class ConfigurationBase : TimeSensitiveBase
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

        // Currency

        public int SymbolId { get; set; }

        [ForeignKey(nameof(SymbolId))]
        public Symbol Symbol { get; set; }

        public int TimeframeId { get; set; }

        [ForeignKey(nameof(TimeframeId))]
        public Timeframe Timeframe { get; set; }

        public int CurrencySpreadId { get; set; }

        [ForeignKey(nameof(CurrencySpreadId))]
        public CurrencySpread CurrencySpread { get; set; }

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