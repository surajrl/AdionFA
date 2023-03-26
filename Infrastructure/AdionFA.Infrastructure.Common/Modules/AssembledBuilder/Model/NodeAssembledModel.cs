using AdionFA.Infrastructure.Common.Base;
using AdionFA.Infrastructure.Enums;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model
{
    public abstract class NodeAssembledModel : TreeNodeBase<List<NodeAssembledModel>, NodeAssembledModel>
    {
        #region Ctor

        public NodeAssembledModel()
        {
            Nodes = new List<NodeAssembledModel>();
        }
        
        #endregion

        public override string Label { get; set; }
        public override string Name { get; set; }

        public string TypeNodeName => Type.GetMetadata()?.Code ?? string.Empty;
        public NodeAssembledTypeEnum Type { get; set; }

        public override List<NodeAssembledModel> Nodes { get; set; }
    }
}
