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
    public class SingleNodesFlyoutViewModel : ViewModelBase
    {
        public SingleNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            SingleNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodes, StringComparison.Ordinal))
            {
                SingleNodes.Clear();
                switch (flyout.ModelOne)
                {
                    case ObservableCollection<SingleNodeModel> collection:
                        SingleNodes.AddRange(collection);
                        break;

                    case List<SingleNodeModel> list:
                        SingleNodes.AddRange(list);
                        break;

                    case BuilderModel<SingleNodeModel> nodeBuilder:
                        SingleNodes.AddRange(((BuilderModel<SingleNodeModel>)flyout.ModelOne).WinningNodesUP);
                        SingleNodes.AddRange(((BuilderModel<SingleNodeModel>)flyout.ModelOne).WinningNodesDOWN);
                        break;

                    case SingleNodeModel node:
                        SingleNodes.Add(node);
                        break;
                }
            }
        });

        // View Bindings

        public ObservableCollection<SingleNodeModel> SingleNodes { get; set; }
    }
}