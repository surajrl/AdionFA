using AdionFA.Infrastructure.Common.Modules.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssemblyBuilder.Model
{
    public class AssemblyBuilderModel
    {
        public List<NodeModel> ChildNodesUP { get; set; } = new();
        public List<NodeModel> ChildNodesDOWN { get; set; } = new();

        public List<AssemblyNodeModel> WinningAssemblyNodesUP { get; set; } = new();
        public List<AssemblyNodeModel> WinningAssemblyNodesDOWN { get; set; } = new();
    }
}