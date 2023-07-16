using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Module.Dashboard.Model;
using System.Collections.Generic;

namespace AdionFA.UI.Module.Dashboard.Services
{
    public interface ISettingService
    {
        // Configuration

        ConfigurationModel GetConfiguration();

        bool UpdateConfiguration(ConfigurationModel config);

        // Historical Data

        IList<HistoricalDataVM> GetAllHistoricalData(bool includeGraph = false);

        bool CreateHistoricalData(UploadHistoricalDataModel vm);

        bool CreateHistoricalData(DownloadHistoricalDataModel vm);

        // Project

        bool CreateProject(CreateProjectModel project);
    }
}