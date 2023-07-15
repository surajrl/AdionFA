using AdionFA.API.Contracts;
using AdionFA.Application.Contracts.Projects;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Persistance.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using Ninject;
using System.Collections.Generic;

namespace AdionFA.API
{
    public class ProjectAPI : IProjectAPI
    {
        // Project

        public IList<ProjectDTO> GetAllProject()
        {
            using var service = IoC.Kernel.Get<IProjectAppService>();
            return service.GetAllProject();
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IProjectAppService>();
            return service.GetProject(projectId, includeGraph);
        }

        public ResponseDTO CreateProject(ProjectDTO projectDTO, int? configurationId = null, int? historicalDataId = null)
        {
            using (var service = IoC.Kernel.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateProject(projectDTO, configurationId, historicalDataId);
            }
        }

        public ResponseDTO UpdateProject(ProjectDTO projectDTO)
        {
            using (var service = IoC.Kernel.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateProject(projectDTO);
            }
        }

        // Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IProjectAppService>();
            return service.GetProjectConfiguration(projectId, includeGraph);
        }

        public ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfigurationDTO)
        {
            using (var service = IoC.Kernel.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateProjectConfiguration(projectConfigurationDTO);
            }
        }

        public ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfiguration)
        {
            using (var service = IoC.Kernel.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateProjectConfiguration(projectConfiguration);
            }
        }

        public ResponseDTO RestoreProjectConfiguration(int projectId)
        {
            using (var service = IoC.Kernel.Get<IProjectAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.RestoreProjectConfiguration(projectId);
            }
        }
    }
}
