using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.Model.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Project.Services
{
    public interface IAppProjectService
    {
        // Project Configuration

        Task<ProjectConfigurationModel> GetProjectConfiguration(int projectId, bool includeGraph = false);

        Task<ResponseVM> UpdateProjectConfiguration(ProjectConfigurationModel config);
    }
}