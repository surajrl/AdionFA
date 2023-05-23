using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.UI.Station.Project.Model.StrategyBuilder
{
    public class StrategyBuilderProcessModel : ViewModelBase
    {
        public string Path { get; set; }

        private string _extractionName;

        public string ExtractionName
        {
            get => _extractionName;
            set => SetProperty(ref _extractionName, value);
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

        private bool _hasWinningStrategy;

        public bool HasWinningStrategy
        {
            get => _hasWinningStrategy;
            set => SetProperty(ref _hasWinningStrategy, value);
        }

        private int _totalStrategy;

        public int TotalStrategy
        {
            get => _totalStrategy;
            set => SetProperty(ref _totalStrategy, value);
        }

        private int _completedBacktests;

        public int CompletedBacktests
        {
            get => _completedBacktests;
            set => SetProperty(ref _completedBacktests, value);
        }

        private int _executingBacktests;

        public int ExecutingBacktests
        {
            get => _executingBacktests;
            set => SetProperty(ref _executingBacktests, value);
        }

        private int _winningTrees;

        public int WinningTrees
        {
            get => _winningTrees;
            set => SetProperty(ref _winningTrees, value);
        }

        private ObservableCollection<REPTreeOutputVM> _instancesList;

        public ObservableCollection<REPTreeOutputVM> InstancesList
        {
            get => _instancesList;
            set => SetProperty(ref _instancesList, value);
        }

        private string _status;

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}