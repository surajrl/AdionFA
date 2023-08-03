using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.AssemblyBuilder.Model
{
    public class AssemblyBuilderModel
    {
        public List<NodeModel> ChildNodesUP { get; set; } = new();
        public List<NodeModel> ChildNodesDOWN { get; set; } = new();

        public List<AssemblyNodeModel> WinningAssemblyNodesUP { get; set; } = new();
        public List<AssemblyNodeModel> WinningAssemblyNodesDOWN { get; set; } = new();
    }
}