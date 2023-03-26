using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdionFA.UI.Station.Project.Model.Configuration;
using System.Collections.Generic;
using AdionFA.UI.Station.Infrastructure.Model.Base;

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

        public async Task<ProjectSettingsModel> GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectConfigurationVM config =
                    await _projectService.GetProjectConfiguration(projectId, includeGraph);

                ProjectSettingsModel pcsModel =
                    _mapper.Map<ProjectConfigurationVM, ProjectSettingsModel>(config);

                ProjectScheduleConfigurationVM europa = config.ProjectScheduleConfigurations.FirstOrDefault(
                    gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                );
                pcsModel.ProjectScheduleEuropeId = europa.ProjectScheduleConfigurationId;
                pcsModel.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                pcsModel.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                ProjectScheduleConfigurationVM america = config.ProjectScheduleConfigurations.FirstOrDefault(
                    gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                );
                pcsModel.ProjectScheduleAmericaId = america.ProjectScheduleConfigurationId;
                pcsModel.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                pcsModel.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                ProjectScheduleConfigurationVM asia = config.ProjectScheduleConfigurations.FirstOrDefault(
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

        public async Task<ResponseVM> UpdateProjectConfiguration(ProjectSettingsModel config)
        {
            try
            {
                var europa = config.ProjectScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                    );
                europa.FromTimeInSeconds = config.FromTimeInSecondsEurope.Hour * 3600;
                europa.ToTimeInSeconds = config.ToTimeInSecondsEurope.Hour * 3600;

                var america = config.ProjectScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                    );
                america.FromTimeInSeconds = config.FromTimeInSecondsAmerica.Hour * 3600;
                america.ToTimeInSeconds = config.ToTimeInSecondsAmerica.Hour * 3600;

                var asia = config.ProjectScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                    );
                asia.FromTimeInSeconds = config.FromTimeInSecondsAsia.Hour * 3600;
                asia.ToTimeInSeconds = config.ToTimeInSecondsAsia.Hour * 3600;

                var configVm = _mapper.Map<ProjectConfigurationVM, ProjectSettingsModel>(config);

                var response = await _projectService.UpdateProjectConfiguration(configVm);

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