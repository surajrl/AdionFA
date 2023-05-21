using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Services;
using AutoMapper;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Weka;

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

        public ICommand AddNodeToMetaTrader => new DelegateCommand<REPTreeNodeVM>(node =>
        {
            if (Nodes.Where(n => n.Name == node.Name).Any())
            {
                return;
            }

            Nodes.Add(node);
        });

        public ICommand RemoveNodeFromMetaTrader => new DelegateCommand<REPTreeNodeVM>(node =>
        {
            Nodes.Remove(node);
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeVM> _nodes;

        public ObservableCollection<REPTreeNodeVM> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }
    }
}