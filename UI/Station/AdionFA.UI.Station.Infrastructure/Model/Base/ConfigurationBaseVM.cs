using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using System;

namespace AdionFA.UI.Station.Infrastructure.Model.Base
{
    public class ConfigurationBaseVM : TimeSensitiveBaseVM
    {
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

        private decimal _sbMinPercentSuccessIS;
        public decimal SBMinPercentSuccessIS
        {
            get => _sbMinPercentSuccessIS;
            set => SetProperty(ref _sbMinPercentSuccessIS, value);
        }

        private int _sbMinTransactionsOS;
        public int SBMinTransactionsOS
        {
            get => _sbMinTransactionsOS;
            set => SetProperty(ref _sbMinTransactionsOS, value);
        }

        private decimal _sbMinPercentSuccessOS;
        public decimal SBMinPercentSuccessOS
        {
            get => _sbMinPercentSuccessOS;
            set => SetProperty(ref _sbMinPercentSuccessOS, value);
        }

        private decimal _sbMaxTransactionsVariation;
        public decimal SBMaxTransactionsVariation
        {
            get => _sbMaxTransactionsVariation;
            set => SetProperty(ref _sbMaxTransactionsVariation, value);
        }

        private bool _isProgressiveness;
        public bool IsProgressiveness
        {
            get => _isProgressiveness;
            set => SetProperty(ref _isProgressiveness, value);
        }

        private decimal _progressiveness;
        public decimal Progressiveness
        {
            get => _progressiveness;
            set => SetProperty(ref _progressiveness, value);
        }

        private decimal _sbMaxPercentCorrelation;
        public decimal SBMaxPercentCorrelation
        {
            get => _sbMaxPercentCorrelation;
            set => SetProperty(ref _sbMaxPercentCorrelation, value);
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


        // Assembled Builder

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
    }
}