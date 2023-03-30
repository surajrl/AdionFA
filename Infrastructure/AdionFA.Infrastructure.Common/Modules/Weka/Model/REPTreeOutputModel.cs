using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class REPTreeOutputModel
    {
        public int Seed { get; set; }
        public string TreeOutput { get; set; }
        public List<REPTreeNodeModel> NodeOutput { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class REPTreeNodeModel
    {
        public string NodeWithoutFormat { get; set; }
        public List<string> Node { get; set; }
        public string HistoricalData { get; set; }

        public string Label { get; set; }
        public double TotalUP { get; set; }
        public double TotalDOWN { get; set; }
        public double Total { get; set; }

        public double RatioUP { get; set; }
        public double RatioDOWN { get; set; }
        public double RatioMax { get; set; }
        public bool Winner { get; set; }

        public int TotalTradesIs { get; set; }
        public int WinningTradesIs { get; set; }
        public int LosingTradesIs { get; set; }
        public int TotalOpportunityIs { get; set; }
        public int PercentSuccessIs { get; set; }
        public int ProgressivenessIs { get; set; }

        public int TotalTradesOs { get; set; }
        public int WinningTradesOs { get; set; }
        public int LosingTradesOs { get; set; }
        public int TotalOpportunityOs { get; set; }
        public int PercentSuccessOs { get; set; }
        public int ProgressivenessOs { get; set; }

        public double VariationPercent => Math.Abs(PercentSuccessIs - PercentSuccessOs);
        public double Progressiveness => Math.Abs(ProgressivenessIs - ProgressivenessOs);
        public bool WinningStrategy { get; set; }
    }
}
