using Adion.FA.Core.API.Contracts.Projects;
using Adion.FA.Core.Application.Contracts.Projects;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Project;
using System.Collections.Generic;

namespace Adion.FA.Core.API.Projects
{
    public class ProjectAPI : IProjectAPI
    {
        #region GlobalConfiguration

        public IList<ProjectGlobalConfigurationDTO> GetAllGlobalConfigurations(bool includeGraph = false)
        {
            using (var service = IoC.Get<IGlobalConfigurationAppService>())
                return service.GetAllGlobalConfigurations(includeGraph);
        }

        public ProjectGlobalConfigurationDTO GetGlobalConfiguration(int? globalConfigurationId = null, bool includeGraph = false)
        {
            using (var service = IoC.Get<IGlobalConfigurationAppService>())
                return service.GetGlobalConfiguration(globalConfigurationId, includeGraph);
        }

        public ResponseDTO UpdateGlobalConfiguration(ProjectGlobalConfigurationDTO configuration)
        {
            using (var service = IoC.Get<IGlobalConfigurationAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateGlobalConfiguration(configuration);
            }
        }

        #endregion

        #region Project

        public IList<ProjectDTO> GetAllProjects()
        {
            using (var service = IoC.Get<IProjectAppService>())
                return service.GetAllProjects();
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph = false)
        {
            using (var service = IoC.Get<IProjectAppService>())
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

        #endregion

        #region Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            using (var service = IoC.Get<IProjectAppService>())
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

        #endregion

        #region Shell module

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

        #endregion
    }
}
