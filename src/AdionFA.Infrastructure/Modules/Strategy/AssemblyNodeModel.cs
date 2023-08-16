using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Weka.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Modules.Strategy
{
    public class AssemblyNodeModel : NodeMetadataModel, INodeModel
    {
        private readonly Guid _guid;

        public AssemblyNodeModel()
        {
            _guid = Guid.NewGuid();
        }

        // Assemebly Node

        public REPTreeNodeModel ParentNodeData { get; set; }
        public List<REPTreeNodeModel> ChildNodesData { get; set; }

        public string Name => _guid.ToString();

        public Label Label => ChildNodesData.FirstOrDefault().Label.ToLowerInvariant() == "up" ? Label.UP : Label.DOWN;

        public string WinningUPDirectory => ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription();
        public string WinningDOWNDirectory => ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription();
    }
}
