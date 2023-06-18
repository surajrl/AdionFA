using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure.Base;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Station.Project.Model.StrategyBuilder
{
    public class StrategyBuilderProcessModel : ViewModelBase
    {
        // Extraction

        public string ExtractionPath { get; set; }

        private string _extractionName;
        public string ExtractionName
        {
            get => _extractionName;
            set => SetProperty(ref _extractionName, value);
        }

        // Backtests

        private int _executingBacktests;
        public int ExecutingBacktests
        {
            get => _executingBacktests;
            set => SetProperty(ref _executingBacktests, value);
        }

        private int _completedBacktests;
        public int CompletedBacktests
        {
            get => _completedBacktests;
            set => SetProperty(ref _completedBacktests, value);
        }

        private REPTreeOutputModel _tree;
        public REPTreeOutputModel Tree
        {
            get => _tree;
            set => SetProperty(ref _tree, value);
        }

        public ObservableCollection<REPTreeNodeModel> BacktestNodes { get; set; }

        // Process Status

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private int _progressCounter;
        public int ProgressCounter
        {
            get => _progressCounter;
            set => SetProperty(ref _progressCounter, value);
        }
    }
}