using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.ObjectModel;

namespace AdionFA.Infrastructure.CrossingBuilder.Model
{
    public class CrossingBuilderModel
    {
        public CrossingBuilderModel()
        {
            WinningStrategyNodesUP = new();
            WinningStrategyNodesDOWN = new();
        }

        public ObservableCollection<StrategyNodeModel> WinningStrategyNodesUP { get; }
        public ObservableCollection<StrategyNodeModel> WinningStrategyNodesDOWN { get; }

        public bool IsStarted => WinningStrategyNodesDOWN.Count > 0 || WinningStrategyNodesUP.Count > 0;
    }
}
