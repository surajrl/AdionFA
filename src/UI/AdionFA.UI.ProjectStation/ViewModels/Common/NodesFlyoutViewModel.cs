using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Weka.Model;
using AdionFA.Infrastructure.StrategyBuilder.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using DynamicData;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.Common
{
    public class NodesFlyoutViewModel : ViewModelBase
    {
        public NodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.SaveNodeCommand.RegisterCommand(SaveNodeCommand);

            Nodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodes, StringComparison.Ordinal))
            {
                Nodes.Clear();
                switch (flyout.ModelOne)
                {
                    case ObservableCollection<NodeModel> collection:
                        Nodes.Add(collection);
                        break;

                    case List<NodeModel> list:
                        Nodes.Add(list);
                        break;

                    case StrategyBuilderModel strategyBuilder:
                        Nodes.Add(((StrategyBuilderModel)flyout.ModelOne).WinningNodesUP);
                        Nodes.Add(((StrategyBuilderModel)flyout.ModelOne).WinningNodesDOWN);
                        break;

                    case NodeModel node:
                        Nodes.Add(node);
                        break;
                }
            }
        });

        public static ICommand SaveNodeCommand => new DelegateCommand<NodeModel>(node =>
        {
            var directory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();
            var filename = RegexHelper.GetValidFileName(node.Name, "_") + ".xml";

            SerializerHelper.XMLSerializeObject(node, string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", directory, filename));
        });

        // View Bindings

        public ObservableCollection<NodeModel> Nodes { get; set; }
    }
}