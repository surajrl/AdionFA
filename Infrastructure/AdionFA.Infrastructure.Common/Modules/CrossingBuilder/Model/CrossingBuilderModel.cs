using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Modules.CrossingBuilder.Model
{
    public class CrossingBuilderModel
    {
        public List<StrategyNodeModel> StrategyNodesUP { get; } = new();
        public List<StrategyNodeModel> StrategyNodesDOWN { get; } = new();

        public List<StrategyNodeModel> WinningStrategyNodesUP { get; } = new();
        public List<StrategyNodeModel> WinningStrategyNodesDOWN { get; } = new();
    }
}
