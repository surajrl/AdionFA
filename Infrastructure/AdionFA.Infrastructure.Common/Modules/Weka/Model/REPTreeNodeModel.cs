using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class REPTreeNodeModel
    {
        public REPTreeNodeModel()
        {
        }

        public REPTreeNodeModel(REPTreeNodeModel parentNodeData)
        {
            Node = parentNodeData.Node;
            Label = parentNodeData.Label;
            Total = parentNodeData.Total;
            TotalUP = parentNodeData.TotalUP;
            TotalDOWN = parentNodeData.TotalDOWN;
            RatioUP = parentNodeData.RatioUP;
            RatioDOWN = parentNodeData.RatioDOWN;
            RatioMax = parentNodeData.RatioMax;
            Winner = parentNodeData.Winner;
        }

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
    }
}
