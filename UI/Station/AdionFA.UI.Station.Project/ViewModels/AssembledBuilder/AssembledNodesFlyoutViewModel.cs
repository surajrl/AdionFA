using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
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

            AssembledNodes = new ObservableCollection<AssembledNodeModel>();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssembledNodes, StringComparison.Ordinal))
            {
                if (flyoutModel.Model != null)
                {
                    AssembledNodes = new((List<AssembledNodeModel>)flyoutModel.Model);

                }
            }
        });

        // View Bindings

        private ObservableCollection<AssembledNodeModel> _assembledNodes;
        public ObservableCollection<AssembledNodeModel> AssembledNodes
        {
            get => _assembledNodes;
            set => SetProperty(ref _assembledNodes, value);
        }
    }
}
