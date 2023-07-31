using AdionFA.Infrastructure.Modules.Weka.Model;
using AdionFA.Infrastructure.Weka.Model;
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