using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class REPTreeNodeModel
    {
        public bool WinningStrategy { get; set; }

        // Weka

        public List<string> Node { get; set; }
        public string Label { get; set; }
        public double Total { get; set; }
        public double TotalUP { get; set; }
        public double TotalDOWN { get; set; }
        public double RatioUP { get; set; }
        public double RatioDOWN { get; set; }
        public double RatioMax { get; set; }
        public bool Winner { get; set; }

        // IS

        public BacktestModel BacktestIS { get; set; } = new();
        public BacktestModel BacktestOS { get; set; } = new();


        // Result

        public double SuccessRateVariation => Math.Abs(BacktestIS.SuccessRatePercent - BacktestOS.SuccessRatePercent);
        public double ProgressivenessVariation => Math.Abs(BacktestIS.Progressiveness - BacktestOS.Progressiveness);

        // Node Name

        public string Name
        {
            get
            {
                var indicators = new List<string>();
                Node.ForEach(n =>
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

                var indicatorsName = string.Join("_", indicators);
                var name = $"{indicatorsName}-{Label}-{BacktestIS.TotalTrades}-{BacktestOS.TotalTrades}";
                return name;
            }
        }
    }
}
