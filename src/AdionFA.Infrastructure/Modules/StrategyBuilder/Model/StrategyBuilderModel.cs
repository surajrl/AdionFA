using AdionFA.Infrastructure.Modules.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.StrategyBuilder.Model
{
    public class StrategyBuilderModel
    {
        public List<NodeModel> WinningNodesUP { get; set; } = new();
        public List<NodeModel> WinningNodesDOWN { get; set; } = new();
    }
}
