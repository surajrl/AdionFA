using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
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

        public async Task<IList<ConfigurationVM>> GetAllConfiguration(bool includeGraph = false)
        {
            try
            {
                var list = await _projectService.GetAllConfiguration(includeGraph);
                var vm = _mapper.Map<IList<ConfigurationVM>, IList<ConfigurationVM>>(list);
                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ConfigurationModel> GetConfiguration()
        {
            try
            {
                var globalConfig = await _projectService.GetConfiguration(includeGraph: true);
                var result = _mapper.Map<ConfigurationVM, ConfigurationModel>(globalConfig);

                var europa = globalConfig.ScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe);
                result.ScheduleEuropeId = europa.ScheduleConfigurationId;
                result.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                var america = globalConfig.ScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.America);
                result.ScheduleAmericaId = america.ScheduleConfigurationId;
                result.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                var asia = globalConfig.ScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia);
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

        public async Task<bool> UpdateConfiguration(ConfigurationModel config)
        {
            try
            {
                int factor = 3600;

                // Convert Hours to Milliseconds Europe

                var europa = config.ScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe
                    );
                europa.FromTimeInSeconds = config.FromTimeInSecondsEurope?.Hour * factor;
                europa.ToTimeInSeconds = config.ToTimeInSecondsEurope?.Hour * factor;

                // Convert Hours to Milliseconds America

                var america = config.ScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                    );
                america.FromTimeInSeconds = config.FromTimeInSecondsAmerica?.Hour * factor;
                america.ToTimeInSeconds = config.ToTimeInSecondsAmerica?.Hour * factor;

                // Convert Hours to Milliseconds Asia

                var asia = config.ScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                    );
                asia.FromTimeInSeconds = config.FromTimeInSecondsAsia?.Hour * factor;
                asia.ToTimeInSeconds = config.ToTimeInSecondsAsia?.Hour * factor;

                var configVm = _mapper.Map<ConfigurationVM, ConfigurationModel>(config);

                var response = await _projectService.UpdateConfiguration(configVm);
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