using AdionFA.Infrastructure.Modules.Strategy;
using AdionFA.Infrastructure.Weka.Model;
using AdionFA.UI.Infrastructure.Base;
using System.Collections.ObjectModel;

namespace AdionFA.UI.ProjectStation.Model.Common
{
    public class BuilderProcess : ViewModelBase
    {
        // Extraction
        public string ExtractionPath { get; set; }

        public string ExtractionTemplatePath { get; set; }

        private string _extractionTemplateName;
        public string ExtractionTemplateName
        {
            get => _extractionTemplateName;
            set => SetProperty(ref _extractionTemplateName, value);
        }

        private string _extractionName;
        public string ExtractionName
        {
            get => _extractionName;
            set => SetProperty(ref _extractionName, value);
        }

        // Backtests

        private int _executingBacktest;
        public int ExecutingBacktests
        {
            get => _executingBacktest;
            set => SetProperty(ref _executingBacktest, value);
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

        public ObservableCollection<NodeModel> BacktestNodes { get; set; }  // Used for Node Builder
        public ObservableCollection<AssemblyNodeModel> BacktestAssemblyNodes { get; set; } // Used for Assembly Builder
        public ObservableCollection<StrategyNodeModel> BacktestStrategyNodes { get; set; } // Used for Crossing Builder
        public StrategyNodeModel PreviousStrategyNode { get; set; } // Used for Crossing Builder

        // Builder Process Status 

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