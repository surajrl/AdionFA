using AdionFA.UI.Station.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Project.Model.Weka
{
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

        private string _historicalData;
        public string HistoricalData
        {
            get => _historicalData;
            set => SetProperty(ref _historicalData, value);
        }

        //  Weka

        private double _totalUP;
        public double TotalUP
        {
            get => _totalUP;
            set => SetProperty(ref _totalUP, value);
        }

        private double _totalDOWN;
        public double TotalDOWN
        {
            get => _totalDOWN;
            set => SetProperty(ref _totalDOWN, value);
        }

        private double _ratioUP;
        public double RatioUP
        {
            get => _ratioUP;
            set => SetProperty(ref _ratioUP, value);
        }

        private double _ratioDOWN;
        public double RatioDOWN
        {
            get => _ratioDOWN;
            set => SetProperty(ref _ratioDOWN, value);
        }

        private double _ratioMax;
        public double RatioMax
        {
            get => _ratioMax;
            set => SetProperty(ref _ratioMax, value);
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

        // IS

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

        // OS

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
