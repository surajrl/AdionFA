using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public class AssembledBuilderModel
    {
        /// <summary>
        /// Correlation nodes from Strategy Builder.
        /// </summary>
        public List<REPTreeNodeModel> ChildNodesUP { get; set; } = new();
        public List<REPTreeNodeModel> ChildNodesDOWN { get; set; } = new();

        /// <summary>
        /// Assembled nodes from Assembled Builder.
        /// </summary>
        public List<AssembledNodeModel> AssembledNodesUP { get; set; } = new();
        public List<AssembledNodeModel> AssembledNodesDOWN { get; set; } = new();
    }
}