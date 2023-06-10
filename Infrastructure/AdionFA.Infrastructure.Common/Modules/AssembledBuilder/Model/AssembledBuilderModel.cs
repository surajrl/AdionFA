using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public class AssembledBuilderModel
    {
        public IList<BacktestModel> ChildBacktestsUP { get; } = new List<BacktestModel>();
        public IList<BacktestModel> ChildBacktestsDOWN { get; } = new List<BacktestModel>();

        public IList<ParentNodeModel> WinningParentNodesUP { get; } = new List<ParentNodeModel>();
        public IList<ParentNodeModel> WinningParentNodesDOWN { get; } = new List<ParentNodeModel>();
    }
}