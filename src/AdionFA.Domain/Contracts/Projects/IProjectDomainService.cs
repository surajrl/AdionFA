using AdionFA.Domain.Entities;
using System.Collections.Generic;

namespace AdionFA.Domain.Projects
{
    public interface IProjectDomainService
    {
        // Project

        IList<Project> GetAllProjects();
        Project GetProject(int projectId, bool includeGraph = false);
        int CreateProject(Project project, int globalConfigurationId, int? marketDataId = null);
        bool UpdateProject(Project project);

        // Project Configuration

        ProjectConfiguration GetProjectConfiguration(int projectId, bool includeGraph = false);
        int CreateProjectConfiguration(ProjectConfiguration projectConfiguration);
        bool UpdateProjectConfiguration(ProjectConfiguration projectConfiguration);
        ProjectConfiguration RestoreProjectConfiguration(int projectId);

        // Process

        bool UpdateProcessId(int entityId, int entityTypeId, long? processId);
    }
}
