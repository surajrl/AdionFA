namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class NodeBuilderConfigurationBaseVM : WekaBuilderConfigurationBaseVM
    {
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

        private int _winningNodesUPTarget;
        public int WinningNodesUPTarget
        {
            get => _winningNodesUPTarget;
            set => SetProperty(ref _winningNodesUPTarget, value);
        }

        private int _winningNodesDOWNTarget;
        public int WinningNodesDOWNTarget
        {
            get => _winningNodesDOWNTarget;
            set => SetProperty(ref _winningNodesDOWNTarget, value);
        }

        private int _totalTradesTarget;
        public int TotalTradesTarget
        {
            get => _totalTradesTarget;
            set => SetProperty(ref _totalTradesTarget, value);
        }

    }
}
