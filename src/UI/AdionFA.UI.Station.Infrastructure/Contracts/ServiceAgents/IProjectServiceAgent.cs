using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface IProjectServiceAgent
    {
        // Project

        Task<IList<ProjectVM>> GetAllProjectAsync();
        Task<ProjectVM> GetProjectAsync(int projectId, bool includeGraph = false);
        Task<ResponseVM> CreateProjectAsync(ProjectVM project, int configurationId, int marketDataId);

        // Project Configuration

        Task<ProjectConfigurationVM> GetProjectConfigurationAsync(int projectId, bool includeGraph = false);
        Task<ResponseVM> UpdateProjectConfigurationAsync(ProjectConfigurationVM configuration);
        Task<ResponseVM> RestoreProjectConfigurationAsync(int projectId);

        // Configuration

        Task<IList<ConfigurationVM>> GetAllConfigurationAsync(bool includeGraph = false);
        Task<ConfigurationVM> GetConfigurationAsync(int? ConfigurationId = null, bool includeGraph = false);
        Task<ConfigurationVM> GetConfigurationAsync(int configurationId, bool includeGraph = false);
        Task<ResponseVM> UpdateConfigurationAsync(ConfigurationVM configuration);
    }
}
