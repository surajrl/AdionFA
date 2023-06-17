using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.MetaTrader
{
    public class NodeMetaTraderFlyoutViewModel : ViewModelBase
    {
        public NodeMetaTraderFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Nodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodeMetaTrader, StringComparison.Ordinal))
            {
                switch (flyoutModel.Model)
                {
                    case ObservableCollection<REPTreeNodeModel> oc:
                        foreach (var node in oc)
                        {
                            if (node is REPTreeNodeModel)
                            {
                                Nodes.Add(node);
                            }
                        }
                        break;

                    case List<REPTreeNodeModel> list:
                        foreach (var node in list)
                        {
                            if (node is REPTreeNodeModel)
                            {
                                Nodes.Add(node);
                            }
                        }
                        break;

                    default:
                        return;
                }
            }
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
