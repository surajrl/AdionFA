using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Services;
using AutoMapper;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Weka;

namespace AdionFA.UI.Station.Project.ViewModels.MetaTrader
{
    public class NodeMetaTraderFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectServiceAgent _projectService;

        private ProjectVM Project;

        public NodeMetaTraderFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AddOrRemoveNodeForTestCommand = new DelegateCommand<REPTreeNodeVM>(AddOrRemoveNodeForTest);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(AddOrRemoveNodeForTestCommand);
        }

        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodeMetaTrader))
            {
                PopulateViewModel();

                if ((NodeOutput?.Count ?? 0) == 0)
                    NodeOutput = new ObservableCollection<REPTreeNodeVM>();

                // TESTING NODE
                //var nodes = new ObservableCollection<string>
                //    {
                //        "STOCHRSI_3_27_9_12_1_1 < 96.50599",
                //        "|   STOCHRSI_3_27_9_12_1_1 >= 3.20718",
                //        "|   |   STOCH_7_5_5_13_7 >= 48.84577",
                //        "|   |   |   AROON_12_1 >= 87.5",
                //        "|   |   |   |   STOCHF_37_14_1_1 < 92.31229",
                //        "|   |   |   |   |   RSI_3_22 >= 45.34515",
                //    };

                //NodeOutput.Add(new REPTreeNodeVM
                //{
                //    Node = nodes,
                //    TotalUP = 82,
                //    TotalDOWN = 400,
                //    RatioUP = 0.20,
                //    RatioDOWN = 4.88,
                //    RatioMax = 4.88,
                //    Label = "DOWN",
                //    Total = 482,
                //    Winner = true,
                //    TotalTradesIs = 228,
                //    WinningTradesIs = 125,
                //    LosingTradesIs = 103,
                //    TotalOpportunityIs = 7811,
                //    PercentSuccessIs = 54,
                //    ProgressivenessIs = 2,
                //    TotalTradesOs = 47,
                //    WinningTradesOs = 29,
                //    LosingTradesOs = 18,
                //    TotalOpportunityOs = 1559,
                //    PercentSuccessOs = 61,
                //    ProgressivenessOs = 3,
                //    WinningStrategy = true,
                //    HistoricalData = "Forex.EURUSD.H4.05-05-2003.22-12-2022",
                //    HasTestInMetaTrader = true
                //});
            }
        }

        private ICommand AddOrRemoveNodeForTestCommand { get; set; }
        public void AddOrRemoveNodeForTest(REPTreeNodeVM node)
        {
            try
            {
                NodeOutput ??= new ObservableCollection<REPTreeNodeVM>();

                foreach (var n in NodeOutput)
                {
                    if (n.Node == node.Node)
                    {
                        NodeOutput.Remove(node);
                        node.HasTestInMetaTrader = false;
                        return;
                    }
                }

                NodeOutput.Add(node);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async void PopulateViewModel()
        {
            try
            {
                Project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
                Configuration = Project?.ProjectConfigurations.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Bindable Model

        private ConfigurationBaseVM _configuration;
        public ConfigurationBaseVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private ObservableCollection<REPTreeNodeVM> _nodeOutput;
        public ObservableCollection<REPTreeNodeVM> NodeOutput
        {
            get => _nodeOutput;
            set => SetProperty(ref _nodeOutput, value);
        }
    }
}
