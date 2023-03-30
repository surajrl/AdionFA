using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Project.Enums;
using AdionFA.UI.Station.Project.Model.Weka;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.UI.Station.Project.Model.StrategyBuilder
{
    public class StrategyBuilderProcessModel : ViewModelBase
    {
        public string Path { get; set; }

        private string _templateName;
        public string TemplateName
        {
            get => _templateName;
            set => SetProperty(ref _templateName, value);
        }

        private int _regionType;
        public int RegionType
        {
            get => _regionType;
            set => SetProperty(ref _regionType, value);
        }

        private string _regionName;
        public string RegionName
        {
            get => _regionName;
            set => SetProperty(ref _regionName, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private bool winningStrategy;
        public bool WinningStrategy
        {
            get => winningStrategy;
            set => SetProperty(ref winningStrategy, value);
        }

        private int totalWinningStrategy;
        public int TotalWinningStrategy
        {
            get => totalWinningStrategy;
            set => SetProperty(ref totalWinningStrategy, value);
        }

        private int totalWinningStrategyUp;
        public int TotalWinningStrategyUp
        {
            get => totalWinningStrategyUp;
            set => SetProperty(ref totalWinningStrategyUp, value);
        }

        private int totalWinningStrategyDown;
        public int TotalWinningStrategyDown
        {
            get => totalWinningStrategyDown;
            set => SetProperty(ref totalWinningStrategyDown, value);
        }

        private int winningTrees;
        public int WinningTrees
        {
            get => winningTrees;
            set => SetProperty(ref winningTrees, value);
        }

        private ObservableCollection<REPTreeOutputModelVM> instancesList;
        public ObservableCollection<REPTreeOutputModelVM> InstancesList
        {
            get => instancesList;
            set => SetProperty(ref instancesList, value);
        }

        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public void Reset()
        {
            Status = StrategyBuilderStatusEnum.NoStarted.GetMetadata().Name;
            Message = string.Empty;
            IsEnabled = false;
            IsExpanded = false;
            WinningStrategy = false;
            TotalWinningStrategy = TotalWinningStrategyUp = TotalWinningStrategyDown = 0;
            WinningTrees = 0;
            InstancesList = new ObservableCollection<REPTreeOutputModelVM>();
        }
    }
}
