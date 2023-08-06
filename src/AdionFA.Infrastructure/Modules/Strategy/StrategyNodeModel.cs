using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.Weka.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        public List<REPTreeNodeModel> ParentNodesData { get; set; }
        public List<REPTreeNodeModel> ChildNodesData { get; set; }

        /// <summary>
        /// REPTreeNodeModel is the node data. <br></br>
        /// int is the symbol ID. <br></br>
        /// string is the symbol name. <br></br>
        /// </summary>
        public List<SerializableTuple<REPTreeNodeModel, int, string>> CrossingNodesData { get; set; }

        public string Name => _guid.ToString();
    }
}
