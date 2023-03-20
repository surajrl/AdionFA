using Adion.FA.UI.Station.Infrastructure.Model.Market;
using System;

namespace Adion.FA.UI.Station.Infrastructure.Model.Base
{
    public class ConfigurationBaseVM : TimeSensitiveBaseVM
    {
        #region Extractor

        #region Variation

        private int variation;
        public int Variation
        { 
            get => variation;
            set => SetProperty(ref variation, value); 
        }

        #endregion

        #endregion

        #region Period

        #region FromDateOS

        private DateTime? fromDateOS;
        public DateTime? FromDateOS
        {
            get => fromDateOS; 
            set => SetProperty(ref fromDateOS, value); 
        }

        #endregion

        #region ToDateOS

        private DateTime? toDateOS;
        public DateTime? ToDateOS
        {
            get => toDateOS;
            set => SetProperty(ref toDateOS, value);
        }

        #endregion

        #region FromDateIS

        private DateTime? fromDateIS;
        public DateTime? FromDateIS
        {
            get => fromDateIS;
            set => SetProperty(ref fromDateIS, value);
        }

        #endregion

        #region ToDateIS

        private DateTime? toDateIS;
        public DateTime? ToDateIS
        {
            get => toDateIS;
            set => SetProperty(ref toDateIS, value);
        }

        #endregion

        #endregion

        #region Schedule

        #region WithoutSchedule

        private bool withoutSchedule;
        public bool WithoutSchedule
        { 
            get => withoutSchedule; 
            set => SetProperty(ref withoutSchedule, value); 
        }

        #endregion

        #endregion

        #region Currency

        #region CurrencyPairId

        private int currencyPairId;
        public int CurrencyPairId
        { 
            get => currencyPairId; 
            set => SetProperty(ref currencyPairId, value); 
        }

        #endregion

        #region CurrencyPair

        private CurrencyPairVM currencyPair;
        public CurrencyPairVM CurrencyPair
        {
            get => currencyPair;
            set => SetProperty(ref currencyPair, value);
        }

        #endregion

        #region CurrencyPeriodId

        private int currencyPeriodId;
        public int CurrencyPeriodId
        {
            get => currencyPeriodId;
            set => SetProperty(ref currencyPeriodId, value);
        }

        #endregion

        #region CurrencyPeriod

        private CurrencyPeriodVM currencyPeriod;
        public CurrencyPeriodVM CurrencyPeriod
        {
            get => currencyPeriod;
            set => SetProperty(ref currencyPeriod, value);
        }

        #endregion

        #region CurrencySpreadId

        private int currencySpreadId;
        public int CurrencySpreadId
        {
            get => currencySpreadId;
            set => SetProperty(ref currencySpreadId, value);
        }

        #endregion

        #region CurrencySpread

        private CurrencySpreadVM currencySpread;
        public CurrencySpreadVM CurrencySpread
        {
            get => currencySpread;
            set => SetProperty(ref currencySpread, value);
        }

        #endregion

        #endregion

        #region Weka

        #region TotalInstanceWeka

        private int totalInstanceWeka;
        public int TotalInstanceWeka 
        {
            get => totalInstanceWeka; 
            set => SetProperty(ref totalInstanceWeka, value);  
        }

        #endregion


        #region DepthWeka

        private int depthWekal;
        public int DepthWeka
        {
            get => depthWekal;
            set => SetProperty(ref depthWekal, value);
        }

        #endregion

        #region MinAdjustDepthWeka

        private int minAdjustDepthWeka;
        public int MinAdjustDepthWeka
        {
            get => minAdjustDepthWeka;
            set => SetProperty(ref minAdjustDepthWeka, value);
        }

        #endregion


        #region TotalDecimalWeka

        private int totalDecimalWeka;
        public int TotalDecimalWeka
        {
            get => totalDecimalWeka;
            set => SetProperty(ref totalDecimalWeka, value);
        }

        #endregion

        #region MinimalSeed

        private int minimalSeed;
        public int MinimalSeed
        {
            get => minimalSeed;
            set => SetProperty(ref minimalSeed, value);
        }

        #endregion

        #region MaximumSeed

        private int maximumSeed;
        public int MaximumSeed
        {
            get => maximumSeed;
            set => SetProperty(ref maximumSeed, value);
        }

        #endregion


        #region MaxRatioTree

        private decimal maxRatioTree;
        public decimal MaxRatioTree
        {
            get => maxRatioTree;
            set => SetProperty(ref maxRatioTree, value);
        }

        #endregion

        #region MinAdjustMaxRatioTree

        private decimal minAdjustMaxRatioTree;
        public decimal MinAdjustMaxRatioTree
        {
            get => minAdjustMaxRatioTree;
            set => SetProperty(ref minAdjustMaxRatioTree, value);
        }

        #endregion


        #region NTotalTree

        private decimal nTotalTree;
        public decimal NTotalTree
        {
            get => nTotalTree;
            set => SetProperty(ref nTotalTree, value);
        }

        #endregion

        #region MinAdjustNTotalTree

        private decimal minAdjustNTotalTree;
        public decimal MinAdjustNTotalTree
        {
            get => minAdjustNTotalTree;
            set => SetProperty(ref minAdjustNTotalTree, value);
        }

        #endregion

        #endregion

        #region Strategy Builder

        #region MinTransactionCountIS
        private int minTransactionCountIS;
        public int MinTransactionCountIS
        {
            get => minTransactionCountIS;
            set => SetProperty(ref minTransactionCountIS, value);
        }
        #endregion

