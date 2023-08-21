using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.Weka.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public class BuilderProcess : BindableBase
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

        // Used for node builder

        public ObservableCollection<SingleNodeModel> BacktestSingleNodes { get; set; }

        // Used for assembly builder

        public ObservableCollection<AssemblyNodeModel> BacktestAssemblyNodes { get; set; }

        // Used for crossing builder

        public ObservableCollection<StrategyNodeModel> BacktestStrategyNodes { get; set; }

        private decimal _currentSuccessRateIS;
        public decimal CurrentSuccessRateIS
        {
            get => _currentSuccessRateIS;
            set => SetProperty(ref _currentSuccessRateIS, value);
        }

        private decimal _currentSuccessRateOS;
        public decimal CurrentSuccessRateOS
        {
            get => _currentSuccessRateOS;
            set => SetProperty(ref _currentSuccessRateOS, value);
        }

        public ObservableCollection<REPTreeNodeModel> CurrentParentNodes { get; set; }
        public ObservableCollection<REPTreeNodeModel> CurrentChildNodes { get; set; }
        public List<SerializableTuple<REPTreeNodeModel, int, string>> CurrentCrossingNodes { get; set; }
        public ObservableCollection<BacktestOperationModel> PreviousBacktestOperationsIS { get; set; }

        // Builder status 

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}