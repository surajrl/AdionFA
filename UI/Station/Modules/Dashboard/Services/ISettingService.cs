using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Module.Dashboard.Services
{
    public interface ISettingService
    {
        #region Configurations

        Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false);
        Task<ProjectGlobalConfigModel> GetGlobalConfiguration();
        Task<bool> UpdateGlobalConfiguration(ProjectGlobalConfigModel config);

        #endregion

        #region Market Data

        Task<IList<MarketDataVM>> GetAllMarketData(bool includeGraph = false);
        Task<UploadMarketDataModel> GetMarketData(int marketId = 0, int currencyPairId = 0, int currencyPeriodId = 0);
        Task<bool> CreateMarketData(UploadMarketDataModel vm);

        #endregion

        #region Project

        Task<bool> CreateProject(CreateProjectModel project);

        #endregion
    }
}
