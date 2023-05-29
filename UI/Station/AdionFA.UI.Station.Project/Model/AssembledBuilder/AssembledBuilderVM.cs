using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Station.Project.Model.AssembledBuilder
{
    public class AssembledBuilderVM : ViewModelBase
    {
        public AssembledBuilderVM()
        {
            UPNodes = new ObservableCollection<REPTreeNodeVM>();
            DOWNNodes = new ObservableCollection<REPTreeNodeVM>();
        }

        private ObservableCollection<REPTreeNodeVM> _upNodes;

        public ObservableCollection<REPTreeNodeVM> UPNodes
        {
            get => _upNodes;
            set => SetProperty(ref _upNodes, value);
        }

        private ObservableCollection<REPTreeNodeVM> _downNodes;

        public ObservableCollection<REPTreeNodeVM> DOWNNodes
        {
            get => _downNodes;
            set => SetProperty(ref _downNodes, value);
        }
    }
}