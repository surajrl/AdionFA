using Adion.FA.Core.Application.Contracts.Projects;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Module.Dashboard.AutoMapper;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Module.Dashboard.Services
{
    public class SettingService : ISettingService
    {
        #region Services

        public readonly IMarketDataServiceAgent MarketDataService;
        public readonly IProjectServiceAgent ProjectService;

        #endregion

        #region AutoMapper

        public readonly IMapper Mapper;

        #endregion

        #region Ctor

        public SettingService(
            IMarketDataServiceAgent marketDataService,
            IProjectServiceAgent projectService)
        {
            MarketDataService = marketDataService;
            ProjectService = projectService;

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingSettingProfile());
            }).CreateMapper();
        }

        #endregion

        #region Configurations

        public async Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false)
        {
            try
            {
                var list = await ProjectService.GetAllGlobalConfigurations(includeGraph);
                var vm = Mapper.Map<IList<ProjectGlobalConfigurationVM>,IList<ProjectGlobalConfigurationVM>>(list);
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

                var config = await ProjectService.GetGlobalConfiguration(includeGraph: true);
                var result = Mapper.Map<ProjectGlobalConfigurationVM, ProjectGlobalConfigModel>(config);

                var europa = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.Europe);
                result.GlobalScheduleEuropeId = europa.ProjectGlobalScheduleConfigurationId;
                result.FromTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsEurope = DateTime.MinValue.AddSeconds(europa.ToTimeInSeconds ?? 0);

                var america = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.America);
                result.GlobalScheduleAmericaId = america.ProjectGlobalScheduleConfigurationId;
                result.FromTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.FromTimeInSeconds ?? 0);
                result.ToTimeInSecondsAmerica = DateTime.MinValue.AddSeconds(america.ToTimeInSeconds ?? 0);

                var asia = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia);
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

                #endregion

                #region Convert Hours to Milliseconds America 

                var america = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.America
                    );
                america.FromTimeInSeconds = config.FromTimeInSecondsAmerica?.Hour * factor;
                america.ToTimeInSeconds = config.ToTimeInSecondsAmerica?.Hour * factor;

                #endregion

                #region Convert Hours to Milliseconds Asia

                var asia = config.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        gc => gc.MarketRegionId == (int)MarketRegionEnum.Asia
                    );
                asia.FromTimeInSeconds = config.FromTimeInSecondsAsia?.Hour * factor;
                asia.ToTimeInSeconds = config.ToTimeInSecondsAsia?.Hour * factor;

                #endregion

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

        #endregion

        #region Market Data

        public async Task<IList<MarketDataVM>> GetAllMarketData(bool includeGraph = false)
        {
            try
            {
                var list = await MarketDataService.GetAllMarketData(includeGraph);
                return Mapper.Map<IList<MarketDataVM>, IList<MarketDataVM>>(list);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<UploadMarketDataModel> GetMarketData(int marketId = 0, int currencyPairId = 0, int currencyPeriodId = 0)
        {
            try
            {
                MarketDataVM vm = await MarketDataService.GetMarketData(marketId, currencyPairId, currencyPeriodId);

                var settingVm = Mapper.Map<MarketDataVM, UploadMarketDataModel>(vm) ?? new UploadMarketDataModel();
                List<MarketDataDetailSettingVM> details = vm?.MarketDataDetails.Select(
                   d => Mapper.Map<MarketDataDetailVM, MarketDataDetailSettingVM>(d))
                            .OrderBy(h => h.StartDate).ThenBy(h => h.StartTime).ToList() 
                            ?? Array.Empty<MarketDataDetailSettingVM>().ToList();

                settingVm.MarketDataDetails?.Clear();
                settingVm.MarketDataDetailSettings = details;

                return settingVm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateMarketData(UploadMarketDataModel vm)
        {
            try
            {
                var result = await MarketDataService.CreateMarketData(vm);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion

        #region Project

        public async Task<bool> CreateProject(CreateProjectModel project)
        {
            try
            {
                var result = await ProjectService.CreateProject(
                    project, project.ConfigurationId ?? 0, project.MarketDataId ?? 0);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
