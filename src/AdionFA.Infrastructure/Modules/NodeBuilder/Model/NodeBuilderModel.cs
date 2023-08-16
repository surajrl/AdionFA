using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Modules.Strategy;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.Infrastructure.NodeBuilder.Model
{
    public class NodeBuilderModel : BindableBase
    {
        public NodeBuilderModel()
        {
            AllWinningNodes = new();
        }

        public IReadOnlyCollection<SingleNodeModel> WinningNodesUP
        {
            get
            {
                return AllWinningNodes.Where(winningNode => winningNode.Label == Label.UP).ToList();
            }
        }

        public IReadOnlyCollection<SingleNodeModel> WinningNodesDOWN
        {
            get
            {
                return AllWinningNodes.Where(winningNode => winningNode.Label == Label.DOWN).ToList();
            }
        }

        public ObservableCollection<SingleNodeModel> AllWinningNodes { get; set; }
    }
}
