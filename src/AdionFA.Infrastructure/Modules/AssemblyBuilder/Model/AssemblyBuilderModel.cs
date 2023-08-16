using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Modules.Strategy;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.AssemblyBuilder.Model
{
    public class AssemblyBuilderModel
    {
        public AssemblyBuilderModel()
        {
            ChildNodesUP = new();
            ChildNodesDOWN = new();

            AllWinningAssemblyNodes = new();
        }

        public List<SingleNodeModel> ChildNodesUP { get; }
        public List<SingleNodeModel> ChildNodesDOWN { get; }

        public IReadOnlyCollection<AssemblyNodeModel> WinningAssemblyNodesUP => AllWinningAssemblyNodes.Where(winningAssemblyNodes => winningAssemblyNodes.Label == Label.UP).ToList();
        public IReadOnlyCollection<AssemblyNodeModel> WinningAssemblyNodesDOWN => AllWinningAssemblyNodes.Where(winningAssemblyNodes => winningAssemblyNodes.Label == Label.DOWN).ToList();

        public List<AssemblyNodeModel> AllWinningAssemblyNodes { get; set; }

        public bool HasWinningAssemblyNodes => AllWinningAssemblyNodes.Count > 0;
    }
}