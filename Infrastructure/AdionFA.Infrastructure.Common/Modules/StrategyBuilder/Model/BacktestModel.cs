using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model
{
    public class BacktestModel
    {
        public string Label { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PeriodId { get; set; }
        public decimal Variation { get; set; }

        public List<string> Node { get; set; }
        public string LastNode => Node.LastOrDefault();
        public string Name => NodeName();
        public string NodeName()
        {
            List<string> indicators = new();
            Node.ForEach(n =>
            {
                string f = n.Replace("|", string.Empty).Replace(" ", string.Empty);
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
                else if (f.Contains(">"))
                {
                    divisions = f.Split(">");
                }
                else if (f.Contains("<"))
                {
                    divisions = f.Split("<");
                }
                else if (f.Contains("="))
                {
                    divisions = f.Split("=");
                }

                string name = divisions[0].Split("_")[0].Replace(".", string.Empty);

                string last = indicators.LastOrDefault();
                if (last != null)
                {
                    string lastName = last.Split(".")[0];
                    int lastCount = 1;
                    if (last.Contains("."))
                    {
                        int.TryParse(last.Split(".")[1], out lastCount);
                    }
                    if (name == lastName)
                    {
                        indicators[indicators.Count - 1] = string.Concat(name, ".", lastCount + 1);
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

        public int TotalOpportunity { get; set; }
        public int TotalTrades { get; set; }
        public int WinningTrades { get; set; }
        public int LosingTrades { get; set; }

        private double? percentSuccess;
        public double PercentSuccess
        {
            get => percentSuccess ?? (TotalTrades > 0 && WinningTrades > 0 ? WinningTrades * 100 / TotalTrades : 0);
            set { percentSuccess = value; }
        }

        private double? progressiveness;
        public double Progressiveness
        {
            get => progressiveness ?? (TotalTrades > 0 && TotalOpportunity > 0 ? TotalTrades * 100 / TotalOpportunity : 0);
            set => progressiveness = value;
        }

        public bool CorrelationPass { get; set; }

        public List<BacktestOperationModel> Backtests { get; set; }
    }
}
