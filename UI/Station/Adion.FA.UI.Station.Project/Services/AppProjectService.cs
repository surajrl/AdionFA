using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Project.AutoMapper;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Adion.FA.UI.Station.Project.Model.Configuration;
using System.Collections.Generic;
using Adion.FA.UI.Station.Infrastructure.Model.Base;

namespace Adion.FA.UI.Station.Project.Services
{
    public class AppProjectService : IAppProjectService
    {
        #region Services
        public readonly IMarketDataServiceAgent MarketDataService;
        public readonly IProjectServiceAgent ProjectService;
        #endregion

        #region AutoMapper
        public readonly IMapper Mapper;
        #endregion

        #region Ctor
        public AppProjectService(
            IMarketDataServiceAgent marketDataService,
            IProjectServiceAgent projectService)
        {
            MarketDataService = marketDataService;
            ProjectService = projectService;

            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }
        #endregion

        #region Configurations
        public async Task<ProjectConfigurationSettingModel> GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectConfigurationVM config = 
                    await ProjectService.GetProjectConfiguration(projectId, includeGraph);
                
                ProjectConfigurationSettingModel pcsModel = 
                    Mapper.Map<ProjectConfigurationVM, ProjectConfigurationSettingModel>(config);

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

        public async Task<ResponseVM> UpdateProjectConfiguration(ProjectConfigurationSettingModel config)
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

                var configVm = Mapper.Map<ProjectConfigurationVM, ProjectConfigurationSettingModel>(config);
                
                var response = await ProjectService.UpdateProjectConfiguration(configVm);

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> RestoreProjectConfiguration(int projectId)
        {
            try
            {
                var response = await ProjectService.RestoreProjectConfiguration(projectId);
                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Project
        public async Task<ProjectVM> GetProject(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectVM result = await ProjectService.GetProject(projectId, includeGraph);
                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Market Data
        public async Task<MarketDataVM> GetMarketData(int marketDataId, bool includeGraph = false)
        {
            try
            {
                MarketDataVM result = await MarketDataService.GetMarketData(marketDataId, includeGraph);
                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<IList<MarketDataVM>> GetAllMarketData()
        {
            try
            {
                IList<MarketDataVM> result = await MarketDataService.GetAllMarketData();
                return result;
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
