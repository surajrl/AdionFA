using AdionFA.Core.Application.Contract.Commons;
using AdionFA.Core.Application.Contracts.MarketData;
using AdionFA.Core.Application.Contracts.Projects;
using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Core.Domain.Contracts.Projects;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdionFA.Core.Application.Services.Projects
{
    public class ProjectAppService : AppServiceBase, IProjectAppService
    {
        [Inject]
        public IConfigurationAppService ConfigurationAppService { get; set; }

        [Inject]
        public IMarketDataAppService MarketDataAppService { get; set; }

        [Inject]
        public IProjectDomainService ProjectDomainService { get; set; }

        private readonly IRepository<ProjectConfiguration> _projectConfigurationRepository;

        public ProjectAppService(IRepository<ProjectConfiguration> projectConfigurationRepository)
            : base()
        {
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        // Project

        public IList<ProjectDTO> GetAllProject()
        {
            try
            {
                var projects = ProjectDomainService.GetAllProjects();
                var dtos = Mapper.Map<IList<ProjectDTO>>(projects);

                return dtos;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph = false)
        {
            try
            {
                var project = ProjectDomainService.GetProject(projectId, includeGraph);
                var dto = Mapper.Map<ProjectDTO>(project);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateProject(ProjectDTO project, int? configurationId = null, int? marketDataId = null)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var p = Mapper.Map<Project>(project);

                var pgc = ConfigurationAppService.GetConfiguration(configurationId);

                var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(pgc, marketDataId ?? 0);
                if (!validation.IsSuccess)
                {
                    response.Message = validation.Message;
                    return response;
                }

                var pId = ProjectDomainService.CreateProject(p, pgc.ConfigurationId, marketDataId);
                response.IsSuccess = pId > 0;
                response.EntityId = pId.ToString();

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateProject(ProjectDTO project)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var p = Mapper.Map<Project>(project);

                if (project.ProjectConfigurations.Any())
                {
                    var pgc = project.ProjectConfigurations.FirstOrDefault(pc => pc.EndDate == null);

                    var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(pgc, pgc.HistoricalDataId ?? 0);
                    if (!validation.IsSuccess)
                    {
                        response.Message = validation.Message;
                        return response;
                    }
                }

                response.IsSuccess = ProjectDomainService.UpdateProject(p);

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                var model = ProjectDomainService.GetProjectConfiguration(projectId, includeGraph);
                var dto = Mapper.Map<ProjectConfigurationDTO>(model);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfiguration)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var pc = Mapper.Map<ProjectConfiguration>(projectConfiguration);

                var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(projectConfiguration, projectConfiguration.HistoricalDataId ?? 0);
                if (!validation.IsSuccess)
                {
                    response.Message = validation.Message;
                    return response;
                }

                response.IsSuccess = ProjectDomainService.CreateProjectConfiguration(pc) > 0;

                return response;
            }
            catch (Exception ex)
            {
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
                    var pc = Mapper.Map<ProjectConfiguration>(projectConfiguration);

                    var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(projectConfiguration, projectConfiguration.HistoricalDataId ?? 0);
                    if (!validation.IsSuccess)
                    {
                        return validation;
                    }

                    response.IsSuccess = ProjectDomainService.UpdateProjectConfiguration(pc);
                }

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO RestoreProjectConfiguration(int projectId)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if (projectId > 0)
                {
                    var pc = ProjectDomainService.RestoreProjectConfiguration(projectId);

                    response.IsSuccess = pc != null;

                    if (response.IsSuccess)
                    {
                        response.Enity = pc;
                        response.EntityId = pc.ProjectConfigurationId.ToString();
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private ResponseDTO CurrencyPairAndCurrencyPeriodMustBeSameValidation(ConfigurationBaseDTO c, int marketDataId)
        {
            var response = new ResponseDTO { IsSuccess = true };

            if (c != null && marketDataId > 0)
            {
                var md = MarketDataAppService.GetHistoricalData(marketDataId);

                if (md != null)
                {
                    var validation = ProjectDTO.CurrencyPairAndCurrencyPeriodMustBeSameValidation(
                    c.SymbolId, c.TimeframeId, md.SymbolId, md.TimeframeId);

                    if (!validation.IsSuccess)
                    {
                        return Mapper.Map<ResponseDTO>(validation);
                    }
                }
            }

            return response;
        }

        // Shell module

        public ResponseDTO PinnedProject(int projectId, bool isPinned)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var projectConf = ProjectDomainService.GetProjectConfiguration(projectId);
                if (projectConf != null)
                {
                    projectConf.IsFavorite = isPinned;
                    _projectConfigurationRepository.Update(projectConf);
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception ex)
            {
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
