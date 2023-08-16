using AdionFA.UI.Infrastructure.Model.Base;

namespace AdionFA.UI.Infrastructure.Model.Common
{
    public class NodeBuilderConfigurationVM : WekaBuilderConfigurationBaseVM
    {
        public int NodeBuilderConfigurationId { get; set; }

        private int _nodesUPTarget;
        public int NodesUPTarget
        {
            get => _nodesUPTarget;
            set => SetProperty(ref _nodesUPTarget, value);
        }

        private int _nodesDOWNTarget;
        public int NodesDOWNTarget
        {
            get => _nodesDOWNTarget;
            set => SetProperty(ref _nodesDOWNTarget, value);
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

        private decimal _minSuccessRatePercentIS;
        public decimal MinSuccessRatePercentIS
        {
            get => _minSuccessRatePercentIS;
            set => SetProperty(ref _minSuccessRatePercentIS, value);
        }

        private int _minTotalTradesOS;
        public int MinTotalTradesOS
        {
            get => _minTotalTradesOS;
            set => SetProperty(ref _minTotalTradesOS, value);
        }

        private decimal _minSuccessRatePercentOS;
        public decimal MinSuccessRatePercentOS
        {
            get => _minSuccessRatePercentOS;
            set => SetProperty(ref _minSuccessRatePercentOS, value);
        }

        private decimal _maxSuccessRateVariation;
        public decimal MaxSuccessRateVariation
        {
            get => _maxSuccessRateVariation;
            set => SetProperty(ref _maxSuccessRateVariation, value);
        }
    }
}