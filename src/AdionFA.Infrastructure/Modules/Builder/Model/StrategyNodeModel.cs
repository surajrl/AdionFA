using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.Weka.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public class StrategyNodeModel : NodeMetadataModel, INodeModel
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

        public string Name => _guid.ToString();
        public Label Label => ChildNodesData.FirstOrDefault().Label.ToLowerInvariant() == "up" ? Label.UP : Label.DOWN;

        public string WinningUPDirectory => ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription();
        public string WinningDOWNDirectory => ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription();
    }
}
