using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Model
{
    public class StrategyBuilderModel
    {
        public List<REPTreeNodeModel> WinningNodesUP { get; set; } = new();
        public List<REPTreeNodeModel> WinningNodesDOWN { get; set; } = new();
    }
}
