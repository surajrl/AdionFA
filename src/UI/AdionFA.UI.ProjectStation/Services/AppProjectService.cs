using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Model.Configuration;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Project.Services
{
    public class AppProjectService : IAppProjectService
    {
        private readonly IProjectServiceAgent _projectService;
        private readonly IMapper _mapper;

        public AppProjectService(
            IProjectServiceAgent projectService)
        {
            _projectService = projectService;

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        // Project Configuration

        public async Task<ProjectConfigurationModel> GetProjectConfigurationAsync(int projectId, bool includeGraph = false)
        {
            try
            {
                var config =
                    await _projectService.GetProjectConfigurationAsync(projectId, includeGraph);

                var pcsModel =
                    _mapper.Map<ProjectConfigurationVM, ProjectConfigurationModel>(config);

                var europa = config.ProjectScheduleConfigurations.FirstOrDefault(
                    gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                );
                pcsModel.ProjectScheduleEuropeId = europa.ProjectScheduleConfigurationId;
                pcsModel.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                pcsModel.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                var america = config.ProjectScheduleConfigurations.FirstOrDefault(
                    gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                );
                pcsModel.ProjectScheduleAmericaId = america.ProjectScheduleConfigurationId;
                pcsModel.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                pcsModel.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                var asia = config.ProjectScheduleConfigurations.FirstOrDefault(
                    gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                );
                pcsModel.ProjectScheduleAsiaId = asia.ProjectScheduleConfigurationId;
                pcsModel.FromTimeInSecondsAsia = DateTime.MinValue.AddSeconds(asia.FromTimeInSeconds ?? 0);
                pcsModel.ToTimeInSecondsAsia = DateTime.MinValue.AddSeconds(asia.ToTimeInSeconds ?? 0);

                return pcsModel;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateProjectConfigurationAsync(ProjectConfigurationModel projectConfiguration)
        {
            try
            {
                var europa = projectConfiguration.ProjectScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                    );
                europa.FromTimeInSeconds = projectConfiguration.FromTimeInSecondsEurope.Hour * 3600;
                europa.ToTimeInSeconds = projectConfiguration.ToTimeInSecondsEurope.Hour * 3600;

                var america = projectConfiguration.ProjectScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                    );
                america.FromTimeInSeconds = projectConfiguration.FromTimeInSecondsAmerica.Hour * 3600;
                america.ToTimeInSeconds = projectConfiguration.ToTimeInSecondsAmerica.Hour * 3600;

                var asia = projectConfiguration.ProjectScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                    );
                asia.FromTimeInSeconds = projectConfiguration.FromTimeInSecondsAsia.Hour * 3600;
                asia.ToTimeInSeconds = projectConfiguration.ToTimeInSecondsAsia.Hour * 3600;

                var configVm = _mapper.Map<ProjectConfigurationVM, ProjectConfigurationModel>(projectConfiguration);

                var response = await _projectService.UpdateProjectConfigurationAsync(configVm);

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}