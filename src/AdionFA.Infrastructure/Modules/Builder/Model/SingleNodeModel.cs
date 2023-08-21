using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Weka.Model;
using System;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public class SingleNodeModel : NodeMetadataModel, INodeModel
    {
        private readonly Guid _guid;

        public SingleNodeModel()
        {
            _guid = Guid.NewGuid();
        }

        // Node

        public REPTreeNodeModel NodeData { get; set; }

        public string Name => _guid.ToString();
        public Label Label => NodeData.Label.ToLowerInvariant() == "up" ? Label.UP : Label.DOWN;

        public string WinningUPDirectory => ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription();
        public string WinningDOWNDirectory => ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription();
    }
}
