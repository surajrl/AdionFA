using AdionFA.Core.API.Contracts.Projects;
using AdionFA.Core.Application.Contracts.Projects;

using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;

using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;

using System.Collections.Generic;

namespace AdionFA.Core.API.Projects
{
    public class ProjectAPI : IProjectAPI
    {
        // Project

        public IList<ProjectDTO> GetAllProject()
        {
            using var service = IoC.Get<IProjectAppService>();
            return service.GetAllProject();
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph = false)
        {
            using IProjectAppService service = IoC.Get<IProjectAppService>();
            return service.GetProject(projectId, includeGraph);
        }

        public ResponseDTO CreateProject(ProjectDTO project, int? globalConfigurationId = null, int? marketDataId = null)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
                return service.CreateProject(project, globalConfigurationId, marketDataId);
        }

        public ResponseDTO UpdateProject(ProjectDTO project)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateProject(project);
            }
        }

        // Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            using var service = IoC.Get<IProjectAppService>();
            return service.GetProjectConfiguration(projectId, includeGraph);
        }

        public ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfiguration)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateProjectConfiguration(projectConfiguration);
            }
        }

        public ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfiguration)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateProjectConfiguration(projectConfiguration);
            }
        }

        public ResponseDTO RestoreProjectConfiguration(int projectId)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.RestoreProjectConfiguration(projectId);
            }
        }

        // Shell Module

        public ResponseDTO PinnedProject(int projectId, bool isPinned)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.PinnedProject(projectId, isPinned);
            }
        }

        public ResponseDTO UpdateProcessId(int projectId, long? processId)
        {
            using (var service = IoC.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateProcessId(projectId, processId);
            }
        }
    }
}
