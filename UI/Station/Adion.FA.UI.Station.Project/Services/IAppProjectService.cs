using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Project.Model.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Project.Services
{
    public interface IAppProjectService
    {
        #region Configurations
        Task<ProjectConfigurationSettingModel> GetProjectConfiguration(int projectId, bool includeGraph = false);
        Task<ResponseVM> UpdateProjectConfiguration(ProjectConfigurationSettingModel config);
        Task<ResponseVM> RestoreProjectConfiguration(int projectId);
        #endregion

        #region Projects
        Task<ProjectVM> GetProject(int projectId, bool includeGraph = false);
        #endregion

        #region Market Data
        Task<MarketDataVM> GetMarketData(int marketDataId, bool includeGraph = false);

        Task<IList<MarketDataVM>> GetAllMarketData();
        #endregion
    }
}
