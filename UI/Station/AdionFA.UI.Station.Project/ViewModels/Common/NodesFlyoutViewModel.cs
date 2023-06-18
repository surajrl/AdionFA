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
    public class NodesFlyoutViewModel : ViewModelBase
    {
        public NodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Nodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodes, StringComparison.Ordinal)
                && (flyout.ModelOne != null))
            {
                Nodes.Clear();
                if (flyout.ModelOne is ObservableCollection<REPTreeNodeModel> collection)
                {
                    Nodes.Add(collection);
                }
                else
                {
                    Nodes.Add((List<REPTreeNodeModel>)flyout.ModelOne);
                }
            }
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> Nodes { get; set; }
    }
}