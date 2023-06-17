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

namespace AdionFA.UI.Station.Project.ViewModels.AssembledBuilder
{
    public class AssembledNodesFlyoutViewModel : ViewModelBase
    {
        public AssembledNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AssembledNodes = new();
            ChildNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssembledNodes, StringComparison.Ordinal))
            {
                if (flyoutModel.ModelOne != null)
                {
                    AssembledNodes.Clear();
                    AssembledNodes.Add((List<AssembledNodeModel>)flyoutModel.ModelOne);

                    ChildNodes.Clear();
                    ChildNodes.Add((List<REPTreeNodeModel>)flyoutModel.ModelTwo);

                }
            }
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> ChildNodes { get; set; }
        public ObservableCollection<AssembledNodeModel> AssembledNodes { get; set; }
    }
}
