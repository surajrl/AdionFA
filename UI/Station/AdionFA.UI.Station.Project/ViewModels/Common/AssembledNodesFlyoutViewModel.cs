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
    public class AssembledNodesFlyoutViewModel : ViewModelBase
    {
        public AssembledNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTraderCommand);

            AssembledNodes = new();
            ChildNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssembledNodes, StringComparison.Ordinal))
            {
                AssembledNodes.Clear();
                ChildNodes.Clear();

                switch (flyout.ModelOne)
                {
                    case ObservableCollection<AssembledNodeModel> collection:
                        AssembledNodes.Add(collection);
                        break;

                    case List<AssembledNodeModel> list:
                        AssembledNodes.Add(list);
                        break;

                    case AssembledNodeModel assembledNode:
                        AssembledNodes.Add(assembledNode);
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
            if (item is AssembledNodeModel assembledNode)
            {
                AssembledNodes.Remove(assembledNode);
            }
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> ChildNodes { get; set; }
        public ObservableCollection<AssembledNodeModel> AssembledNodes { get; set; }
    }
}
