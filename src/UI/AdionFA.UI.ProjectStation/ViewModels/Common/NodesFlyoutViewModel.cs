using AdionFA.Infrastructure.Modules.Strategy;
using AdionFA.Infrastructure.NodeBuilder.Model;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels.Common
{
    public class NodesFlyoutViewModel : ViewModelBase
    {
        public NodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Nodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodes, StringComparison.Ordinal))
            {
                Nodes.Clear();
                switch (flyout.ModelOne)
                {
                    case ObservableCollection<SingleNodeModel> collection:
                        Nodes.AddRange(collection);
                        break;

                    case List<SingleNodeModel> list:
                        Nodes.AddRange(list);
                        break;

                    case NodeBuilderModel strategyBuilder:
                        Nodes.AddRange(((NodeBuilderModel)flyout.ModelOne).WinningNodesUP);
                        Nodes.AddRange(((NodeBuilderModel)flyout.ModelOne).WinningNodesDOWN);
                        break;

                    case SingleNodeModel node:
                        Nodes.Add(node);
                        break;
                }
            }
        });

        // View Bindings

        public ObservableCollection<SingleNodeModel> Nodes { get; set; }
    }
}