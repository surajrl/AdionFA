using AdionFA.Infrastructure.Common.Modules.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Model
{
    public class StrategyBuilderModel
    {
        public List<NodeModel> WinningNodesUP { get; set; } = new();
        public List<NodeModel> WinningNodesDOWN { get; set; } = new();
    }
}
