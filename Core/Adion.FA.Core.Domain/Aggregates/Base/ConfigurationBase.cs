using Adion.FA.Core.Domain.Aggregates.Market;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Base
{
    public class ConfigurationBase : TimeSensitiveBase
    {
        #region Extractor

        public int Variation { get; set; }

        #endregion

        #region Period

        public DateTime? FromDateIS { get; set; }
        public DateTime? ToDateIS { get; set; }

        public DateTime? FromDateOS { get; set; }
        public DateTime? ToDateOS { get; set; }

        #endregion

        #region Schedule

        public bool WithoutSchedule { get; set; }

        #endregion

        #region Currency

        public int CurrencyPairId { get; set; }
        [ForeignKey(nameof(CurrencyPairId))]
        public CurrencyPair CurrencyPair { get; set; }

        public int CurrencyPeriodId { get; set; }
        [ForeignKey(nameof(CurrencyPeriodId))]
        public CurrencyPeriod CurrencyPeriod { get; set; }

        public int CurrencySpreadId { get; set; }
        [ForeignKey(nameof(CurrencySpreadId))]
        public CurrencySpread CurrencySpread { get; set; }

        #endregion

        #region Weka

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

        #endregion

        #region Strategy Builder

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

        #endregion

        #region Assembled Builder

        public int TransactionTarget { get; set; }
        public decimal MinAssemblyPercent { get; set; }
        public int TotalAssemblyIterations { get; set; }

        #endregion
    }
}
