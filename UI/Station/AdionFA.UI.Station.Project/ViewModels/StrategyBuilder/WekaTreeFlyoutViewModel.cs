using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class WekaTreeFlyoutViewModel : ViewModelBase
    {

        public WekaTreeFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Nodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleWekaTree, StringComparison.Ordinal))
            {
                Nodes.Clear();
                Nodes.AddRange(((List<REPTreeNodeModel>)flyoutModel.ModelOne).Where(node => node.Winner));
            }
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> Nodes { get; set; }
    }
}