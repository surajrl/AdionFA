using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.UI.Station.Project.Model.AssembledBuilder
{
    public class AssembledBuilderProcessModel : ViewModelBase
    {
        // Extraction

        private string _extractionTemplatePath;
        public string ExtractionTemplatePath
        {
            get => _extractionTemplatePath;
            set => SetProperty(ref _extractionTemplatePath, value);
        }

        private string _extractionTemplateName;
        public string ExtractionTemplateName
        {
            get => _extractionTemplateName;
            set => SetProperty(ref _extractionTemplateName, value);
        }

        private string _extractionAssembledBuilderPath;
        public string ExtractionAssembledBuilderPath
        {
            get => _extractionAssembledBuilderPath;
            set => SetProperty(ref _extractionAssembledBuilderPath, value);
        }

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

        private int _totalBacktest;
        public int TotalBacktests
        {
            get => _totalBacktest;
            set => SetProperty(ref _totalBacktest, value);
        }

        // Process Status 

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

        public ObservableCollection<REPTreeOutputVM> TreeOutputs { get; set; }

        private ObservableCollection<REPTreeNodeModel> _nodes;
        public ObservableCollection<REPTreeNodeModel> Nodes
        {
            get => _nodes;
            set
            {
                if (SetProperty(ref _nodes, value))
                {
                    TotalBacktests = _nodes.Where(node => node.Label.ToLowerInvariant() == "up" && node.Winner).Count();
                }
            }
        }
    }
}