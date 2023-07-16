using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.MarketData;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.Module.Dashboard.AutoMapper;
using AdionFA.UI.Module.Dashboard.Model;
using AutoMapper;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdionFA.UI.Module.Dashboard.Services
{
    public class SettingService : ISettingService
    {
        private readonly IMapper _mapper;

        private readonly IMarketDataAppService _marketDataService;
        private readonly IProjectAppService _projectService;
        private readonly IConfigurationAppService _configurationAppService;
        private readonly IProjectDirectoryService _projectDirectoryService;

        public SettingService()
        {
            _marketDataService = IoC.Kernel.Get<IMarketDataAppService>();
            _projectService = IoC.Kernel.Get<IProjectAppService>();
            _configurationAppService = IoC.Kernel.Get<IConfigurationAppService>();
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
                mc.AddProfile(new AutoMappingDashboardProfile());
            }).CreateMapper();
        }

        // Configuration

        public ConfigurationModel GetConfiguration()
        {
            try
            {
                var configurationDTO = _configurationAppService.GetConfiguration(includeGraph: true);
                var result = _mapper.Map<ConfigurationDTO, ConfigurationModel>(configurationDTO);

                var europa = configurationDTO.ScheduleConfigurations.FirstOrDefault(scheduleConfig => scheduleConfig.MarketRegionId == (int)MarketRegionEnum.Europe);
                result.ScheduleEuropeId = europa.ScheduleConfigurationId;
                result.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                var america = configurationDTO.ScheduleConfigurations.FirstOrDefault(scheduleConfig => scheduleConfig.MarketRegionId == (int)MarketRegionEnum.America);
                result.ScheduleAmericaId = america.ScheduleConfigurationId;
                result.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                var asia = configurationDTO.ScheduleConfigurations.FirstOrDefault(scheduleConfig => scheduleConfig.MarketRegionId == (int)MarketRegionEnum.Asia);
                result.ScheduleAsiaId = asia.ScheduleConfigurationId;
                result.FromTimeInSecondsAsia = DateTime.MinValue.AddSeconds(asia.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsAsia = DateTime.MinValue.AddSeconds(asia.ToTimeInSeconds ?? 0);

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool UpdateConfiguration(ConfigurationModel config)
        {
            try
            {
                const int factor = 3600;

                // Convert Hours to Milliseconds Europe

                var europa = config.ScheduleConfigurations.FirstOrDefault(scheduleConfig => scheduleConfig.MarketRegionId == (int)MarketRegionEnum.Europe);
                europa.FromTimeInSeconds = config.FromTimeInSecondsEurope?.Hour * factor;
                europa.ToTimeInSeconds = config.ToTimeInSecondsEurope?.Hour * factor;

                // Convert Hours to Milliseconds America

                var america = config.ScheduleConfigurations.FirstOrDefault(scheduleConfig => scheduleConfig.MarketRegionId == (int)MarketRegionEnum.America);
                america.FromTimeInSeconds = config.FromTimeInSecondsAmerica?.Hour * factor;
                america.ToTimeInSeconds = config.ToTimeInSecondsAmerica?.Hour * factor;

                // Convert Hours to Milliseconds Asia

                var asia = config.ScheduleConfigurations.FirstOrDefault(scheduleConfig => scheduleConfig.MarketRegionId == (int)MarketRegionEnum.Asia);
                asia.FromTimeInSeconds = config.FromTimeInSecondsAsia?.Hour * factor;
                asia.ToTimeInSeconds = config.ToTimeInSecondsAsia?.Hour * factor;

                var configurationDTO = _mapper.Map<ConfigurationModel, ConfigurationDTO>(config);

                var response = _configurationAppService.UpdateConfiguration(configurationDTO);

                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Historical Data

        public IList<HistoricalDataVM> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                return _mapper.Map<IList<HistoricalDataDTO>, IList<HistoricalDataVM>>(_marketDataService.GetAllHistoricalData(includeGraph));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool CreateHistoricalData(UploadHistoricalDataModel vm)
        {
            try
            {
                var result = _marketDataService.CreateHistoricalData(_mapper.Map<UploadHistoricalDataModel, HistoricalDataDTO>(vm));
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool CreateHistoricalData(DownloadHistoricalDataModel vm)
        {
            try
            {
                var result = _marketDataService.CreateHistoricalData(_mapper.Map<DownloadHistoricalDataModel, HistoricalDataDTO>(vm));
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Project

        public bool CreateProject(CreateProjectModel project)
        {
            try
            {
                var projectDTO = _mapper.Map<ProjectVM, ProjectDTO>(project);
                var response = _projectService.CreateProject(projectDTO, project.ConfigurationId, project.HistoricalDataId);

                if (response.IsSuccess)
                {
                    _projectDirectoryService.CreateDefaultProjectWorkspace(project.ProjectName);

                    var projectId = int.Parse(response.EntityId);
                    var projectConfig = _projectService.GetProjectConfiguration(projectId);
                    projectConfig.WorkspacePath = ProjectDirectoryManager.DefaultDirectory();
                    _projectService.UpdateProjectConfiguration(projectConfig);
                }

                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}