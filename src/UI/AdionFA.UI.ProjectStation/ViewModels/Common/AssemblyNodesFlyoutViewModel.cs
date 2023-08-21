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
    public class AssemblyNodesFlyoutViewModel : ViewModelBase
    {
        public AssemblyNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTraderCommand);

            AssemblyNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssemblyNodes, StringComparison.Ordinal))
            {
                AssemblyNodes.Clear();

                switch (flyout.ModelOne)
                {
                    case ObservableCollection<AssemblyNodeModel> collection:
                        AssemblyNodes.AddRange(collection);
                        break;

                    case List<AssemblyNodeModel> list:
                        AssemblyNodes.AddRange(list);
                        break;

                    case BuilderModel<AssemblyNodeModel> nodeBuilder:
                        AssemblyNodes.AddRange(((BuilderModel<AssemblyNodeModel>)flyout.ModelOne).WinningNodesUP);
                        AssemblyNodes.AddRange(((BuilderModel<AssemblyNodeModel>)flyout.ModelOne).WinningNodesDOWN);
                        break;
                    case AssemblyNodeModel assemblyNode:
                        AssemblyNodes.Add(assemblyNode);
                        break;
                }
            }
        });

        public ICommand RemoveNodeFromMetaTraderCommand => new DelegateCommand<object>(item =>
        {
            if (item is AssemblyNodeModel assemblyNode)
            {
                AssemblyNodes.Remove(assemblyNode);
            }
        });

        // View Bindings

        public ObservableCollection<AssemblyNodeModel> AssemblyNodes { get; set; }
    }
}
