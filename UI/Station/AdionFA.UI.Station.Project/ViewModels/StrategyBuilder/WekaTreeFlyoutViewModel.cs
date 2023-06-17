using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Services;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
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
            applicationCommands.SaveNodeCommand.RegisterCommand(SaveNodeCommand);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Nodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleWekaTree, StringComparison.Ordinal))
            {
                Nodes.Clear();
                Nodes.AddRange(((List<REPTreeNodeModel>)flyoutModel.Model).Where(node => node.Winner));
            }
        });

        public ICommand SaveNodeCommand => new DelegateCommand<REPTreeNodeModel>(node =>
        {
            var directory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();
            StrategyBuilderService.SerializeNode(EntityTypeEnum.StrategyBuilder, ProcessArgs.ProjectName, node);
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> Nodes { get; set; }
    }
}