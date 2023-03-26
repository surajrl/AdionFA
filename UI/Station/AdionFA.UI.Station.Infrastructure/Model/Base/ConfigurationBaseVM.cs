using AdionFA.UI.Station.Infrastructure.Model.Market;
using Prism.Unity;
using System;

namespace AdionFA.UI.Station.Infrastructure.Model.Base
{
    public class ConfigurationBaseVM : TimeSensitiveBaseVM
    {
        // Extractor

        private int _variation;
        public int Variation
        {
            get => _variation;
            set => SetProperty(ref _variation, value);
        }

        // Period

        private DateTime? fromDateOS;
        public DateTime? FromDateOS
        {
            get => fromDateOS;
            set => SetProperty(ref fromDateOS, value);
        }

        private DateTime? toDateOS;
        public DateTime? ToDateOS
        {
            get => toDateOS;
            set => SetProperty(ref toDateOS, value);
        }

        private DateTime? fromDateIS;
        public DateTime? FromDateIS
        {
            get => fromDateIS;
            set => SetProperty(ref fromDateIS, value);
        }

        private DateTime? toDateIS;
        public DateTime? ToDateIS
        {
            get => toDateIS;
            set => SetProperty(ref toDateIS, value);
        }

        private bool withoutSchedule;
        public bool WithoutSchedule
        {
            get => withoutSchedule;
            set => SetProperty(ref withoutSchedule, value);
        }

        // Symbol
        private int _symbolId;
        public int SymbolId
        {
            get => _symbolId;
            set => SetProperty(ref _symbolId, value);
        }

        private SymbolVM _symbol;
        public SymbolVM Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        // Timeframe
        private int _timeframeId;
        public int TimeframeId
        {
            get => _timeframeId;
            set => SetProperty(ref _timeframeId, value);
        }

        private TimeframeVM _timeframe;
        public TimeframeVM Timeframe
        {
            get => _timeframe;
            set => SetProperty(ref _timeframe, value);
        }

        // CurrencySpreadId

        private int _currencySpreadId;
        public int CurrencySpreadId
        {
            get => _currencySpreadId;
            set => SetProperty(ref _currencySpreadId, value);
        }

        private CurrencySpreadVM currencySpread;

        public CurrencySpreadVM CurrencySpread
        {
            get => currencySpread;
            set => SetProperty(ref currencySpread, value);
        }

        // Weka

        private int _totalInstanceWeka;
        public int TotalInstanceWeka
        {
            get => _totalInstanceWeka;
            set => SetProperty(ref _totalInstanceWeka, value);
        }

        private int _depthWeka;

        public int DepthWeka
        {
            get => _depthWeka;
            set => SetProperty(ref _depthWeka, value);
        }

        private int minAdjustDepthWeka;

        public int MinAdjustDepthWeka
        {
            get => minAdjustDepthWeka;
            set => SetProperty(ref minAdjustDepthWeka, value);
        }

        private int totalDecimalWeka;

        public int TotalDecimalWeka
        {
            get => totalDecimalWeka;
            set => SetProperty(ref totalDecimalWeka, value);
        }

        private int minimalSeed;

        public int MinimalSeed
        {
            get => minimalSeed;
            set => SetProperty(ref minimalSeed, value);
        }

        private int maximumSeed;

        public int MaximumSeed
        {
            get => maximumSeed;
            set => SetProperty(ref maximumSeed, value);
        }

        private decimal maxRatioTree;

        public decimal MaxRatioTree
        {
            get => maxRatioTree;
            set => SetProperty(ref maxRatioTree, value);
        }

        private decimal minAdjustMaxRatioTree;

        public decimal MinAdjustMaxRatioTree
        {
            get => minAdjustMaxRatioTree;
            set => SetProperty(ref minAdjustMaxRatioTree, value);
        }

        private decimal nTotalTree;

        public decimal NTotalTree
        {
            get => nTotalTree;
            set => SetProperty(ref nTotalTree, value);
        }

        private decimal minAdjustNTotalTree;

        public decimal MinAdjustNTotalTree
        {
            get => minAdjustNTotalTree;
            set => SetProperty(ref minAdjustNTotalTree, value);
        }

        // Strategy Builder

        private int minTransactionCountIS;

        public int MinTransactionCountIS
        {
            get => minTransactionCountIS;
            set => SetProperty(ref minTransactionCountIS, value);
        }

        private int minAdjustMinTransactionCountIS;
        public int MinAdjustMinTransactionCountIS
        {
            get => minAdjustMinTransactionCountIS;
            set => SetProperty(ref minAdjustMinTransactionCountIS, value);
        }

        private decimal minPercentSuccessIS;
        public decimal MinPercentSuccessIS
        {
            get => minPercentSuccessIS;
            set => SetProperty(ref minPercentSuccessIS, value);
        }

        #region MinAdjustMinPercentSuccessIS

        private decimal minAdjustMinPercentSuccessIS;

        public decimal MinAdjustMinPercentSuccessIS
        {
            get => minAdjustMinPercentSuccessIS;
            set => SetProperty(ref minAdjustMinPercentSuccessIS, value);
        }

