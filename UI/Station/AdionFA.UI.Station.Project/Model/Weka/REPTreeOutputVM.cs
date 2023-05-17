using AdionFA.UI.Station.Infrastructure.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.UI.Station.Infrastructure.Model.Weka
{
    public class REPTreeOutputVM : ViewModelBase
    {
        private int _seed;

        public int Seed
        {
            get => _seed;
            set => SetProperty(ref _seed, value);
        }

        private string _treeOutput;

        public string TreeOutput
        {
            get => _treeOutput;
            set => SetProperty(ref _treeOutput, value);
        }

        private ObservableCollection<REPTreeNodeVM> _nodeOutput;

        public ObservableCollection<REPTreeNodeVM> NodeOutput
        {
            get => _nodeOutput;
            set => SetProperty(ref _nodeOutput, value);
        }

        private bool _isSuccess;

        public bool IsSuccess
        {
            get => _isSuccess;
            set => SetProperty(ref _isSuccess, value);
        }

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private bool? _hasWinningStrategy;

        public bool? HasWinningStrategy
        {
            get => _hasWinningStrategy;
            set => SetProperty(ref _hasWinningStrategy, value);
        }

        private int _totalWinningStrategy;

        public int TotalWinningStrategy
        {
            get => _totalWinningStrategy;
            set => SetProperty(ref _totalWinningStrategy, value);
        }

        private int _totalWinningStrategyUP;

        public int TotalWinningStrategyUP
        {
            get => _totalWinningStrategyUP;
            set => SetProperty(ref _totalWinningStrategyUP, value);
        }

        private int _totalWinningStrategyDOWN;

        public int TotalWinningStrategyDOWN
        {
            get => _totalWinningStrategyDOWN;
            set => SetProperty(ref _totalWinningStrategyDOWN, value);
        }

        private int _validNodes;

        public int ValidNodes
        {
            get => _validNodes;
            set => SetProperty(ref _validNodes, value);
        }

        public int _counterProgressBar;

        public int CounterProgressBar
        {
            get => _counterProgressBar;
            set => SetProperty(ref _counterProgressBar, value);
        }
    }
}