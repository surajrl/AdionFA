using AdionFA.UI.Infrastructure.Model.Base;

namespace AdionFA.UI.Infrastructure.Model.Common
{
    public class AssemblyBuilderConfigurationVM : WekaBuilderConfigurationBaseVM
    {
        public int AssemblyBuilderConfigurationId { get; set; }

        private int _assemblyNodesUPTarget;
        public int AssemblyNodesUPTarget
        {
            get => _assemblyNodesUPTarget;
            set => SetProperty(ref _assemblyNodesUPTarget, value);
        }

        private int _assemblyNodesDOWNTarget;
        public int AssemblyNodesDOWNTarget
        {
            get => _assemblyNodesDOWNTarget;
            set => SetProperty(ref _assemblyNodesDOWNTarget, value);
        }

        private int _totalTradesTarget;
        public int TotalTradesTarget
        {
            get => _totalTradesTarget;
            set => SetProperty(ref _totalTradesTarget, value);
        }

        private int _minTotalTradesIS;
        public int MinTotalTradesIS
        {
            get => _minTotalTradesIS;
            set => SetProperty(ref _minTotalTradesIS, value);
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
