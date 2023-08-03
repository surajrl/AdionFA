using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.NodeBuilder.Model
{
    public class NodeBuilderModel
    {
        public List<NodeModel> WinningNodesUP { get; set; } = new();
        public List<NodeModel> WinningNodesDOWN { get; set; } = new();

        public bool HasWinningNodes => WinningNodesUP.Count > 0 || WinningNodesDOWN.Count > 0;
    }
}
