using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure.Base;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Station.Project.Model.AssembledBuilder
{
    public class AssembledBuilderProcessModel : ViewModelBase
    {
        // Extraction

        public string ExtractionTemplatePath { get; set; }

        private string _extractionTemplateName;
        public string ExtractionTemplateName
        {
            get => _extractionTemplateName;
            set => SetProperty(ref _extractionTemplateName, value);
        }

        public string ExtractionAssembledBuilderPath { get; set; }

        private string _extractionAssembledBuilderName;
        public string ExtractionAssembledBuilderName
        {
            get => _extractionAssembledBuilderName;
            set => SetProperty(ref _extractionAssembledBuilderName, value);
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