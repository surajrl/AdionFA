using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using AutoMapper;
using AdionFA.UI.Station.Project.Services;
using Prism.Ioc;
using AdionFA.UI.Station.Project.AutoMapper;
using System;
using System.Diagnostics;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class WekaTreeFlyoutViewModel : ViewModelBase
    {
        public readonly IMapper Mapper;

        private readonly IProjectServiceAgent _projectService;

        private ProjectVM Project;

        public WekaTreeFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            AddOrRemoveNodeForTestCommand = new DelegateCommand<REPTreeNodeModelVM>(AddOrRemoveNodeForTest);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(AddOrRemoveNodeForTestCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        #region Commands

        #region FlyoutCommand

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleWekaTree))
            {
                PopulateViewModel();

                var projection = ((ObservableCollection<REPTreeNodeModelVM>)flyoutModel.Model)
                    .Where(m => m.Winner).OrderByDescending(n => n.Winner).ThenByDescending(n => n.WinningStrategy).ToList();
                projection.ForEach(m =>
                {
                    m.Node = new ObservableCollection<string>(m.Node.OrderByDescending(n => n).ToList());
                });
                NodeOutput = new ObservableCollection<REPTreeNodeModelVM>(projection);
            }
        }

        #endregion FlyoutCommand

        #region AddOrRemoveNodeForTestCommand

        private ICommand AddOrRemoveNodeForTestCommand { get; set; }

        public void AddOrRemoveNodeForTest(REPTreeNodeModelVM node)
        {
            try
            {
                foreach (var n in NodeOutput)
                {
                    if (n.NodeWithoutFormat == node.NodeWithoutFormat)
                    {
                        n.HasTestInMetatrader = !n.HasTestInMetatrader;
                    }
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

        private int seed;

        public int Seed
        {
            get => seed;
            set => SetProperty(ref seed, value);
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