        #region MinAdjustMinTransactionCountIS
        private int minAdjustMinTransactionCountIS;
        public int MinAdjustMinTransactionCountIS
        {
            get => minAdjustMinTransactionCountIS;
            set => SetProperty(ref minAdjustMinTransactionCountIS, value);
        }
        #endregion

        #region MinPercentSuccessIS
        private decimal minPercentSuccessIS;
        public decimal MinPercentSuccessIS
        {
            get => minPercentSuccessIS;
            set => SetProperty(ref minPercentSuccessIS, value);
        }
        #endregion

        #region MinAdjustMinPercentSuccessIS
        private decimal minAdjustMinPercentSuccessIS;
        public decimal MinAdjustMinPercentSuccessIS
        {
            get => minAdjustMinPercentSuccessIS;
            set => SetProperty(ref minAdjustMinPercentSuccessIS, value);
        }
        #endregion


        #region MinTransactionCountOS
        private int minTransactionCountOS;
        public int MinTransactionCountOS 
        {
            get => minTransactionCountOS; 
            set => SetProperty(ref minTransactionCountOS, value); 
        }
        #endregion

        #region MinAdjustMinTransactionCountOS
        private int minAdjustMinTransactionCountOS;
        public int MinAdjustMinTransactionCountOS
        {
            get => minAdjustMinTransactionCountOS;
            set => SetProperty(ref minAdjustMinTransactionCountOS, value);
        }
        #endregion

        #region MinPercentSuccessOS
        private decimal minPercentSuccessOS;
        public decimal MinPercentSuccessOS
        {
            get => minPercentSuccessOS;
            set => SetProperty(ref minPercentSuccessOS, value);
        }
        #endregion

        #region MinAdjustMinPercentSuccessOS
        private decimal minAdjustMinPercentSuccessOS;
        public decimal MinAdjustMinPercentSuccessOS
        {
            get => minAdjustMinPercentSuccessOS;
            set => SetProperty(ref minAdjustMinPercentSuccessOS, value);
        }
        #endregion


        #region VariationTransaction
        private decimal variationTransaction;
        public decimal VariationTransaction
        {
            get => variationTransaction;
            set => SetProperty(ref variationTransaction, value);
        }
        #endregion

        #region MinAdjustVariationTransaction
        private decimal minAdjustVariationTransaction;
        public decimal MinAdjustVariationTransaction
        {
            get => minAdjustVariationTransaction;
            set => SetProperty(ref minAdjustVariationTransaction, value);
        }
        #endregion


        #region Progressiveness
        private decimal progressiveness;
        public decimal Progressiveness
        {
            get => progressiveness;
            set => SetProperty(ref progressiveness, value);
        }
        #endregion

        #region MinAdjustProgressiveness
        private decimal minAdjustProgressiveness;
        public decimal MinAdjustProgressiveness
        {
            get => minAdjustProgressiveness;
            set => SetProperty(ref minAdjustProgressiveness, value);
        }
        #endregion

        #region IsProgressiveness
        private bool isProgressiveness;
        public bool IsProgressiveness
        {
            get => isProgressiveness;
            set => SetProperty(ref isProgressiveness, value);
        }
        #endregion


        #region MaxPercentCorrelation
        private decimal maxPercentCorrelation;
        public decimal MaxPercentCorrelation
        {
            get => maxPercentCorrelation;
            set => SetProperty(ref maxPercentCorrelation, value);
        }
        #endregion


        #region WinningStrategyTotalUP
        private int winningStrategyTotalUP;
        public int WinningStrategyTotalUP
        {
            get => winningStrategyTotalUP;
            set => SetProperty(ref winningStrategyTotalUP, value);
        }
        #endregion

        #region WinningStrategyTotalDOWN
        private int winningStrategyTotalDOWN;
        public int WinningStrategyTotalDOWN
        { 
            get => winningStrategyTotalDOWN; 
            set => SetProperty(ref winningStrategyTotalDOWN, value); 
        }
        #endregion

        
        #region AutoAdjustConfig
        private bool autoAdjustConfig;
        public bool AutoAdjustConfig 
        {
            get => autoAdjustConfig; 
            set => SetProperty(ref autoAdjustConfig, value); 
        }
        #endregion

        #region MaxAdjustConfig
        private int maxAdjustConfig;
        public int MaxAdjustConfig 
        {
            get => maxAdjustConfig; 
            set => SetProperty(ref maxAdjustConfig, value); 
        }
        #endregion

        #region AsynchronousMode
        private bool asynchronousMode;
        public bool AsynchronousMode 
        {
            get => asynchronousMode; 
            set => SetProperty(ref asynchronousMode, value);
        }
        #endregion

        #endregion

        #region Assembled Builder

        #region TransactionTarget
        private int transactionTarget;
        public int TransactionTarget
        {
            get => transactionTarget;
            set => SetProperty(ref transactionTarget, value);
        }
        #endregion

        #region MinAssemblyPercent
        private decimal minAssemblyPercent;
        public decimal MinAssemblyPercent
        {
            get => minAssemblyPercent;
            set => SetProperty(ref minAssemblyPercent, value); 
        }
        #endregion

        #region TotalAssemblyIterations
        private int totalAssemblyIterations;
        public int TotalAssemblyIterations
        {
            get => totalAssemblyIterations; 
            set => SetProperty(ref totalAssemblyIterations, value); 
        }
        #endregion

        #endregion
    }
}
