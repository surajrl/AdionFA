using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface IProjectServiceAgent
    {
        #region Project
        Task<IList<ProjectVM>> GetAllProjects();
        Task<ProjectVM> GetProject(int projectId, bool includeGraph = false);
        Task<ProjectConfigurationVM> GetProjectConfiguration(int projectId, bool includeGraph = false);
        Task<ResponseVM> UpdateProjectConfiguration(ProjectConfigurationVM configuration);
        Task<ResponseVM> RestoreProjectConfiguration(int projectId);
        Task<ResponseVM> CreateProject(ProjectVM project, int globalConfigurationId, int marketDataId);
        Task<ResponseVM> PinnedProject(int projectId, bool isPinned);
        Task<ResponseVM> UpdateProcessId(int projectId, long? processId);
        #endregion

        #region Configuration
        Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false);
        Task<ProjectGlobalConfigurationVM> GetGlobalConfiguration(int? globalConfigurationId = null, bool includeGraph = false);
        Task<ProjectGlobalConfigurationVM> GetGlobalConfiguration(int configurationId, bool includeGraph = false);
        Task<ResponseVM> UpdateGlobalConfiguration(ProjectGlobalConfigurationVM configuration);
        #endregion
    }
}
