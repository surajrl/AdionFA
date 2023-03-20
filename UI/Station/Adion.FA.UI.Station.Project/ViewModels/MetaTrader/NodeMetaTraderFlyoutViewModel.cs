using Adion.FA.UI.Station.Project.AutoMapper;
using Adion.FA.UI.Station.Project.Model.StrategyBuilder;
using Adion.FA.UI.Station.Project.Services;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.Services;
using AutoMapper;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Project.ViewModels.MetaTrader
{
    public class NodeMetaTraderFlyoutViewModel : ViewModelBase
    {
        #region AutoMapper
        public readonly IMapper Mapper;
        #endregion

        #region Services
        private readonly IAppProjectService AppProjectService;
        #endregion

        private ProjectVM Project;

        #region Ctor
        public NodeMetaTraderFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AddOrRemoveNodeForTestCommand = new DelegateCommand<REPTreeNodeModelVM>(AddOrRemoveNodeForTest);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(AddOrRemoveNodeForTestCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }
        #endregion

        #region Commands

        #region FlyoutCommand
        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleNodeMetaTrader))
            {
                PopulateViewModel();

                if((NodeOutput?.Count ?? 0) == 0)
                    NodeOutput = new ObservableCollection<REPTreeNodeModelVM>();
            }
        }
        #endregion

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
        #endregion

        #endregion

        public async void PopulateViewModel()
        {
            try
            {
                Project = await AppProjectService.GetProject(ProcessArgs.ProjectId, true);
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
        #endregion
    }
}
