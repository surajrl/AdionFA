using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.ProjectStation.AutoMapper;
using AdionFA.UI.ProjectStation.Model.Configuration;
using AutoMapper;
using Ninject;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdionFA.UI.ProjectStation.Services
{
    public class AppProjectService : IAppProjectService
    {
        private readonly IProjectAppService _projectService;
        private readonly IMapper _mapper;

        public AppProjectService()
        {
            _projectService = IoC.Kernel.Get<IProjectAppService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
                mc.AddProfile(new AutoMappingProjectStationProfile());
            }).CreateMapper();
        }

        // Project Configuration

        public ProjectConfigurationModel GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                var projectConfigDTO = _projectService.GetProjectConfiguration(projectId, includeGraph);

                var projectConfigModel = _mapper.Map<ProjectConfigurationDTO, ProjectConfigurationModel>(projectConfigDTO);

                var europa = projectConfigDTO.ProjectScheduleConfigurations.FirstOrDefault(projectScheduleConfig => projectScheduleConfig.MarketRegionId == (int)MarketRegionEnum.Europe);
                projectConfigModel.ProjectScheduleEuropeId = europa.ProjectScheduleConfigurationId;
                projectConfigModel.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                projectConfigModel.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                var america = projectConfigDTO.ProjectScheduleConfigurations.FirstOrDefault(projectScheduleConfig => projectScheduleConfig.MarketRegionId == (int)MarketRegionEnum.America);
                projectConfigModel.ProjectScheduleAmericaId = america.ProjectScheduleConfigurationId;
                projectConfigModel.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                projectConfigModel.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                var asia = projectConfigDTO.ProjectScheduleConfigurations.FirstOrDefault(projectScheduleConfig => projectScheduleConfig.MarketRegionId == (int)MarketRegionEnum.Asia);
                projectConfigModel.ProjectScheduleAsiaId = asia.ProjectScheduleConfigurationId;
                projectConfigModel.FromTimeInSecondsAsia = DateTime.MinValue.AddSeconds(asia.FromTimeInSeconds ?? 0);
                projectConfigModel.ToTimeInSecondsAsia = DateTime.MinValue.AddSeconds(asia.ToTimeInSeconds ?? 0);

                return projectConfigModel;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseVM UpdateProjectConfiguration(ProjectConfigurationModel projectConfiguration)
        {
            try
            {
                var europa = projectConfiguration.ProjectScheduleConfigurations.FirstOrDefault(projectScheduleConfig => projectScheduleConfig.MarketRegionId == (int)MarketRegionEnum.Europe);
                europa.FromTimeInSeconds = projectConfiguration.FromTimeInSecondsEurope.Hour * 3600;
                europa.ToTimeInSeconds = projectConfiguration.ToTimeInSecondsEurope.Hour * 3600;

                var america = projectConfiguration.ProjectScheduleConfigurations.FirstOrDefault(projectScheduleConfig => projectScheduleConfig.MarketRegionId == (int)MarketRegionEnum.America);
                america.FromTimeInSeconds = projectConfiguration.FromTimeInSecondsAmerica.Hour * 3600;
                america.ToTimeInSeconds = projectConfiguration.ToTimeInSecondsAmerica.Hour * 3600;

                var asia = projectConfiguration.ProjectScheduleConfigurations.FirstOrDefault(projectScheduleConfig => projectScheduleConfig.MarketRegionId == (int)MarketRegionEnum.Asia);
                asia.FromTimeInSeconds = projectConfiguration.FromTimeInSecondsAsia.Hour * 3600;
                asia.ToTimeInSeconds = projectConfiguration.ToTimeInSecondsAsia.Hour * 3600;

                var projectConfigDTO = _mapper.Map<ProjectConfigurationModel, ProjectConfigurationDTO>(projectConfiguration);

                var response = _projectService.UpdateProjectConfiguration(projectConfigDTO);

                return _mapper.Map<ResponseDTO, ResponseVM>(response);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}