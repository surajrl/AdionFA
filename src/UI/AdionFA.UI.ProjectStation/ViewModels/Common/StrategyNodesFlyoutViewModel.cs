﻿using AdionFA.Infrastructure.Weka.Model;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Services;
using DynamicData;
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