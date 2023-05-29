using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Module.Dashboard.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Dashboard.Services
{
    public interface ISettingService
    {
        // Configuration

        Task<IList<ConfigurationVM>> GetAllConfiguration(bool includeGraph = false);

        Task<ConfigurationModel> GetConfiguration();

        Task<bool> UpdateConfiguration(ConfigurationModel config);

        // Historical Data

        Task<IList<HistoricalDataVM>> GetAllHistoricalData(bool includeGraph = false);

        Task<UploadHistoricalDataModel> GetHistoricalData(int marketId = 0, int symbolId = 0, int timeframeId = 0);

        Task<bool> CreateHistoricalData(UploadHistoricalDataModel vm);

        Task<bool> CreateHistoricalData(DownloadHistoricalDataModel vm);

        // Project

        Task<bool> CreateProject(CreateProjectModel project);
    }
}