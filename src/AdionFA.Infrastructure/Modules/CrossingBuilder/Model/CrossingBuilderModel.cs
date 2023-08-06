using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.CrossingBuilder.Model
{
    public class CrossingBuilderModel
    {
        public CrossingBuilderModel()
        {
            WinningStrategyNodesUP = new();
            WinningStrategyNodesDOWN = new();
        }

        public List<StrategyNodeModel> WinningStrategyNodesUP { get; set; }
        public List<StrategyNodeModel> WinningStrategyNodesDOWN { get; set; }
    }
}
