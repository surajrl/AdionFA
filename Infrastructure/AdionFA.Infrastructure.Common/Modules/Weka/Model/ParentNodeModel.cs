using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class ParentNodeModel : REPTreeNodeModel
    {
        public ParentNodeModel(REPTreeNodeModel repTreeNodeModel)
            : base(repTreeNodeModel)
        {
        }

        public IEnumerable<BacktestModel> ChildNodes { get; set; }
    }
}
