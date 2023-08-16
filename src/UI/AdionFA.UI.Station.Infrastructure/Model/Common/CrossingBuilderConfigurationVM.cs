using AdionFA.UI.Infrastructure.Model.Base;

namespace AdionFA.UI.Infrastructure.Model.Common
{
    public class CrossingBuilderConfigurationVM : WekaBuilderConfigurationBaseVM
    {
        public int CrossingBuilderConfigurationId { get; set; }

        private int _strategyNodesUPTarget;
        public int StrategyNodesUPTarget
        {
            get => _strategyNodesUPTarget;
            set => SetProperty(ref _strategyNodesUPTarget, value);
        }

        private int _strategyNodesDOWNTarget;
        public int StrategyNodesDOWNTarget
        {
            get => _strategyNodesDOWNTarget;
            set => SetProperty(ref _strategyNodesDOWNTarget, value);
        }

        private int _totalTradesTarget;
        public int TotalTradesTarget
        {
            get => _totalTradesTarget;
            set => SetProperty(ref _totalTradesTarget, value);
        }

        private decimal _minSuccessRateImprovementIS;
        public decimal MinSuccessRateImprovementIS
        {
            get => _minSuccessRateImprovementIS;
            set => SetProperty(ref _minSuccessRateImprovementIS, value);
        }

        private decimal _minSuccessRateImprovementOS;
        public decimal MinSuccessRateImprovementOS
        {
            get => _minSuccessRateImprovementOS;
            set => SetProperty(ref _minSuccessRateImprovementOS, value);
        }

        private decimal _maxSuccessRateImprovementIS;
        public decimal MaxSuccessRateImprovementIS
        {
            get => _maxSuccessRateImprovementIS;
            set => SetProperty(ref _maxSuccessRateImprovementIS, value);
        }

        private decimal _maxSuccessRateImprovementOS;
        public decimal MaxSuccessRateImprovementOS
        {
            get => _maxSuccessRateImprovementOS;
            set => SetProperty(ref _maxSuccessRateImprovementOS, value);
        }
    }
}
