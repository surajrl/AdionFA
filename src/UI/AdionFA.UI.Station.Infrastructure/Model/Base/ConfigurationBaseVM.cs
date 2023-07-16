using AdionFA.UI.Infrastructure.Model.MarketData;
using System;

namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class ConfigurationBaseVM : TimeSensitiveBaseVM
    {
        // Period

        private DateTime? _fromDateIS;
        public DateTime? FromDateIS
        {
            get => _fromDateIS;
            set => SetProperty(ref _fromDateIS, value);
        }

        private DateTime? _toDateIS;
        public DateTime? ToDateIS
        {
            get => _toDateIS;
            set => SetProperty(ref _toDateIS, value);
        }

        private DateTime? _fromDateOS;
        public DateTime? FromDateOS
        {
            get => _fromDateOS;
            set => SetProperty(ref _fromDateOS, value);
        }

        private DateTime? _toDateOS;
        public DateTime? ToDateOS
        {
            get => _toDateOS;
            set => SetProperty(ref _toDateOS, value);
        }

        private bool _withoutSchedule;
        public bool WithoutSchedule
        {
            get => _withoutSchedule;
            set => SetProperty(ref _withoutSchedule, value);
        }

        // Historical Data Information

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

        // Extractor

        private int _extractorMinVariation;
        public int ExtractorMinVariation
        {
            get => _extractorMinVariation;
            set => SetProperty(ref _extractorMinVariation, value);
        }

        // MetaTrader

        private string _expertAdvisorHost;
        public string ExpertAdvisorHost
        {
            get => _expertAdvisorHost;
            set => SetProperty(ref _expertAdvisorHost, value);
        }

        private string _expertAdvisorResponsePort;
        public string ExpertAdvisorResponsePort
        {
            get => _expertAdvisorResponsePort;
            set => SetProperty(ref _expertAdvisorResponsePort, value);
        }


        private string _expertAdvisorPublisherPort;
        public string ExpertAdvisorPublisherPort
        {
            get => _expertAdvisorPublisherPort;
            set => SetProperty(ref _expertAdvisorPublisherPort, value);
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

        private int _totalDecimalWeka;
        public int TotalDecimalWeka
        {
            get => _totalDecimalWeka;
            set => SetProperty(ref _totalDecimalWeka, value);
        }

        private int _minimalSeed;
        public int MinimalSeed
        {
            get => _minimalSeed;
            set => SetProperty(ref _minimalSeed, value);
        }

        private int _maximumSeed;
        public int MaximumSeed
        {
            get => _maximumSeed;
            set => SetProperty(ref _maximumSeed, value);
        }

        private decimal _maxRatioTree;
        public decimal MaxRatioTree
        {
            get => _maxRatioTree;
            set => SetProperty(ref _maxRatioTree, value);
        }

        private decimal _nTotalTree;
        public decimal NTotalTree
        {
            get => _nTotalTree;
            set => SetProperty(ref _nTotalTree, value);
        }

        // Strategy Builder

        private int _sbMinTransactionsIS;
        public int SBMinTransactionsIS
        {
            get => _sbMinTransactionsIS;
            set => SetProperty(ref _sbMinTransactionsIS, value);
        }

        private decimal _sbMinSuccessRatePercentIS;
        public decimal SBMinSuccessRatePercentIS
        {
            get => _sbMinSuccessRatePercentIS;
            set => SetProperty(ref _sbMinSuccessRatePercentIS, value);
        }

        private int _sbMinTransactionsOS;
        public int SBMinTransactionsOS
        {
            get => _sbMinTransactionsOS;
            set => SetProperty(ref _sbMinTransactionsOS, value);
        }

        private decimal _sbMinSuccessRatePercentOS;
        public decimal SBMinSuccessRatePercentOS
        {
            get => _sbMinSuccessRatePercentOS;
            set => SetProperty(ref _sbMinSuccessRatePercentOS, value);
        }

        private decimal _sbMaxSuccessRateVariation;
        public decimal SBMaxSuccessRateVariation
        {
            get => _sbMaxSuccessRateVariation;
            set => SetProperty(ref _sbMaxSuccessRateVariation, value);
        }

        private bool _isProgressiveness;
        public bool IsProgressiveness
        {
            get => _isProgressiveness;
            set => SetProperty(ref _isProgressiveness, value);
        }

        private decimal _maxProgressivenessVariation;
        public decimal MaxProgressivenessVariation
        {
            get => _maxProgressivenessVariation;
            set => SetProperty(ref _maxProgressivenessVariation, value);
        }

        private decimal _sbMaxCorrelationPercent;
        public decimal SBMaxCorrelationPercent
        {
            get => _sbMaxCorrelationPercent;
            set => SetProperty(ref _sbMaxCorrelationPercent, value);
        }

        private int _sbWinningStrategyUPTarget;
        public int SBWinningStrategyUPTarget
        {
            get => _sbWinningStrategyUPTarget;
            set => SetProperty(ref _sbWinningStrategyUPTarget, value);
        }

        private int _sbWinningStrategyDOWNTarget;
        public int SBWinningStrategyDOWNTarget
        {
            get => _sbWinningStrategyDOWNTarget;
            set => SetProperty(ref _sbWinningStrategyDOWNTarget, value);
        }

        private int _sbTransactionsTarget;
        public int SBTransactionsTarget
        {
            get => _sbTransactionsTarget;
            set => SetProperty(ref _sbTransactionsTarget, value);
        }


        // Assembly Builder

        private int _abTransactionsTarget;
        public int ABTransactionsTarget
        {
            get => _abTransactionsTarget;
            set => SetProperty(ref _abTransactionsTarget, value);
        }

        private decimal _abMinImprovePercent;
        public decimal ABMinImprovePercent
        {
            get => _abMinImprovePercent;
            set => SetProperty(ref _abMinImprovePercent, value);
        }

        private decimal _abWekaMaxRatioTree;
        public decimal ABWekaMaxRatioTree
        {
            get => _abWekaMaxRatioTree;
            set => SetProperty(ref _abWekaMaxRatioTree, value);
        }

        private decimal _abWekaNTotalTree;
        public decimal ABWekaNTotalTree
        {
            get => _abWekaNTotalTree;
            set => SetProperty(ref _abWekaNTotalTree, value);
        }
    }
}
