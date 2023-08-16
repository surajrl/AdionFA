using System;

namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class ConfigurationBaseVM : EntityBaseVM
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

        // Schedule

        private bool _withoutSchedule;
        public bool WithoutSchedule
        {
            get => _withoutSchedule;
            set => SetProperty(ref _withoutSchedule, value);
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

        private int _totalDecimalWeka;
        public int TotalDecimalWeka
        {
            get => _totalDecimalWeka;
            set => SetProperty(ref _totalDecimalWeka, value);
        }

        // Builder

        private decimal _maxCorrelationPercent;
        public decimal MaxCorrelationPercent
        {
            get => _maxCorrelationPercent;
            set => SetProperty(ref _maxCorrelationPercent, value);
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
    }
}
