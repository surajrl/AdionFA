using AdionFA.Application.Contracts;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdionFA.Application.Services.Projects
{
    public class ProjectService : AppServiceBase, IProjectService
    {
        private readonly IGlobalConfigurationService _globalConfigurationService;
        private readonly IProjectDirectoryService _projectDirectoryService;

        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<ProjectConfiguration> _projectConfigurationRepository;

        public ProjectService(
            IGenericRepository<Project> projectRepository,
            IGenericRepository<ProjectConfiguration> projectConfigurationRepository)
        {
            _projectRepository = projectRepository;
            _projectConfigurationRepository = projectConfigurationRepository;

            _globalConfigurationService = IoC.Kernel.Get<IGlobalConfigurationService>();
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
        }

        // Project

        public IList<ProjectDTO> GetAllProject(bool includeGraph)
        {
            try
            {
                return includeGraph
                    ? Mapper.Map<IList<ProjectDTO>>(_projectRepository.GetAll(
                        project => project.HistoricalData))
                    : Mapper.Map<IList<ProjectDTO>>(_projectRepository.GetAll());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph)
        {
            try
            {
                return includeGraph
                    ? Mapper.Map<ProjectDTO>(_projectRepository.FirstOrDefault(
                        project => project.ProjectId == projectId,
                        project => project.HistoricalData))
                    : Mapper.Map<ProjectDTO>(_projectRepository.FirstOrDefault(
                        project => project.ProjectId == projectId));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDTO> CreateProjectAsync(ProjectDTO projectDTO)
        {
            try
            {
                var responseDTO = new ResponseDTO
                {
                    IsSuccess = false
                };

                // Create project configuration from the current global configuration
                var globalConfigurationDTO = _globalConfigurationService.GetGlobalConfiguration();

                projectDTO.ProjectConfiguration = new(globalConfigurationDTO);

                projectDTO.WorkspacePath = ProjectDirectoryManager.DefaultDirectory();

                var project = Mapper.Map<Project>(projectDTO);
                await _projectRepository.CreateAsync(project).ConfigureAwait(false);

                responseDTO.IsSuccess = project.ProjectId > 0;

                if (!responseDTO.IsSuccess)
                {
                    return responseDTO;
                }

                _projectDirectoryService.CreateDefaultProjectWorkspace(project.ProjectName);

                return responseDTO;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph)
        {
            try
            {
                return includeGraph
                    ? Mapper.Map<ProjectConfigurationDTO>(_projectConfigurationRepository.FirstOrDefault(
                        projectConfiguration => projectConfiguration.ProjectId == projectId,
                        projectConfiguration => projectConfiguration.Project,
                        projectConfiguration => projectConfiguration.ProjectScheduleConfigurations))
                    : Mapper.Map<ProjectConfigurationDTO>(_projectConfigurationRepository.FirstOrDefault(
                        projectConfiguration => projectConfiguration.ProjectId == projectId));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDTO> UpdateProjectConfigurationAsync(ProjectConfigurationDTO updatedProjectConfiguration)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false
                };

                await _projectConfigurationRepository.UpdateAsync(Mapper.Map<ProjectConfiguration>(updatedProjectConfiguration)).ConfigureAwait(false);

                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDTO> RestoreProjectConfigurationAsync(int projectId)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false
                };

                var projectConfiguration = _projectConfigurationRepository.FirstOrDefault(projectConfiguration => projectConfiguration.ProjectId == projectId);

                projectConfiguration.RestoreConfiguration();

                await _projectConfigurationRepository.UpdateAsync(projectConfiguration).ConfigureAwait(false);

                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
