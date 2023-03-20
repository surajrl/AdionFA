using Adion.FA.Infrastructure.Common.Directories.Services;
using Adion.FA.TransferObject.Project;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.AutoMapper;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Core.API.Contracts.Projects;
using Adion.FA.TransferObject.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Base;

namespace Adion.FA.UI.Station.Infrastructure.Services.AppServices
{
    public class ProjectServiceAgent : IProjectServiceAgent
    {
        #region Automapper
        private readonly IMapper Mapper;
        #endregion

        #region Ctor
        public ProjectServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }
        #endregion

        #region Project
        public async Task<IList<ProjectVM>> GetAllProjects()
        {
            try
            {
                IList<ProjectVM> projects = Array.Empty<ProjectVM>().ToList();

                await Task.Run(() =>
                {
                    IList<ProjectDTO> all = IoC.Get<IProjectAPI>().GetAllProjects();

                    projects = Mapper.Map<IList<ProjectDTO>, IList<ProjectVM>>(all);
                });
                
                return projects;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ProjectVM> GetProject(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectVM project = null;
                await Task.Run(() =>
                {
                    ProjectDTO dto = IoC.Get<IProjectAPI>().GetProject(projectId, includeGraph);

                    project = Mapper.Map<ProjectDTO, ProjectVM>(dto);
                });

                return project;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ProjectConfigurationVM> GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectConfigurationVM project = null;
                await Task.Run(() =>
                {
                    ProjectConfigurationDTO dto = IoC.Get<IProjectAPI>().GetProjectConfiguration(projectId, includeGraph);
                
                    project = Mapper.Map<ProjectConfigurationDTO, ProjectConfigurationVM>(dto);
                });

                return project;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateProjectConfiguration(ProjectConfigurationVM configuration)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    ProjectConfigurationDTO config = Mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(configuration);
                    
                    result = IoC.Get<IProjectAPI>().UpdateProjectConfiguration(config);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> RestoreProjectConfiguration(int projectId)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    result = IoC.Get<IProjectAPI>().RestoreProjectConfiguration(projectId);
                });
                
                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> CreateProject(ProjectVM project, int globalConfigurationId, int marketDataId)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                ProjectDTO dto = Mapper.Map<ProjectVM, ProjectDTO>(project);

                await Task.Run(() => 
                {
                    var service = FacadeService.ProjectAPI;
                    result = service.CreateProject(dto, globalConfigurationId, marketDataId);

                    if (result.IsSuccess)
                    {
                        FacadeService.DirectoryService.CreateDefaultProjectWorkspace(dto.ProjectName);
                        if (int.TryParse(result.EntityId, out int pId))
                        {
                            var conf = service.GetProjectConfiguration(pId);
                            if (conf != null) 
                            {
                                conf.WorkspacePath = ProjectDirectoryManager.DefaultDirectory();
                                FacadeService.ProjectAPI.UpdateProjectConfiguration(conf);
                            }
                        }
                    }
                });
                
                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> PinnedProject(int projectId, bool isPinned)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() => {
                
                    result = IoC.Get<IProjectAPI>().PinnedProject(projectId, isPinned);
                });
                
                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateProcessId(int projectId, long? processId)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() => {
                
                    result = IoC.Get<IProjectAPI>().UpdateProcessId(projectId, processId);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Configuration
        public async Task<IList<ProjectGlobalConfigurationVM>> GetAllGlobalConfigurations(bool includeGraph = false)
        {
            try
            {
                IList<ProjectGlobalConfigurationDTO> all = Array.Empty<ProjectGlobalConfigurationDTO>().ToList();

                await Task.Run(() => {
                    all = IoC.Get<IProjectAPI>().GetAllGlobalConfigurations(includeGraph);
                });

                return Mapper.Map<IList<ProjectGlobalConfigurationDTO>, IList<ProjectGlobalConfigurationVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ProjectGlobalConfigurationVM> GetGlobalConfiguration(int? globalConfigurationId = null, bool includeGraph = false)
        {
            try
            {
                ProjectGlobalConfigurationVM vm = null;
                await Task.Run(() =>
                {
                    var dto = IoC.Get<IProjectAPI>().GetGlobalConfiguration(globalConfigurationId, includeGraph);

                    vm = Mapper.Map<ProjectGlobalConfigurationDTO, ProjectGlobalConfigurationVM>(dto);
                });

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ProjectGlobalConfigurationVM> GetGlobalConfiguration(int configurationId, bool includeGraph = false)
        {
            try
            {
                ProjectGlobalConfigurationDTO result = null;

                await Task.Run(() => {

                    result = IoC.Get<IProjectAPI>().GetGlobalConfiguration(configurationId, includeGraph);
                });

                var vm = Mapper.Map<ProjectGlobalConfigurationDTO, ProjectGlobalConfigurationVM>(result);

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateGlobalConfiguration(ProjectGlobalConfigurationVM configuration)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    ProjectGlobalConfigurationDTO config = Mapper.Map<ProjectGlobalConfigurationVM, ProjectGlobalConfigurationDTO>(configuration);

                    result = IoC.Get<IProjectAPI>().UpdateGlobalConfiguration(config);
                });
                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion
    }
}
