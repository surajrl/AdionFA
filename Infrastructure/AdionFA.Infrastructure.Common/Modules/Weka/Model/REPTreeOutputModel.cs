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
        public List<string> Node { get; set; }
        public double TotalUP { get; set; }
        public double TotalDOWN { get; set; }
        public double RatioDOWN { get; set; }
        public double RatioUP { get; set; }
        public double RatioMax { get; set; }
        public double Total { get; set; }
        public string Label { get; set; }
        public bool Winner { get; set; }
    }
}
