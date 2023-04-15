using AdionFA.Infrastructure.Common.Base;
using AdionFA.Infrastructure.Enums;

using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public abstract class NodeAssembledModel : TreeNodeBase<List<NodeAssembledModel>, NodeAssembledModel>
    {
        public NodeAssembledModel()
        {
            Nodes = new List<NodeAssembledModel>();
        }

        public override string Label { get; set; }
        public override string Name { get; set; }

        public string TypeNodeName => Type.GetMetadata()?.Code ?? string.Empty;
        public NodeAssembledTypeEnum Type { get; set; }

        public override List<NodeAssembledModel> Nodes { get; set; }
    }
}
