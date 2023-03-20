﻿using Adion.FA.Core.Domain.Aggregates.Project;
using System.Collections.Generic;

namespace Adion.FA.Core.Domain.Contracts.Projects
{
    public interface IProjectDomainService
    {
        #region Project
        IList<Project> GetAllProjects();
        Project GetProject(int projectId, bool includeGraph = false);
        int CreateProject(Project project, int globalConfigurationId, int? marketDataId = null);
        bool UpdateProject(Project project);
        #endregion

        #region Project Configuration
        ProjectConfiguration GetProjectConfiguration(int projectId, bool includeGraph = false);
        int CreateProjectConfiguration(ProjectConfiguration projectConfiguration);
        bool UpdateProjectConfiguration(ProjectConfiguration projectConfiguration);
        ProjectConfiguration RestoreProjectConfiguration(int projectId);
        #endregion

        #region Process
        bool UpdateProcessId(int entityId, int entityTypeId, long? processId);
        #endregion
    }
}
