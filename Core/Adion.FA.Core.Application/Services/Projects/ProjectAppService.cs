using Adion.FA.Core.Application.Contracts.Projects;
using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Core.Domain.Contracts.Projects;
using Adion.FA.Infrastructure.Enums;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Adion.FA.TransferObject.Project;
using Adion.FA.Core.Application.Contracts.Markets;
using Adion.FA.TransferObject.Market;
using Adion.FA.TransferObject.Base;
using System.Linq;
using Adion.FA.Core.Domain.Contracts.Repositories;

namespace Adion.FA.Core.Application.Services.Projects
{
    public class ProjectAppService : AppServiceBase, IProjectAppService
    {
        #region App Services

        [Inject]
        public IGlobalConfigurationAppService GlobalConfigurationAppService { get; set; }

        [Inject]
        public IMarketDataAppService MarketDataAppService { get; set; }

        #endregion

        #region Domain Services

        [Inject]
        public IProjectDomainService ProjectDomainService { get; set; }

        #endregion

        #region Repositories

        public IRepository<ProjectConfiguration> ProjectConfigurationRepository { get; set; }

        #endregion

        #region Ctor
        
        public ProjectAppService(
            IRepository<ProjectConfiguration> projectConfigurationRepository) : base()
        {
            ProjectConfigurationRepository = projectConfigurationRepository;
        }

        #endregion

        #region Project

        public IList<ProjectDTO> GetAllProjects()
        {
            try
            {
                IList<Project> projects = ProjectDomainService.GetAllProjects();
                IList<ProjectDTO> dtos = Mapper.Map<IList<ProjectDTO>>(projects);

                return dtos;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph = false)
        {
            try
            {
                Project project = ProjectDomainService.GetProject(projectId, includeGraph);
                ProjectDTO dto = Mapper.Map<ProjectDTO>(project);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        
        public ResponseDTO CreateProject(ProjectDTO project, int? globalConfigurationId = null, int? marketDataId = null)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                Project p = Mapper.Map<Project>(project);

                ProjectGlobalConfigurationDTO pgc = GlobalConfigurationAppService.GetGlobalConfiguration(globalConfigurationId);

                var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(pgc, marketDataId ?? 0);
                if(!validation.IsSuccess)
                {
                    response.Message = validation.Message;
                    return response;
                }

                var pId = ProjectDomainService.CreateProject(p, pgc.ProjectGlobalConfigurationId, marketDataId);
                response.IsSuccess = pId > 0;
                response.EntityId = pId.ToString();

                if (response.IsSuccess)
                {
                    LogInfoCreate<ProjectDTO>();
                }

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                LogException<ProjectAppService>(ex);
                throw;
            }
        }
        
        public ResponseDTO UpdateProject(ProjectDTO project)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                Project p = Mapper.Map<Project>(project);

                if(project.ProjectConfigurations.Any())
                {
                    ProjectConfigurationDTO pgc = project.ProjectConfigurations.FirstOrDefault(pc => pc.EndDate == null);

                    var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(pgc, pgc.MarketDataId ?? 0);
                    if (!validation.IsSuccess)
                    {
                        response.Message = validation.Message;
                        return response;
                    }
                }

                response.IsSuccess = ProjectDomainService.UpdateProject(p);

                if (response.IsSuccess)
                {
                    LogInfoUpdate<ProjectDTO>();
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion

        #region Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectConfiguration model = ProjectDomainService.GetProjectConfiguration(projectId, includeGraph);
                ProjectConfigurationDTO dto = Mapper.Map<ProjectConfigurationDTO>(model);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfiguration)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                ProjectConfiguration pc = Mapper.Map<ProjectConfiguration>(projectConfiguration);

                var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(projectConfiguration, projectConfiguration.MarketDataId ?? 0);
                if (!validation.IsSuccess)
                {
                    response.Message = validation.Message;
                    return response;
                }

                response.IsSuccess = ProjectDomainService.CreateProjectConfiguration(pc) > 0;

                if (response.IsSuccess)
                {
                    LogInfoCreate<ProjectConfigurationDTO>();
                }
                
                return response;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfiguration)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if ((projectConfiguration?.ProjectId ?? 0) > 0)
                {
                    
                    ProjectConfiguration pc = Mapper.Map<ProjectConfiguration>(projectConfiguration);

                    var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(projectConfiguration, projectConfiguration.MarketDataId ?? 0);
                    if (!validation.IsSuccess)
                    {
                        return validation;
                    }

                    response.IsSuccess = ProjectDomainService.UpdateProjectConfiguration(pc);
                    
                    if (response.IsSuccess)
                    {
                        LogInfoUpdate<ProjectConfigurationDTO>();
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO RestoreProjectConfiguration(int projectId)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if (projectId  > 0)
                {
                    ProjectConfiguration pc = ProjectDomainService.RestoreProjectConfiguration(projectId);

                    response.IsSuccess = pc != null;
                    
                    if (response.IsSuccess)
                    {
                        response.Enity = pc;
                        response.EntityId = pc.ProjectConfigurationId.ToString();

                        LogInfoUpdate<ProjectConfigurationDTO>();
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private ResponseDTO CurrencyPairAndCurrencyPeriodMustBeSameValidation(ConfigurationBaseDTO c, int marketDataId)
        {
            var response = new ResponseDTO { IsSuccess = true };

            if (c != null && marketDataId > 0)
            {
                MarketDataDTO md = MarketDataAppService.GetMarketData(marketDataId);

                if (md != null)
                {
                    var validation = ProjectDTO.CurrencyPairAndCurrencyPeriodMustBeSameValidation(
                    c.CurrencyPairId, c.CurrencyPeriodId, md.CurrencyPairId, md.CurrencyPeriodId);

                    if (!validation.IsSuccess)
                    {
                        return Mapper.Map<ResponseDTO>(validation);
                    }
                }
            }

            return response;
        }

        #endregion

        #region Shell module

        public ResponseDTO PinnedProject(int projectId, bool isPinned)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };
                
                var projectConf = ProjectDomainService.GetProjectConfiguration(projectId);
                if (projectConf != null)
                {
                    projectConf.IsFavorite = isPinned;
                    ProjectConfigurationRepository.Update(projectConf);
                    response.IsSuccess = true;
                }

                if (response.IsSuccess)
                {
                    LogInfoUpdate<ProjectDTO>();
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateProcessId(int projectId, long? processId)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                response.IsSuccess = ProjectDomainService.UpdateProcessId(projectId, (int)EntityTypeEnum.Project, processId);

                if (response.IsSuccess)
                {
                    LogInfoUpdate<ProjectDTO>();
                }
                
                return response;
            }
            catch (Exception ex)
            {
                LogException<ProjectAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
