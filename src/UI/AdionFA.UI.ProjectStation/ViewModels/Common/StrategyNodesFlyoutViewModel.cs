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
                        StrategyNodes.AddRange(collection);
                        break;

                    case List<StrategyNodeModel> list:
                        StrategyNodes.AddRange(list);
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
