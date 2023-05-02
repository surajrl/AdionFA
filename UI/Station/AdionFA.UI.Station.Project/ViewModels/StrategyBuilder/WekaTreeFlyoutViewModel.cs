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
using AdionFA.UI.Station.Infrastructure.Model.Weka;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class WekaTreeFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectServiceAgent _projectService;
        private ProjectVM Project;

        public WekaTreeFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            NodeTestInMetatraderCommand = new DelegateCommand<REPTreeNodeVM>(AddNode);
            applicationCommands.NodeTestInMetatraderCommand.RegisterCommand(NodeTestInMetatraderCommand);
        }

        private ICommand NodeTestInMetatraderCommand { get; set; }
        public void AddNode(REPTreeNodeVM node)
        {
            node.HasTestInMetaTrader = true;
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleWekaTree))
            {
                PopulateViewModel();

                var projection = ((ObservableCollection<REPTreeNodeVM>)flyoutModel.Model)
                    .Where(m => m.Winner)
                    .OrderByDescending(n => n.Winner)
                    .ThenByDescending(n => n.WinningStrategy)
                    .ToList();

                projection.ForEach(m =>
                {
                    m.Node = new ObservableCollection<string>(m.Node.OrderByDescending(n => n).ToList());
                });

                NodeOutput = new ObservableCollection<REPTreeNodeVM>(projection);
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

        private int _seed;
        public int Seed
        {
            get => _seed;
            set => SetProperty(ref _seed, value);
        }

        private ObservableCollection<REPTreeNodeVM> _nodeOutput;
        public ObservableCollection<REPTreeNodeVM> NodeOutput
        {
            get => _nodeOutput;
            set => SetProperty(ref _nodeOutput, value);
        }
    }
}
