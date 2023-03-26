using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Module.Dashboard.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Dashboard.Services
{
    public interface ISettingService
    {
        #region Configurations

        Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false);

        Task<ProjectGlobalConfigModel> GetGlobalConfiguration();

        Task<bool> UpdateGlobalConfiguration(ProjectGlobalConfigModel config);

        #endregion Configurations

        #region Historical Data

        Task<IList<HistoricalDataVM>> GetAllHistoricalData(bool includeGraph = false);

        Task<UploadHistoricalDataModel> GetHistoricalData(int marketId = 0, int symbolId = 0, int timeframeId = 0);

        Task<bool> CreateHistoricalData(UploadHistoricalDataModel vm);

        Task<bool> CreateHistoricalData(DownloadHistoricalDataModel vm);

        #endregion Historical Data

        #region Project

        Task<bool> CreateProject(CreateProjectModel project);

        #endregion Project
    }
}