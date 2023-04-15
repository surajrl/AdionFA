using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Logger.Helpers;

using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;

using Prism.Commands;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using System.Collections;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AddNodeForTestCommand = new DelegateCommand<REPTreeNodeVM>(AddNode);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(AddNodeForTestCommand);

            CorrelationNodesUP = new();
            CorrelationNodesDOWN = new();
        }

        private ICommand AddNodeForTestCommand { get; set; }
        public void AddNode(REPTreeNodeVM node)
        {
            node.HasTestInMetaTrader = true;
        }

        // Flyout Command

        private ICommand FlyoutCommand { get; set; }
        private void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelation))
            {
                AllWinningNodes = flyoutModel.Model != null ? (ObservableCollection<REPTreeNodeVM>)flyoutModel.Model : new ObservableCollection<REPTreeNodeVM>();
                PopulateViewModel();
            }
        }

        private void PopulateViewModel()
        {
            try
            {
                CorrelationNodesUP.Clear();
                CorrelationNodesDOWN.Clear();

                foreach (var node in AllWinningNodes)
                {
                    if (node.CorrelationPass)
                    {
                        switch (node.Label.ToLower())
                        {
                            case "up":
                                CorrelationNodesUP.Add(node);
                                break;
                            case "down":
                                CorrelationNodesDOWN.Add(node);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<CorrelationFlyoutViewModel>(ex);
                throw;
            }
        }

        // Bindable Model

        private ObservableCollection<REPTreeNodeVM> _allNodes;
        public ObservableCollection<REPTreeNodeVM> AllWinningNodes
        {
            get => _allNodes;
            set => SetProperty(ref _allNodes, value);
        }

        private ObservableCollection<REPTreeNodeVM> _correlationNodesUP;
        public ObservableCollection<REPTreeNodeVM> CorrelationNodesUP
        {
            get => _correlationNodesUP;
            set => SetProperty(ref _correlationNodesUP, value);
        }

        private ObservableCollection<REPTreeNodeVM> _correlationNodesDOWN;
        public ObservableCollection<REPTreeNodeVM> CorrelationNodesDOWN
        {
            get => _correlationNodesDOWN;
            set => SetProperty(ref _correlationNodesDOWN, value);
        }
    }
}
