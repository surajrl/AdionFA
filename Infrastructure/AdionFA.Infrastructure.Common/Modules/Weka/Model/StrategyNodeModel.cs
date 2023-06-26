using AdionFA.Infrastructure.Common.Extractor.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class StrategyNodeModel
    {
        public AssemblyNodeModel MainNode { get; set; }
        public List<(REPTreeNodeModel, List<Candle>)> CrossingNodes { get; set; }
    }
}