        #endregion MinAdjustMinPercentSuccessIS

        #region MinTransactionCountOS

        private int minTransactionCountOS;

        public int MinTransactionCountOS
        {
            get => minTransactionCountOS;
            set => SetProperty(ref minTransactionCountOS, value);
        }

        #endregion MinTransactionCountOS

        #region MinAdjustMinTransactionCountOS

        private int minAdjustMinTransactionCountOS;

        public int MinAdjustMinTransactionCountOS
        {
            get => minAdjustMinTransactionCountOS;
            set => SetProperty(ref minAdjustMinTransactionCountOS, value);
        }

        #endregion MinAdjustMinTransactionCountOS

        #region MinPercentSuccessOS

        private decimal minPercentSuccessOS;

        public decimal MinPercentSuccessOS
        {
            get => minPercentSuccessOS;
            set => SetProperty(ref minPercentSuccessOS, value);
        }

        #endregion MinPercentSuccessOS

        #region MinAdjustMinPercentSuccessOS

        private decimal minAdjustMinPercentSuccessOS;

        public decimal MinAdjustMinPercentSuccessOS
        {
            get => minAdjustMinPercentSuccessOS;
            set => SetProperty(ref minAdjustMinPercentSuccessOS, value);
        }

        #endregion MinAdjustMinPercentSuccessOS

        #region VariationTransaction

        private decimal variationTransaction;

        public decimal VariationTransaction
        {
            get => variationTransaction;
            set => SetProperty(ref variationTransaction, value);
        }

        #endregion VariationTransaction

        #region MinAdjustVariationTransaction

        private decimal minAdjustVariationTransaction;

        public decimal MinAdjustVariationTransaction
        {
            get => minAdjustVariationTransaction;
            set => SetProperty(ref minAdjustVariationTransaction, value);
        }

        #endregion MinAdjustVariationTransaction

        #region Progressiveness

        private decimal progressiveness;

        public decimal Progressiveness
        {
            get => progressiveness;
            set => SetProperty(ref progressiveness, value);
        }

        #endregion Progressiveness

        #region MinAdjustProgressiveness

        private decimal minAdjustProgressiveness;

        public decimal MinAdjustProgressiveness
        {
            get => minAdjustProgressiveness;
            set => SetProperty(ref minAdjustProgressiveness, value);
        }

        #endregion MinAdjustProgressiveness

        #region IsProgressiveness

        private bool isProgressiveness;

        public bool IsProgressiveness
        {
            get => isProgressiveness;
            set => SetProperty(ref isProgressiveness, value);
        }

        #endregion IsProgressiveness

        #region MaxPercentCorrelation

        private decimal maxPercentCorrelation;

        public decimal MaxPercentCorrelation
        {
            get => maxPercentCorrelation;
            set => SetProperty(ref maxPercentCorrelation, value);
        }

        #endregion MaxPercentCorrelation

        #region WinningStrategyTotalUP

        private int winningStrategyTotalUP;

        public int WinningStrategyTotalUP
        {
            get => winningStrategyTotalUP;
            set => SetProperty(ref winningStrategyTotalUP, value);
        }

        #endregion WinningStrategyTotalUP

        #region WinningStrategyTotalDOWN

        private int winningStrategyTotalDOWN;

        public int WinningStrategyTotalDOWN
        {
            get => winningStrategyTotalDOWN;
            set => SetProperty(ref winningStrategyTotalDOWN, value);
        }

        #endregion WinningStrategyTotalDOWN

        #region AutoAdjustConfig

        private bool autoAdjustConfig;

        public bool AutoAdjustConfig
        {
            get => autoAdjustConfig;
            set => SetProperty(ref autoAdjustConfig, value);
        }

        #endregion AutoAdjustConfig

        #region MaxAdjustConfig

        private int maxAdjustConfig;

        public int MaxAdjustConfig
        {
            get => maxAdjustConfig;
            set => SetProperty(ref maxAdjustConfig, value);
        }

        #endregion MaxAdjustConfig

        #region AsynchronousMode

        private bool asynchronousMode;

        public bool AsynchronousMode
        {
            get => asynchronousMode;
            set => SetProperty(ref asynchronousMode, value);
        }

        #endregion AsynchronousMode

        // Assembled Builder

        #region TransactionTarget

        private int transactionTarget;

        public int TransactionTarget
        {
            get => transactionTarget;
            set => SetProperty(ref transactionTarget, value);
        }

        #endregion TransactionTarget

        #region MinAssemblyPercent

        private decimal minAssemblyPercent;

        public decimal MinAssemblyPercent
        {
            get => minAssemblyPercent;
            set => SetProperty(ref minAssemblyPercent, value);
        }

        #endregion MinAssemblyPercent

        #region TotalAssemblyIterations

        private int totalAssemblyIterations;

        public int TotalAssemblyIterations
        {
            get => totalAssemblyIterations;
            set => SetProperty(ref totalAssemblyIterations, value);
        }

        #endregion TotalAssemblyIterations
    }
}
