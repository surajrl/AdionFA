using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Weka.Model;

using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Project.AutoMapper;

using AutoMapper;

using Prism.Ioc;
using Prism.Commands;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using DynamicData;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AddNodeForTestCommand = new DelegateCommand<BacktestModelVM>(AddNode);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(AddNodeForTestCommand);
        }

        private ICommand AddNodeForTestCommand { get; set; }
        public void AddNode(BacktestModelVM backtest)
        {
            backtest.HasTestInMetaTrader = true;
        }

        // Flyout Command

        private ICommand FlyoutCommand { get; set; }
        private void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelation))
            {
                CorrelationModel = flyoutModel.Model != null ? (CorrelationModel)flyoutModel.Model : new CorrelationModel();
                PopulateViewModel();
            }
        }

        private void PopulateViewModel()
        {
            try
            {
                if (CorrelationModel.Success)
                {
                    NodesUP = new();
                    CorrelationModel.ISBacktestUP.ForEach(backtest => NodesUP.Add(new BacktestModelVM { BacktestModel = backtest }));

                    NodesDOWN = new();
                    CorrelationModel.ISBacktestDOWN.ForEach(backtest => NodesDOWN.Add(new BacktestModelVM { BacktestModel = backtest }));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<CorrelationFlyoutViewModel>(ex);
                throw;
            }
        }

        // Bindable Model

        private CorrelationModel _correlationModel;
        public CorrelationModel CorrelationModel
        {
            get => _correlationModel;
            set => SetProperty(ref _correlationModel, value);
        }

        private ObservableCollection<BacktestModelVM> _nodesUP;
        public ObservableCollection<BacktestModelVM> NodesUP
        {
            get => _nodesUP;
            set => SetProperty(ref _nodesUP, value);
        }

        private ObservableCollection<BacktestModelVM> _nodesDown;
        public ObservableCollection<BacktestModelVM> NodesDOWN
        {
            get => _nodesDown;
            set => SetProperty(ref _nodesDown, value);
        }
    }
}
