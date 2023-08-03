using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.CrossingBuilder.Model
{
    public class CrossingBuilderModel
    {
        public List<StrategyNodeModel> WinningStrategyNodesUP { get; } = new();
        public List<StrategyNodeModel> WinningStrategyNodesDOWN { get; } = new();
    }
}
