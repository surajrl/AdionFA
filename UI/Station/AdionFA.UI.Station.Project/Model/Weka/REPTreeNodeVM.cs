using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace AdionFA.UI.Station.Infrastructure.Model.Weka
{
    public class REPTreeNodeVM : ViewModelBase
    {
        private string _historicalData;

        public string HistoricalData
        {
            get => _historicalData;
            set => SetProperty(ref _historicalData, value);
        }

        public REPTreeOutputVM Tree { get; set; }
        public StrategyBuilderProcessModel StrategyBuilderProcess { get; set; }

        // Weka

        private ObservableCollection<string> _node;

        public ObservableCollection<string> Node
        {
            get => _node;
            set => SetProperty(ref _node, value);
        }

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

        private double _total;

        public double Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        private string _label;

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private bool _winner;

        public bool Winner
        {
            get => _winner;
            set => SetProperty(ref _winner, value);
        }

        // IS

        private int _totalTradesIs;

        public int TotalTradesIs
        {
            get => _totalTradesIs;
            set => SetProperty(ref _totalTradesIs, value);
        }

        private int _winningTradesIs;

        public int WinningTradesIs
        {
            get => _winningTradesIs;
            set => SetProperty(ref _winningTradesIs, value);
        }

        private int _losingTradesIs;

        public int LosingTradesIs
        {
            get => _losingTradesIs;
            set => SetProperty(ref _losingTradesIs, value);
        }

        public double TotalOpportunityIs { get; set; }
        public double SuccessRatePercentIs { get; set; }
        public double ProgressivenessIs { get; set; }

        // OS

        private int _totalTradesOs;

        public int TotalTradesOs
        {
            get => _totalTradesOs;
            set => SetProperty(ref _totalTradesOs, value);
        }

        private int _winningTradesOs;

        public int WinningTradesOs
        {
            get => _winningTradesOs;
            set => SetProperty(ref _winningTradesOs, value);
        }

        private int _losingTradesOs;

        public int LosingTradesOs
        {
            get => _losingTradesOs;
            set => SetProperty(ref _losingTradesOs, value);
        }

        public double TotalOpportunityOs { get; set; }
        public double SuccessRatePercentOs { get; set; }
        public double ProgressivenessOs { get; set; }

        // Correlation

        private bool _correlationPass;

        public bool CorrelationPass
        {
            get => _correlationPass;
            set => SetProperty(ref _correlationPass, value);
        }

        // Results

        public double SuccessRateVariation => Math.Abs(SuccessRatePercentIs - SuccessRatePercentOs);
        public double Progressiveness => Math.Abs(ProgressivenessIs - ProgressivenessOs);
        public bool WinningStrategy { get; set; }

        public bool IsBacktestCompleted { get; set; }

        public string Name => NodeName();

        public string NodeName()
        {
            var indicators = new List<string>();
            Node.ToList().ForEach(n =>
            {
                var f = n.Replace("|", string.Empty).Replace(" ", string.Empty);
                string[] divisions = null;

                // Operator Split

                if (f.Contains(">="))
                {
                    divisions = f.Split(">=");
                }
                else if (f.Contains("<="))
                {
                    divisions = f.Split("<=");
                }
                else if (f.Contains('>'))
                {
                    divisions = f.Split('>');
                }
                else if (f.Contains('<'))
                {
                    divisions = f.Split('<');
                }
                else if (f.Contains('='))
                {
                    divisions = f.Split('=');
                }

                var name = divisions[0].Split("_")[0].Replace(".", string.Empty);

                var last = indicators.LastOrDefault();
                if (last != null)
                {
                    var lastName = last.Split(".")[0];
                    var lastCount = 1;
                    if (last.Contains('.'))
                    {
                        lastCount = int.Parse(last.Split(".")[1], CultureInfo.InvariantCulture);
                    }
                    if (name == lastName)
                    {
                        indicators[^1] = string.Concat(name, ".", lastCount + 1);
                    }
                    else
                    {
                        indicators.Add(name);
                    }
                }
                else
                {
                    indicators.Add(name);
                }
            });

            return string.Join("_", indicators); ;
        }
    }
}