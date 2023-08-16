using System.Collections.Generic;

namespace AdionFA.Infrastructure.Weka.Model
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
        public decimal Total { get; set; }
        public decimal TotalUP { get; set; }
        public decimal TotalDOWN { get; set; }
        public decimal RatioUP { get; set; }
        public decimal RatioDOWN { get; set; }
        public decimal RatioMax { get; set; }
        public bool Winner { get; set; }
    }
}
