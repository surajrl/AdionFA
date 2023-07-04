﻿using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Modules.Weka.Model
{
    public class NodeModel
    {
        // Node

        public bool WinningStrategy { get; set; }

        public REPTreeNodeModel NodeData { get; set; }

        // Backtest

        public BacktestModel BacktestIS { get; set; } = new();
        public BacktestModel BacktestOS { get; set; } = new();

        public double SuccessRateVariation => Math.Abs(BacktestIS.SuccessRatePercent - BacktestOS.SuccessRatePercent);
        public double ProgressivenessVariation => Math.Abs(BacktestIS.Progressiveness - BacktestOS.Progressiveness);

        public string Name
        {
            get
            {
                var indicators = new List<string>();

                NodeData.Node.ForEach(n =>
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
                var name = $"{indicatorsName}-{NodeData.Label}-{BacktestIS.TotalTrades}-{BacktestOS.TotalTrades}";

                return name;
            }
        }
    }
}