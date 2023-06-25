using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class StrategyNodeModel
    {
        public AssemblyNodeModel MainNode { get; set; }
        public List<REPTreeNodeModel> CrossingNodes { get; set; }
    }
}
