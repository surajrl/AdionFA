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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class ConfigurationFlyoutViewModel : ViewModelBase
    {
        #region AutoMapper
        public readonly IMapper Mapper;
        #endregion

        #region Services
        private readonly IAppProjectService AppProjectService;
        #endregion

        private ProjectVM Project;

        #region Ctor
        public ConfigurationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }
        #endregion

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

        private List<AutoAdjustConfigModel> autoAdjustConfigs;
        public List<AutoAdjustConfigModel> AutoAdjustConfigs
        {
            get => autoAdjustConfigs;
            set => SetProperty(ref autoAdjustConfigs, value);
        }
        #endregion
    }
}
