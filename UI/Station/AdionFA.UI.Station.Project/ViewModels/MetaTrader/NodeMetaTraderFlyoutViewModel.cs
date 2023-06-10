using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.MetaTrader
{
    public class NodeMetaTraderFlyoutViewModel : ViewModelBase
    {
        public NodeMetaTraderFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.AddNodeToMetaTrader.RegisterCommand(AddNodeToMetaTrader);
            applicationCommands.RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTrader);

            Nodes = new();
        }

        public ICommand AddNodeToMetaTrader => new DelegateCommand<REPTreeNodeModel>(node =>
        {
            if (Nodes.Where(n => n.Equals(node)).Any())
            {
                return;
            }

            Nodes.Add(node);
        });

        public ICommand RemoveNodeFromMetaTrader => new DelegateCommand<REPTreeNodeModel>(node =>
        {
            Nodes.Remove(node);
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeModel> _nodes;
        public ObservableCollection<REPTreeNodeModel> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }
    }
}