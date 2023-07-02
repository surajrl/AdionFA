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
    public class StrategyNodesFlyoutViewModel : ViewModelBase
    {
        public StrategyNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            StrategyNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleStrategyNodes, StringComparison.Ordinal))
            {
                StrategyNodes.Clear();

                switch (flyout.ModelOne)
                {
                    case ObservableCollection<StrategyNodeModel> collection:
                        StrategyNodes.Add(collection);
                        break;

                    case List<StrategyNodeModel> list:
                        StrategyNodes.Add(list);
                        break;

                    case StrategyNodeModel strategyNode:
                        StrategyNodes.Add(strategyNode);
                        break;
                }
            }
        });

        // View Bindings

        public ObservableCollection<StrategyNodeModel> StrategyNodes { get; set; }
    }
}
