using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
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

            Backtests = new ObservableCollection<BacktestModel>();
            WinningNodes = new ObservableCollection<ParentNodeModel>();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAssembledNodes, StringComparison.Ordinal))
            {
                Backtests = new((List<BacktestModel>)flyoutModel.ModelOne);
                WinningNodes = new((List<ParentNodeModel>)flyoutModel.ModelTwo);

                foreach (var node in WinningNodes)
                {
                    node.ChildNodes = Backtests;
                }
            }
        });

        // View Bindings

        private ObservableCollection<BacktestModel> _backtests;
        public ObservableCollection<BacktestModel> Backtests
        {
            get => _backtests;
            set => SetProperty(ref _backtests, value);
        }

        private ObservableCollection<ParentNodeModel> _winningNodes;
        public ObservableCollection<ParentNodeModel> WinningNodes
        {
            get => _winningNodes;
            set => SetProperty(ref _winningNodes, value);
        }
    }
}
