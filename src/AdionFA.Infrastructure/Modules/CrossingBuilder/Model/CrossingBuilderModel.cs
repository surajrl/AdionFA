using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.CrossingBuilder.Model
{
    public class CrossingBuilderModel
    {
        public CrossingBuilderModel()
        {
            AllWinningStrategyNodes = new();
        }

        public IReadOnlyCollection<StrategyNodeModel> WinningStrategyNodesUP => AllWinningStrategyNodes.Where(winningAssemblyNodes => winningAssemblyNodes.Label == Label.UP).ToList();
        public IReadOnlyCollection<StrategyNodeModel> WinningStrategyNodesDOWN => AllWinningStrategyNodes.Where(winningAssemblyNodes => winningAssemblyNodes.Label == Label.DOWN).ToList();

        public List<StrategyNodeModel> AllWinningStrategyNodes { get; set; }

        public bool HasWinningStrategyNodes => AllWinningStrategyNodes.Count > 0;
    }
}
