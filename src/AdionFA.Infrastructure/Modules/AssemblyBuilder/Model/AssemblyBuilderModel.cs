using AdionFA.Infrastructure.Modules.Strategy;
using System.CodeDom;
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

        public bool IsMultiAssembly { get; set; }

        public List<NodeModel> ChildNodesUP { get; set; }
        public List<NodeModel> ChildNodesDOWN { get; set; }

        public List<AssemblyNodeModel> WinningAssemblyNodesUP { get; set; }
        public List<AssemblyNodeModel> WinningAssemblyNodesDOWN { get; set; }
    }
}