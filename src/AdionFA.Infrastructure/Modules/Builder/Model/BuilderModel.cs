using AdionFA.Domain.Enums;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public class BuilderModel<T> : BindableBase where T : INodeModel
    {
        public BuilderModel()
        {
            AllWinningNodes = new ObservableCollection<T>();
        }

        public ObservableCollection<T> AllWinningNodes { get; }

        public ReadOnlyObservableCollection<T> WinningNodesUP => new(new(AllWinningNodes.Where(node => node.Label == Label.UP)));

        public ReadOnlyObservableCollection<T> WinningNodesDOWN => new(new(AllWinningNodes.Where(node => node.Label == Label.DOWN)));
    }
}
