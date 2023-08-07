using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.Weka.Model;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Modules.Strategy
{
    public class StrategyNodeModel : NodeMetadataModel
    {
        private readonly Guid _guid;

        public StrategyNodeModel()
        {
            _guid = Guid.NewGuid();
        }

        // Strategy Node

        public List<REPTreeNodeModel> ParentNodesData { get; init; }
        public List<REPTreeNodeModel> ChildNodesData { get; init; }
        public List<SerializableTuple<REPTreeNodeModel, int, string>> CrossingNodesData { get; init; }

        public bool HasParentNodes => ParentNodesData.Count > 0;

        public string Name => _guid.ToString();
    }
}
