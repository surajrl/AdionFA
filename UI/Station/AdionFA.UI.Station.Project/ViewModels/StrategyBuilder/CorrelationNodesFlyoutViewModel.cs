using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using DynamicData;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationNodesFlyoutViewModel : ViewModelBase
    {
        public CorrelationNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            CorrelationNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelationNodes, StringComparison.Ordinal))
            {
                CorrelationNodes.Clear();
                CorrelationNodes.Add(((StrategyBuilderModel)flyout.ModelOne).CorrelationNodesDOWN);
                CorrelationNodes.Add(((StrategyBuilderModel)flyout.ModelOne).CorrelationNodesUP);
            }
        });


        // View Bindings

        public ObservableCollection<REPTreeNodeModel> CorrelationNodes { get; set; }
    }
}