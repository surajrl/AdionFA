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
        Task<IList<ProjectVM>> GetAllProjects();
        Task<ProjectVM> GetProjectAsync(int projectId, bool includeGraph = false);
        Task<ProjectConfigurationVM> GetProjectConfiguration(int projectId, bool includeGraph = false);
        Task<ResponseVM> UpdateProjectConfiguration(ProjectConfigurationVM configuration);
        Task<ResponseVM> RestoreProjectConfiguration(int projectId);
        Task<ResponseVM> CreateProject(ProjectVM project, int configurationId, int marketDataId);
        Task<ResponseVM> PinnedProject(int projectId, bool isPinned);
        Task<ResponseVM> UpdateProcessId(int projectId, long? processId);

        // Configuration

        Task<IList<ConfigurationVM>> GetAllConfiguration(bool includeGraph = false);
        Task<ConfigurationVM> GetConfiguration(int? ConfigurationId = null, bool includeGraph = false);
        Task<ConfigurationVM> GetConfiguration(int configurationId, bool includeGraph = false);
        Task<ResponseVM> UpdateConfiguration(ConfigurationVM configuration);
    }
}
