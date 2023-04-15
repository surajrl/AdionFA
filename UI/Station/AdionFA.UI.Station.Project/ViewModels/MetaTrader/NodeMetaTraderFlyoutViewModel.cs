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
                Project = await _projectService.GetProject(ProcessArgs.ProjectId, true);
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
