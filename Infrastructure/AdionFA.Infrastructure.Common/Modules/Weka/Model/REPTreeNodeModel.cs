using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class REPTreeNodeModel
    {
        // Weka

        public List<string> Node { get; set; }
        public double TotalUP { get; set; }
        public double TotalDOWN { get; set; }
        public double RatioDOWN { get; set; }
        public double RatioUP { get; set; }
        public double RatioMax { get; set; }
        public double Total { get; set; }
        public string Label { get; set; }
        public bool Winner { get; set; }

        // IS

        public int TotalTradesIs { get; set; }
        public int WinningTradesIs { get; set; }
        public int LosingTradesIs { get; set; }
        public double TotalOpportunityIs { get; set; }
        public double PercentSuccessIs { get; set; }
        public double ProgressivenessIs { get; set; }

        // OS

        public int TotalTradesOs { get; set; }
        public int WinningTradesOs { get; set; }
        public int LosingTradesOs { get; set; }
        public double TotalOpportunityOs { get; set; }
        public double PercentSuccessOs { get; set; }
        public double ProgressivenessOs { get; set; }

        // Other

        public double VariationPercent { get; set; }
        public double Progressiveness { get; set; }
        public bool WinningStrategy { get; set; }
        public string HistoricalData { get; set; }
    }
}