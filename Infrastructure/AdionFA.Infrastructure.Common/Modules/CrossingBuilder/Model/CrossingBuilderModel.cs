using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.CrossingBuilder.Model
{
    public class CrossingBuilderModel
    {
        public List<StrategyNodeModel> WinningStrategyNodesUP { get; } = new();
        public List<StrategyNodeModel> WinningStrategyNodesDOWN { get; } = new();
    }
}
