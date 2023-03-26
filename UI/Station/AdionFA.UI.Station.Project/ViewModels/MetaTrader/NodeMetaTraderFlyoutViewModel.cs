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

namespace AdionFA.UI.Station.Project.ViewModels.MetaTrader
{
    public class NodeMetaTraderFlyoutViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectServiceAgent _projectService;

        private ProjectVM Project;

        public NodeMetaTraderFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AddOrRemoveNodeForTestCommand = new DelegateCommand<REPTreeNodeModelVM>(AddOrRemoveNodeForTest);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(AddOrRemoveNodeForTestCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        #region Commands

        #region FlyoutCommand

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodeMetaTrader))
            {
                PopulateViewModel();

                if ((NodeOutput?.Count ?? 0) == 0)
                    NodeOutput = new ObservableCollection<REPTreeNodeModelVM>();
            }
        }

        #endregion FlyoutCommand

        #region AddOrRemoveNodeForTestCommand

        private ICommand AddOrRemoveNodeForTestCommand { get; set; }

        public void AddOrRemoveNodeForTest(REPTreeNodeModelVM node)
        {
            try
            {
                if (NodeOutput == null)
                    NodeOutput = new ObservableCollection<REPTreeNodeModelVM>();

                var n = NodeOutput.FirstOrDefault(_n => _n.NodeWithoutFormat == node.NodeWithoutFormat);
                if (n != null)
                {
                    NodeOutput.Remove(node);
                }
                else
                {
                    nodeOutput.Add(node);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion AddOrRemoveNodeForTestCommand

        #endregion Commands

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

        #region Bindable Model

        private ConfigurationBaseVM _configuration;

        public ConfigurationBaseVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private ObservableCollection<REPTreeNodeModelVM> nodeOutput;

        public ObservableCollection<REPTreeNodeModelVM> NodeOutput
        {
            get => nodeOutput;
            set => SetProperty(ref nodeOutput, value);
        }

        #endregion Bindable Model
    }
}