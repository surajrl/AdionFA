using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.Application.Contracts
{
    public interface IProjectService
    {
        // Project

        IList<ProjectDTO> GetAllProject(bool includeGraph);
        ProjectDTO GetProject(int projectId, bool includeGraph);
        Task<ResponseDTO> CreateProjectAsync(ProjectDTO projectDTO);

        // Project Configuration

        ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph);
        Task<ResponseDTO> UpdateProjectConfigurationAsync(ProjectConfigurationDTO updatedProjectConfiguration);
        Task<ResponseDTO> RestoreProjectConfigurationAsync(int projectId);

    }
}
