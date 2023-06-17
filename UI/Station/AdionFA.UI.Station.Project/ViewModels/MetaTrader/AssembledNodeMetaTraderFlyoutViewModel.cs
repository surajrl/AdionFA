using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.MetaTrader
{
    public class AssembledNodeMetaTraderFlyoutViewModel : ViewModelBase
    {
        public AssembledNodeMetaTraderFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssembledNodeMetaTrader, StringComparison.Ordinal))
            {
                AssembledNode = (AssembledNodeModel)flyoutModel.Model;
            }
        });

        // View Bindings

        private AssembledNodeModel _assembledNode;
        public AssembledNodeModel AssembledNode
        {
            get => _assembledNode;
            set => SetProperty(ref _assembledNode, value);
        }
    }
}
