using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;

namespace AdionFA.Application.Contracts
{
    public interface IProjectService
    {
        // Project

        IList<ProjectDTO> GetAllProject(bool includeGraph);
        ProjectDTO GetProject(int projectId, bool includeGraph);
        ResponseDTO CreateProject(ProjectDTO projectDTO);

        // Project Configuration

        TransferObject.Project.ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph);
        ResponseDTO UpdateProjectConfiguration(TransferObject.Project.ProjectConfigurationDTO updatedProjectConfiguration);
        ResponseDTO RestoreProjectConfiguration(int projectId);

    }
}
