using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class REPTreeNodeModel
    {
        public REPTreeNodeModel()
        {
        }

        public REPTreeNodeModel(REPTreeNodeModel repTreeNodeModel)
        {
            HistoricalData = repTreeNodeModel.HistoricalData;

            Node = repTreeNodeModel.Node;
            TotalUP = repTreeNodeModel.TotalUP;
            TotalDOWN = repTreeNodeModel.TotalDOWN;
            RatioUP = repTreeNodeModel.RatioUP;
            RatioDOWN = repTreeNodeModel.RatioDOWN;
            RatioMax = repTreeNodeModel.RatioMax;
            Total = repTreeNodeModel.Total;
            Label = repTreeNodeModel.Label;
            Winner = repTreeNodeModel.Winner;

            TotalTradesIs = repTreeNodeModel.TotalTradesIs;
            WinningTradesIs = repTreeNodeModel.WinningTradesIs;
            LosingTradesIs = repTreeNodeModel.LosingTradesIs;
            TotalOpportunityIs = repTreeNodeModel.TotalOpportunityIs;
            SuccessRatePercentIs = repTreeNodeModel.SuccessRatePercentIs;
            ProgressivenessIs = repTreeNodeModel.TotalOpportunityIs;

            TotalTradesOs = repTreeNodeModel.TotalTradesOs;
            WinningTradesOs = repTreeNodeModel.WinningTradesOs;
            LosingTradesOs = repTreeNodeModel.LosingTradesOs;
            TotalOpportunityOs = repTreeNodeModel.TotalOpportunityOs;
            SuccessRatePercentOs = repTreeNodeModel.SuccessRatePercentOs;
            ProgressivenessOs = repTreeNodeModel.ProgressivenessOs;

            SuccessRateVariation = repTreeNodeModel.SuccessRateVariation;
            Progressiveness = repTreeNodeModel.Progressiveness;
            WinningStrategy = repTreeNodeModel.WinningStrategy;
        }

        public string HistoricalData { get; set; }

        // Weka

        public List<string> Node { get; set; }
        public double TotalUP { get; set; }
        public double TotalDOWN { get; set; }
        public double RatioUP { get; set; }
        public double RatioDOWN { get; set; }
        public double RatioMax { get; set; }
        public double Total { get; set; }
        public string Label { get; set; }
        public bool Winner { get; set; }

        // IS

        public int TotalTradesIs { get; set; }
        public int WinningTradesIs { get; set; }
        public int LosingTradesIs { get; set; }
        public double TotalOpportunityIs { get; set; }
        public double SuccessRatePercentIs { get; set; }
        public double ProgressivenessIs { get; set; }

        // OS

        public int TotalTradesOs { get; set; }
        public int WinningTradesOs { get; set; }
        public int LosingTradesOs { get; set; }
        public double TotalOpportunityOs { get; set; }
        public double SuccessRatePercentOs { get; set; }
        public double ProgressivenessOs { get; set; }

        // Result

        public double SuccessRateVariation { get; set; }
        public double Progressiveness { get; set; }
        public bool WinningStrategy { get; set; }

        // Node Name

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
