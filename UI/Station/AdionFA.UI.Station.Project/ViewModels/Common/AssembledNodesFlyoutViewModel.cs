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
    public class AssembledNodesFlyoutViewModel : ViewModelBase
    {
        public AssembledNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AssembledNodes = new();
            ChildNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssembledNodes, StringComparison.Ordinal))
            {
                AssembledNodes.Clear();
                if (flyout.ModelOne is ObservableCollection<AssembledNodeModel> assembledNodeCollection)
                {
                    AssembledNodes.Add(assembledNodeCollection);
                }
                else
                {
                    AssembledNodes.Add((List<AssembledNodeModel>)flyout.ModelOne);
                }

                ChildNodes.Clear();
                if (flyout.ModelTwo is ObservableCollection<REPTreeNodeModel> nodeCollection)
                {
                    ChildNodes.Add(nodeCollection);
                }
                else
                {
                    ChildNodes.Add((List<REPTreeNodeModel>)flyout.ModelTwo);
                }
            }
        });

        // View Bindings

        public ObservableCollection<REPTreeNodeModel> ChildNodes { get; set; }
        public ObservableCollection<AssembledNodeModel> AssembledNodes { get; set; }
    }
}
