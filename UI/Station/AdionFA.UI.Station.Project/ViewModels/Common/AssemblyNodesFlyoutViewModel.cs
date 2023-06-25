using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using DynamicData;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.Common
{
    public class AssemblyNodesFlyoutViewModel : ViewModelBase
    {
        public AssemblyNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTraderCommand);

            AssemblyNodes = new();
            ChildNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssemblyNodes, StringComparison.Ordinal))
            {
                AssemblyNodes.Clear();
                ChildNodes.Clear();

                switch (flyout.ModelOne)
                {
                    case ObservableCollection<AssemblyNodeModel> collection:
                        AssemblyNodes.Add(collection);
                        break;

                    case List<AssemblyNodeModel> list:
                        AssemblyNodes.Add(list);
                        break;

                    case AssemblyNodeModel assembledNode:
                        AssemblyNodes.Add(assembledNode);
                        break;
                }

                switch (flyout.ModelTwo)
                {
                    case ObservableCollection<REPTreeNodeModel> collection:
                        ChildNodes.Add(collection);
                        break;

                    case List<REPTreeNodeModel> list:
                        ChildNodes.Add(list);
                        break;

                    case REPTreeNodeModel node:
                        ChildNodes.Add(node);
                        break;
                }
            }
        });

        public ICommand RemoveNodeFromMetaTraderCommand => new DelegateCommand<object>(item =>
        {
            if (item is AssemblyNodeModel assembledNode)
            {
                AssemblyNodes.Remove(assembledNode);
            }
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> ChildNodes { get; set; }
        public ObservableCollection<AssemblyNodeModel> AssemblyNodes { get; set; }
    }
}
