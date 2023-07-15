using AdionFA.Infrastructure.Weka.Model;
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
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssemblyNodes, StringComparison.Ordinal))
            {
                AssemblyNodes.Clear();

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

        public ObservableCollection<AssemblyNodeModel> AssemblyNodes { get; set; }
    }
}
