using AdionFA.Core.Application.Contract.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;

namespace AdionFA.Core.Application.Contracts.Projects
{
    public interface IProjectAppService : IAppContractBase
    {
        #region Project

        IList<ProjectDTO> GetAllProjects();
        ProjectDTO GetProject(int projectId, bool includeGraph = false);
        ResponseDTO CreateProject(ProjectDTO project, int? globalConfigurationId = null, int? marketDataId = null);
        ResponseDTO UpdateProject(ProjectDTO project);

        #endregion

        #region Project Configuration

        ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false);
        ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfiguration);
        ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfiguration);
        ResponseDTO RestoreProjectConfiguration(int projectId);

        #endregion

        #region Shell module

        ResponseDTO PinnedProject(int projectId, bool isPinned);
        ResponseDTO UpdateProcessId(int projectId, long? processId);
        
        #endregion
    }
}
