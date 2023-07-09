using AdionFA.Core.API.Contracts.Commons;
using AdionFA.Core.API.Contracts.Projects;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Station.Infrastructure.AutoMapper;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Services.AppServices
{
    public class ProjectServiceAgent : IProjectServiceAgent
    {
        private readonly IMapper Mapper;

        public ProjectServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        // Project

        public async Task<IList<ProjectVM>> GetAllProjectAsync()
        {
            try
            {
                IList<ProjectVM> projects = Array.Empty<ProjectVM>().ToList();

                await Task.Run(() =>
                {
                    var all = IoC.Get<IProjectAPI>().GetAllProject();

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

        public async Task<ProjectVM> GetProjectAsync(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectVM project = null;
                await Task.Run(() =>
                {
                    var dto = IoC.Get<IProjectAPI>().GetProject(projectId, includeGraph);

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

        public async Task<ProjectConfigurationVM> GetProjectConfigurationAsync(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectConfigurationVM project = null;
                await Task.Run(() =>
                {
                    var dto = IoC.Get<IProjectAPI>().GetProjectConfiguration(projectId, includeGraph);

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

        public async Task<ResponseVM> UpdateProjectConfigurationAsync(ProjectConfigurationVM configuration)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    var config = Mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(configuration);

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

        public async Task<ResponseVM> RestoreProjectConfigurationAsync(int projectId)
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

        public async Task<ResponseVM> CreateProjectAsync(ProjectVM project, int globalConfigurationId, int marketDataId)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                var dto = Mapper.Map<ProjectVM, ProjectDTO>(project);

                await Task.Run(() =>
                {
                    var service = FacadeService.ProjectAPI;
                    result = service.CreateProject(dto, globalConfigurationId, marketDataId);

                    if (result.IsSuccess)
                    {
                        FacadeService.DirectoryService.CreateDefaultProjectWorkspace(dto.ProjectName);

                        if (int.TryParse(result.EntityId, out var pId))
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

        public async Task<ResponseVM> PinnedProjectAsync(int projectId, bool isPinned)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
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

        public async Task<ResponseVM> UpdateProcessIdAsync(int projectId, long? processId)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
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

        // Configuration

        public async Task<IList<ConfigurationVM>> GetAllConfigurationAsync(bool includeGraph = false)
        {
            try
            {
                IList<ConfigurationDTO> all = Array.Empty<ConfigurationDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<ISharedAPI>().GetAllConfiguration(includeGraph);
                });

                return Mapper.Map<IList<ConfigurationDTO>, IList<ConfigurationVM>>((IList<ConfigurationDTO>)all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ConfigurationVM> GetConfigurationAsync(int? configurationId = null, bool includeGraph = false)
        {
            try
            {
                ConfigurationVM vm = null;
                await Task.Run(() =>
                {
                    var dto = IoC.Get<ISharedAPI>().GetConfiguration(configurationId, includeGraph);

                    vm = Mapper.Map<TransferObject.Common.ConfigurationDTO, ConfigurationVM>(dto);
                });

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ConfigurationVM> GetConfigurationAsync(int configurationId, bool includeGraph = false)
        {
            try
            {
                TransferObject.Common.ConfigurationDTO result = null;

                await Task.Run(() =>
                {
                    result = IoC.Get<ISharedAPI>().GetConfiguration(configurationId, includeGraph);
                });

                var vm = Mapper.Map<TransferObject.Common.ConfigurationDTO, ConfigurationVM>(result);

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateConfigurationAsync(ConfigurationVM configuration)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    var config = Mapper.Map<ConfigurationVM, TransferObject.Common.ConfigurationDTO>(configuration);

                    result = IoC.Get<ISharedAPI>().UpdateConfiguration(config);
                });
                return Mapper.Map<ResponseDTO, ResponseVM>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
