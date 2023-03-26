using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Project.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.UI.Station.Project.Model.StrategyBuilder
{
    public class StrategyBuilderProcessModel : ViewModelBase
    {
        public string Path { get; set; }


        private string templateName;
        public string TemplateName
        {
            get => templateName;
            set => SetProperty(ref templateName, value);
        }


        private int regionType;
        public int RegionType
        {
            get => regionType;
            set => SetProperty(ref regionType, value);
        }


        private string regionName;
        public string RegionName
        {
            get => regionName;
            set => SetProperty(ref regionName, value);
        }


        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }


        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
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

    public class REPTreeOutputModelVM : ViewModelBase
    {
        private int seed;
        public int Seed 
        {
            get => seed;
            set => SetProperty(ref seed, value); 
        }


        private string treeOutput;
        public string TreeOutput 
        {
            get => treeOutput;
            set => SetProperty(ref treeOutput, value);
        }


        private bool? winningStrategy;
        public bool? WinningStrategy
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


        private int winningNodes;
        public int WinningNodes 
        {
            get => winningNodes;
            set => SetProperty(ref winningNodes, value);
        }


        private ObservableCollection<REPTreeNodeModelVM> nodeOutput;
        public ObservableCollection<REPTreeNodeModelVM> NodeOutput 
        {
            get => nodeOutput;
            set => SetProperty(ref nodeOutput, value);
        }

        #region Progress Bar
        public int counterProgressBar;
        public int CounterProgressBar
        { 
            get => counterProgressBar;
            set => SetProperty(ref counterProgressBar, value); }
        #endregion

        private bool isSuccess;
        public bool IsSuccess 
        {
            get => isSuccess;
            set => SetProperty(ref isSuccess, value); 
        }


        private string message;
        public string Message 
        {
            get => message;
            set => SetProperty(ref message, value);
        }
    }

    public class REPTreeNodeModelVM : ViewModelBase
    {
        public string LastNode => Node.LastOrDefault();

        public string NodeWithoutFormat { get; set; }

        private ObservableCollection<string> node;
        public ObservableCollection<string> Node
        {
            get => node;
            set => SetProperty(ref node, value);
        }

        private string marketData;
        public string MarketData
        { 
            get => marketData;
            set => SetProperty(ref marketData, value);
        }

        #region Weka
        private double totalUp;
        public double TotalUP
        {
            get => totalUp;
            set => SetProperty(ref totalUp, value);
        }


        private double totalDown;
        public double TotalDOWN
        {
            get => totalDown;
            set => SetProperty(ref totalDown, value);
        }


        private double rationUp;
        public double RatioUP
        {
            get => rationUp;
            set => SetProperty(ref rationUp, value);
        }


        private double rationDown;
        public double RatioDOWN
        {
            get => rationDown;
            set => SetProperty(ref rationDown, value);
        }


        private double rationMax;
        public double RatioMax
        {
            get => rationMax;
            set => SetProperty(ref rationMax, value);
        }


        private double total;
        public double Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }


        private string label;
        public string Label
        {
            get => label;
            set => SetProperty(ref label, value);
        }


        private bool winner;
        public bool Winner
        {
            get => winner;
            set => SetProperty(ref winner, value);
        }
        #endregion

        #region IS
        private int totalTradesIs;
        public int TotalTradesIs 
        {
            get => totalTradesIs;
            set => SetProperty(ref totalTradesIs, value);
        }


        private int winningTradesIs;
        public int WinningTradesIs
        {
            get => winningTradesIs;
            set => SetProperty(ref winningTradesIs, value);
        }


        private int losingTradesIs;
        public int LosingTradesIs
        {
            get => losingTradesIs;
            set => SetProperty(ref losingTradesIs, value);
        }

        public double TotalOpportunityIs { get; set; }
        public double PercentSuccessIs { get; set; }
        public double ProgressivenessIs { get; set; }
        #endregion

        #region OS
        private int totalTradesOs;
        public int TotalTradesOs
        {
            get => totalTradesOs;
            set => SetProperty(ref totalTradesOs, value);
        }


        private int winningTradesOs;
        public int WinningTradesOs
        {
            get => winningTradesOs;
            set => SetProperty(ref winningTradesOs, value);
        }


        private int losingTradesOs;
        public int LosingTradesOs
        {
            get => losingTradesOs;
            set => SetProperty(ref losingTradesOs, value);
        }

        public double TotalOpportunityOs { get; set; }
        public double PercentSuccessOs { get; set; }
        public double ProgressivenessOs { get; set; }
        #endregion

        public double VariationPercent => Math.Abs(PercentSuccessIs - PercentSuccessOs);
        public double Progressiveness => Math.Abs(ProgressivenessIs - ProgressivenessOs);
        public bool WinningStrategy { get; set; }

        private bool hasTestInMetatrader;
        public bool HasTestInMetatrader 
        {
            get => hasTestInMetatrader; 
            set => SetProperty(ref hasTestInMetatrader, value);
        }
    }
}
