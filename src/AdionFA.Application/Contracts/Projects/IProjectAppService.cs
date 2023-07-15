using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;

namespace AdionFA.Application.Contracts.Projects
{
    public interface IProjectAppService : IAppContractBase
    {
        // Project

        IList<ProjectDTO> GetAllProject();
        ProjectDTO GetProject(int projectId, bool includeGraph = false);
        ResponseDTO CreateProject(ProjectDTO projectDTO, int? configurationId = null, int? historicalDataId = null);
        ResponseDTO UpdateProject(ProjectDTO projectDTO);

        // Project Configuration

        ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false);
        ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfigurationDTO);
        ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfigurationDTO);
        ResponseDTO RestoreProjectConfiguration(int projectId);

    }
}
