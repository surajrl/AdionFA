using AdionFA.Core.Application.Contracts.Projects;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Module.Dashboard.AutoMapper;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Dashboard.Services
{
    public class SettingService : ISettingService
    {
        #region Services

        public readonly IHistoricalDataServiceAgent HistoricalDataService;
        public readonly IProjectServiceAgent ProjectService;

        #endregion Services

        #region AutoMapper

        public readonly IMapper Mapper;

        #endregion AutoMapper

        #region Constructor

        public SettingService(
            IHistoricalDataServiceAgent historicalDataService,
            IProjectServiceAgent projectService)
        {
            HistoricalDataService = historicalDataService;
            ProjectService = projectService;

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingSettingProfile());
            }).CreateMapper();
        }

        #endregion Constructor

        #region Configurations

        public async Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false)
        {
            try
            {
                var list = await ProjectService.GetAllGlobalConfigurations(includeGraph);
                var vm = Mapper.Map<IList<ProjectGlobalConfigurationVM>, IList<ProjectGlobalConfigurationVM>>(list);
                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ProjectGlobalConfigModel> GetGlobalConfiguration()
        {
            try
            {
                var globalConfig = await ProjectService.GetGlobalConfiguration(includeGraph: true);
                var result = Mapper.Map<ProjectGlobalConfigurationVM, ProjectGlobalConfigModel>(globalConfig);

                var europa = globalConfig.ProjectGlobalScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe);
                result.GlobalScheduleEuropeId = europa.ProjectGlobalScheduleConfigurationId;
                result.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                var america = globalConfig.ProjectGlobalScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.America);
                result.GlobalScheduleAmericaId = america.ProjectGlobalScheduleConfigurationId;
                result.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                var asia = globalConfig.ProjectGlobalScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia);
                result.GlobalScheduleAsiaId = asia.ProjectGlobalScheduleConfigurationId;
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

        public async Task<bool> UpdateGlobalConfiguration(ProjectGlobalConfigModel config)
        {
            try
            {
                int factor = 3600;

                #region Convert Hours to Milliseconds Europe

                var europa = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                    );
                europa.FromTimeInSeconds = config.FromTimeInSecondsEurope?.Hour * factor;
                europa.ToTimeInSeconds = config.ToTimeInSecondsEurope?.Hour * factor;

                #endregion Convert Hours to Milliseconds Europe

                #region Convert Hours to Milliseconds America

                var america = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                    );
                america.FromTimeInSeconds = config.FromTimeInSecondsAmerica?.Hour * factor;
                america.ToTimeInSeconds = config.ToTimeInSecondsAmerica?.Hour * factor;

                #endregion Convert Hours to Milliseconds America

                #region Convert Hours to Milliseconds Asia

                var asia = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                    );
                asia.FromTimeInSeconds = config.FromTimeInSecondsAsia?.Hour * factor;
                asia.ToTimeInSeconds = config.ToTimeInSecondsAsia?.Hour * factor;

                #endregion Convert Hours to Milliseconds Asia

                var configVm = Mapper.Map<ProjectGlobalConfigurationVM, ProjectGlobalConfigModel>(config);

                var response = await ProjectService.UpdateGlobalConfiguration(configVm);
                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion Configurations

        #region Market Data

        public async Task<IList<HistoricalDataVM>> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                var list = await HistoricalDataService.GetAllHistoricalData(includeGraph);
                return Mapper.Map<IList<HistoricalDataVM>, IList<HistoricalDataVM>>(list);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<UploadHistoricalDataModel> GetHistoricalData(int marketId = 0, int symbolId = 0, int timeframeId = 0)
        {
            try
            {
                HistoricalDataVM vm = await HistoricalDataService.GetHistoricalData(marketId, symbolId, timeframeId);

                var settingVm = Mapper.Map<HistoricalDataVM, UploadHistoricalDataModel>(vm) ?? new UploadHistoricalDataModel();
                List<HistoricalDataDetailSettingVM> details = vm?.HistoricalDataDetails.Select(
                   d => Mapper.Map<HistoricalDataDetailVM, HistoricalDataDetailSettingVM>(d))
                            .OrderBy(h => h.StartDate).ThenBy(h => h.StartTime).ToList()
                            ?? Array.Empty<HistoricalDataDetailSettingVM>().ToList();

                settingVm.HistoricalDataDetails?.Clear();
                settingVm.HistoricalDataDetailSettings = details;

                return settingVm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateHistoricalData(UploadHistoricalDataModel vm)
        {
            try
            {
                var result = await HistoricalDataService.CreateHistoricalData(vm);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateHistoricalData(DownloadHistoricalDataModel vm)
        {
            try
            {
                var result = await HistoricalDataService.CreateHistoricalData(vm);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion Market Data

        #region Project

        public async Task<bool> CreateProject(CreateProjectModel project)
        {
            try
            {
                var result = await ProjectService.CreateProject(
                    project,
                    project.ConfigurationId ?? 0,
                    project.HistoricalDataId ?? 0);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion Project
    }
}