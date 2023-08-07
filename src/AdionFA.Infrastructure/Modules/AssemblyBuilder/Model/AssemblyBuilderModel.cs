using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.AssemblyBuilder.Model
{
    public class AssemblyBuilderModel
    {
        public AssemblyBuilderModel()
        {
            ChildNodesUP = new();
            ChildNodesDOWN = new();

            WinningAssemblyNodesUP = new();
            WinningAssemblyNodesDOWN = new();
        }

        public List<NodeModel> ChildNodesUP { get; }
        public List<NodeModel> ChildNodesDOWN { get; }

        public List<AssemblyNodeModel> WinningAssemblyNodesUP { get; }
        public List<AssemblyNodeModel> WinningAssemblyNodesDOWN { get; }
    }
}