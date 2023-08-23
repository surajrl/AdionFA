using AdionFA.Infrastructure.Modules.Builder;
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
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleSingleNodes, StringComparison.Ordinal)
            || (flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssemblyNodes, StringComparison.Ordinal)
            || (flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleStrategyNodes, StringComparison.Ordinal))
            {
                Nodes.Clear();

                switch (flyout.ModelOne)
                {
                    // Single Node

                    case ObservableCollection<SingleNodeModel> observableCollection:
                        Nodes.AddRange(observableCollection);
                        break;

                    case ReadOnlyCollection<SingleNodeModel> readOnlyObservableCollection:
                        Nodes.AddRange(readOnlyObservableCollection);
                        break;

                    case List<SingleNodeModel> list:
                        Nodes.AddRange(list);
                        break;

                    case BuilderModel<SingleNodeModel> builder:
                        Nodes.AddRange(builder.WinningNodesUP);
                        Nodes.AddRange(builder.WinningNodesDOWN);
                        break;

                    case SingleNodeModel node:
                        Nodes.Add(node);
                        break;

                    // Assembly Node

                    case ObservableCollection<AssemblyNodeModel> observableCollection:
                        Nodes.AddRange(observableCollection);
                        break;

                    case ReadOnlyCollection<AssemblyNodeModel> readOnlyObservableCollection:
                        Nodes.AddRange(readOnlyObservableCollection);
                        break;

                    case List<AssemblyNodeModel> list:
                        Nodes.AddRange(list);
                        break;

                    case BuilderModel<AssemblyNodeModel> builder:
                        Nodes.AddRange(builder.WinningNodesUP);
                        Nodes.AddRange(builder.WinningNodesDOWN);
                        break;

                    case AssemblyNodeModel node:
                        Nodes.Add(node);
                        break;

                    // Strategy Node

                    case ObservableCollection<StrategyNodeModel> observableCollection:
                        Nodes.AddRange(observableCollection);
                        break;

                    case ReadOnlyCollection<StrategyNodeModel> readOnlyObservableCollection:
                        Nodes.AddRange(readOnlyObservableCollection);
                        break;

                    case List<StrategyNodeModel> list:
                        Nodes.AddRange(list);
                        break;

                    case BuilderModel<StrategyNodeModel> builder:
                        Nodes.AddRange(builder.WinningNodesUP);
                        Nodes.AddRange(builder.WinningNodesDOWN);
                        break;

                    case StrategyNodeModel node:
                        Nodes.Add(node);
                        break;
                }
            }
        });

        // View Bindings

        public ObservableCollection<INodeModel> Nodes { get; set; }
    }
}
