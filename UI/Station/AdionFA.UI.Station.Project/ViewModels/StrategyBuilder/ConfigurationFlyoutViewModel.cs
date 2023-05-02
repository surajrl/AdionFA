using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Project.Services;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Services;
using AutoMapper;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using System.Runtime.CompilerServices;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class ConfigurationFlyoutViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectServiceAgent _projectService;
        private ProjectVM _project;

        public ConfigurationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        private ICommand FlyoutCommand { get; set; }
        private void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAutoConfiguration))
            {
                PopulateViewModel();
                AutoAdjustConfigs = flyoutModel.Model != null ? ((ObservableCollection<AutoAdjustConfigModel>)flyoutModel.Model).ToList() : new List<AutoAdjustConfigModel>();
            }
        }

        private async void PopulateViewModel()
        {
            try
            {
                _project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
                Configuration = _project?.ProjectConfigurations.FirstOrDefault();
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

        private List<AutoAdjustConfigModel> _autoAdjustConfigs;
        public List<AutoAdjustConfigModel> AutoAdjustConfigs
        {
            get => _autoAdjustConfigs;
            set => SetProperty(ref _autoAdjustConfigs, value);
        }
    }
}
