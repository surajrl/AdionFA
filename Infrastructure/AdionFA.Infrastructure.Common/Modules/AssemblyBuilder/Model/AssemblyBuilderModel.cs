using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssemblyBuilder.Model
{
    public class AssemblyBuilderModel
    {
        /// <summary>
        /// Correlation nodes from Strategy Builder.
        /// </summary>
        public List<REPTreeNodeModel> ChildNodesUP { get; set; } = new();
        public List<REPTreeNodeModel> ChildNodesDOWN { get; set; } = new();

        /// <summary>
        /// Assembly nodes from Assembly Builder.
        /// </summary>
        public List<AssemblyNodeModel> WinningAssemblyNodesUP { get; set; } = new();
        public List<AssemblyNodeModel> WinningAssemblyNodesDOWN { get; set; } = new();
    }
}