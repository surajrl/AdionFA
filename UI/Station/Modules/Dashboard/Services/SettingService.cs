using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
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
        private readonly IMapper _mapper;

        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly IProjectServiceAgent _projectService;

        public SettingService(
            IMarketDataServiceAgent historicalDataService,
            IProjectServiceAgent projectService)
        {
            _marketDataService = historicalDataService;
            _projectService = projectService;

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingSettingProfile());
            }).CreateMapper();
        }

        // Global Configuration

        public async Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false)
        {
            try
            {
                var list = await _projectService.GetAllGlobalConfigurations(includeGraph);
                var vm = _mapper.Map<IList<ProjectGlobalConfigurationVM>, IList<ProjectGlobalConfigurationVM>>(list);
                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ProjectGlobalConfigurationModel> GetGlobalConfiguration()
        {
            try
            {
                var globalConfig = await _projectService.GetGlobalConfiguration(includeGraph: true);
                var result = _mapper.Map<ProjectGlobalConfigurationVM, ProjectGlobalConfigurationModel>(globalConfig);

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

        public async Task<bool> UpdateGlobalConfiguration(ProjectGlobalConfigurationModel config)
        {
            try
            {
                int factor = 3600;

                // Convert Hours to Milliseconds Europe

                var europa = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                    );
                europa.FromTimeInSeconds = config.FromTimeInSecondsEurope?.Hour * factor;
                europa.ToTimeInSeconds = config.ToTimeInSecondsEurope?.Hour * factor;

                // Convert Hours to Milliseconds America

                var america = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                    );
                america.FromTimeInSeconds = config.FromTimeInSecondsAmerica?.Hour * factor;
                america.ToTimeInSeconds = config.ToTimeInSecondsAmerica?.Hour * factor;

                // Convert Hours to Milliseconds Asia

                var asia = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                    );
                asia.FromTimeInSeconds = config.FromTimeInSecondsAsia?.Hour * factor;
                asia.ToTimeInSeconds = config.ToTimeInSecondsAsia?.Hour * factor;

                var configVm = _mapper.Map<ProjectGlobalConfigurationVM, ProjectGlobalConfigurationModel>(config);

                var response = await _projectService.UpdateGlobalConfiguration(configVm);
                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Historical Data

        public async Task<IList<HistoricalDataVM>> GetAllHistoricalData(bool includeGraph = false)
        {
            try
            {
                var list = await _marketDataService.GetAllHistoricalData(includeGraph);
                return _mapper.Map<IList<HistoricalDataVM>, IList<HistoricalDataVM>>(list);
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
                HistoricalDataVM vm = await _marketDataService.GetHistoricalData(marketId, symbolId, timeframeId);

                var settingVm = _mapper.Map<HistoricalDataVM, UploadHistoricalDataModel>(vm) ?? new UploadHistoricalDataModel();
                List<HistoricalDataCandleSettingVM> details = vm?.HistoricalDataCandles.Select(
                   d => _mapper.Map<HistoricalDataCandleVM, HistoricalDataCandleSettingVM>(d))
                            .OrderBy(h => h.StartDate).ThenBy(h => h.StartTime).ToList()
                            ?? Array.Empty<HistoricalDataCandleSettingVM>().ToList();

                settingVm.HistoricalDataCandles?.Clear();
                settingVm.HistoricalDataCandleSettings = details;

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
                var result = await _marketDataService.CreateHistoricalData(vm);
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
                var result = await _marketDataService.CreateHistoricalData(vm);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateHistoricalData(DownloadHistoricalDataModel vm)
        {
            try
            {
                var result = await _marketDataService.CreateHistoricalData(vm);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Project

        public async Task<bool> CreateProject(CreateProjectModel project)
        {
            try
            {
                var result = await _projectService.CreateProject(
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
    }
}