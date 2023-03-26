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

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class ConfigurationFlyoutViewModel : ViewModelBase
    {
        #region AutoMapper

        public readonly IMapper Mapper;

        #endregion AutoMapper

        #region Services

        private readonly IProjectServiceAgent _projectService;

        #endregion Services

        private ProjectVM Project;

        #region Ctor

        public ConfigurationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        #endregion Ctor

        #region Commands

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleAutoConfiguration))
            {
                PopulateViewModel();
                AutoAdjustConfigs = flyoutModel.Model != null ? ((ObservableCollection<AutoAdjustConfigModel>)flyoutModel.Model).ToList() : new List<AutoAdjustConfigModel>();
            }
        }

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

        private List<AutoAdjustConfigModel> autoAdjustConfigs;

        public List<AutoAdjustConfigModel> AutoAdjustConfigs
        {
            get => autoAdjustConfigs;
            set => SetProperty(ref autoAdjustConfigs, value);
        }

        #endregion Bindable Model
    }
}