using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;

namespace AdionFA.API.Contracts
{
    public interface IProjectAPI
    {
        // Project

        IList<ProjectDTO> GetAllProject();
        ProjectDTO GetProject(int projectId, bool includeGraph = false);
        ResponseDTO CreateProject(ProjectDTO project, int? globalConfigurationId = null, int? marketDataId = null);
        ResponseDTO UpdateProject(ProjectDTO project);

        // Project Configuration

        ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false);
        ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfiguration);
        ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfiguration);
        ResponseDTO RestoreProjectConfiguration(int projectId);
    }
}
