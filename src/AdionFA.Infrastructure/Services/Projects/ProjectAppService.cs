using AdionFA.Application.Contracts.Projects;
using AdionFA.Application.Services.Commons;
using AdionFA.Application.Services.MarketData;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.Domain.Extensions;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Application.Services.Projects
{
    public class ProjectAppService : AppServiceBase, IProjectAppService
    {
        private readonly string _ownerId = "0";
        private readonly string _owner = "admin";

        [Inject]
        public ConfigurationAppService ConfigurationService { get; set; }

        [Inject]
        public MarketDataAppService MarketDataService { get; set; }

        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<ProjectConfiguration> _projectConfigurationRepository;
        private readonly IGenericRepository<ProjectScheduleConfiguration> _projectScheduleConfigurationRepository;
        private readonly IGenericRepository<Configuration> _configurationRepository;

        public ProjectAppService(
            IGenericRepository<Project> projectRepository,
            IGenericRepository<Configuration> configurationRepository,
            IGenericRepository<ProjectConfiguration> projectConfigurationRepository,
            IGenericRepository<ProjectScheduleConfiguration> projectScheduleConfigurationRepository)
            : base()
        {
            _projectRepository = projectRepository;
            _configurationRepository = configurationRepository;
            _projectConfigurationRepository = projectConfigurationRepository;
            _projectScheduleConfigurationRepository = projectScheduleConfigurationRepository;
        }

        // Project

        public IList<ProjectDTO> GetAllProject()
        {
            try
            {
                var allProject = _projectRepository.GetAll(p => p.ProjectConfigurations).ToList();

                return Mapper.Map<IList<ProjectDTO>>(allProject);
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
                Project project;
                Expression<Func<Project, bool>> predicate = p => p.ProjectId == projectId;

                var includes = new List<Expression<Func<Project, dynamic>>> { };
                if (includeGraph)
                {
                    includes.Add(p => p.ExpertAdvisors);

                    project = _projectRepository.FirstOrDefault(predicate, includes.ToArray());

                    var projectConfigurationDTO = GetProjectConfiguration(project.ProjectId, true);
                    project.ProjectConfigurations = new List<ProjectConfiguration>()
                    {
                         Mapper.Map<ProjectConfiguration>(projectConfigurationDTO)
                    };

                    return Mapper.Map<ProjectDTO>(project);
                }

                project = _projectRepository.FirstOrDefault(predicate, includes.ToArray());

                return Mapper.Map<ProjectDTO>(project);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateProject(ProjectDTO projectDTO, int? configurationId = null, int? marketDataId = null)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var project = Mapper.Map<Project>(projectDTO);

                var pgc = ConfigurationService.GetConfiguration(configurationId);

                var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(pgc, marketDataId ?? 0);
                if (!validation.IsSuccess)
                {
                    response.Message = validation.Message;
                    return response;
                }

                Project p = new()
                {
                    ProjectName = projectDTO.ProjectName,
                    ProjectConfigurations = new List<ProjectConfiguration>(),
                };

                // Find Project Global Configuration

                var configuration = _configurationRepository.FirstOrDefault(
                    c => c.ConfigurationId == configurationId,
                    c => c.ScheduleConfigurations);

                ProjectConfiguration projectConfiguration = new()
                {
                    HistoricalDataId = marketDataId,

                    // From Global Configuration

                    Description = configuration.Description,

                    // Extractor

                    ExtractorMinVariation = configuration.ExtractorMinVariation,

                    // Period

                    FromDateIS = configuration.FromDateIS,
                    ToDateIS = configuration.ToDateIS,
                    FromDateOS = configuration.FromDateOS,
                    ToDateOS = configuration.ToDateOS,
                    WithoutSchedule = configuration.WithoutSchedule,

                    // Currency

                    SymbolId = configuration.SymbolId,
                    TimeframeId = configuration.TimeframeId,

                    // Weka

                    TotalInstanceWeka = configuration.TotalInstanceWeka,
                    DepthWeka = configuration.DepthWeka,
                    TotalDecimalWeka = configuration.TotalDecimalWeka,
                    MinimalSeed = configuration.MinimalSeed,
                    MaximumSeed = configuration.MaximumSeed,
                    MaxRatioTree = configuration.MaxRatioTree,
                    NTotalTree = configuration.NTotalTree,

                    // Strategy Builder

                    SBMinTransactionsIS = configuration.SBMinTransactionsIS,
                    SBMinSuccessRatePercentIS = configuration.SBMinSuccessRatePercentIS,

                    SBMinTransactionsOS = configuration.SBMinTransactionsIS,
                    SBMinSuccessRatePercentOS = configuration.SBMinSuccessRatePercentOS,

                    SBMaxSuccessRateVariation = configuration.SBMaxSuccessRateVariation,

                    MaxProgressivenessVariation = configuration.MaxProgressivenessVariation,
                    IsProgressiveness = configuration.IsProgressiveness,

                    SBMaxCorrelationPercent = configuration.SBMaxCorrelationPercent,

                    SBWinningStrategyUPTarget = configuration.SBWinningStrategyUPTarget,
                    SBWinningStrategyDOWNTarget = configuration.SBWinningStrategyDOWNTarget,
                    SBTransactionsTarget = configuration.SBTransactionsTarget,

                    // Assembly Builder

                    ABTransactionsTarget = configuration.ABTransactionsTarget,
                    ABMinImprovePercent = configuration.ABMinImprovePercent,

                    ProjectScheduleConfigurations = new List<ProjectScheduleConfiguration>(),

                    // Other

                    StartDate = DateTime.UtcNow,
                    EndDate = null,

                    CreatedById = _ownerId,
                    CreatedByUserName = _owner,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                };

                if (configuration.ScheduleConfigurations?.Any() ?? false)
                {
                    foreach (var sch in configuration.ScheduleConfigurations)
                    {
                        projectConfiguration.ProjectScheduleConfigurations.Add(new ProjectScheduleConfiguration
                        {
                            MarketRegionId = sch.MarketRegionId,

                            FromTimeInSeconds = sch.FromTimeInSeconds,
                            ToTimeInSeconds = sch.ToTimeInSeconds,

                            StartDate = DateTime.UtcNow,
                            EndDate = null,

                            CreatedById = _ownerId,
                            CreatedByUserName = _owner,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false,
                        });
                    }
                }

                project.ProjectConfigurations.Add(projectConfiguration);
                _projectRepository.Create(project);

                response.IsSuccess = project.ProjectId > 0;
                response.EntityId = project.ProjectId.ToString();

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateProject(ProjectDTO projectDTO)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var project = Mapper.Map<Project>(projectDTO);

                if (project.ProjectConfigurations.Any())
                {
                    var projectConfiguration = project.ProjectConfigurations.FirstOrDefault(pc => pc.EndDate == null);

                    var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(Mapper.Map<ProjectConfigurationDTO>(projectConfiguration), projectConfiguration.HistoricalDataId ?? 0);
                    if (!validation.IsSuccess)
                    {
                        response.Message = validation.Message;
                        return response;
                    }
                }

                if (project.ProjectId > 0)
                {
                    _projectRepository.Update(project);

                    project.ProjectConfigurations?.ToList().ForEach(projectConfiguration =>
                    {
                        if (projectConfiguration.ProjectConfigurationId > 0)
                        {
                            UpdateProjectConfiguration(Mapper.Map<ProjectConfigurationDTO>(projectConfiguration));
                        }
                    });

                    response.IsSuccess = true;
                    return response;
                }

                response.IsSuccess = false;
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
                var projectConfiguration = includeGraph
                    ? _projectConfigurationRepository.FirstOrDefault(
                        pc => pc.ProjectId == projectId && pc.EndDate == null,
                        pc => pc.ProjectScheduleConfigurations,
                        pc => pc.HistoricalData)
                    : _projectConfigurationRepository.FirstOrDefault(
                        pc => pc.ProjectId == projectId && pc.EndDate == null);

                return Mapper.Map<ProjectConfigurationDTO>(projectConfiguration);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateProjectConfiguration(ProjectConfigurationDTO projectConfigurationDTO)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var projectConfiguration = Mapper.Map<ProjectConfiguration>(projectConfigurationDTO);

                var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(projectConfigurationDTO, projectConfigurationDTO.HistoricalDataId ?? 0);
                if (!validation.IsSuccess)
                {
                    response.Message = validation.Message;
                    return response;
                }

                _projectConfigurationRepository.CloseTemporalRecord();

                projectConfigurationDTO.StartDate = DateTime.UtcNow;

                _projectConfigurationRepository.Create(projectConfiguration);


                response.IsSuccess = projectConfiguration.ProjectConfigurationId > 0;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfigurationDTO)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if ((projectConfigurationDTO?.ProjectId ?? 0) > 0)
                {
                    var projectConfiguration = Mapper.Map<ProjectConfiguration>(projectConfigurationDTO);

                    var validation = CurrencyPairAndCurrencyPeriodMustBeSameValidation(Mapper.Map<ProjectConfigurationDTO>(projectConfiguration), projectConfigurationDTO.HistoricalDataId ?? 0);
                    if (!validation.IsSuccess)
                    {
                        return validation;
                    }

                    if (projectConfiguration.ProjectConfigurationId > 0)
                    {
                        _projectConfigurationRepository.Update(projectConfiguration);

                        if ((projectConfiguration.ProjectScheduleConfigurations?.Count ?? 0) > 0)
                        {
                            foreach (var projectScheduleConfiguration in projectConfiguration.ProjectScheduleConfigurations)
                            {
                                if (projectScheduleConfiguration.ProjectScheduleConfigurationId > 0)
                                {
                                    _projectScheduleConfigurationRepository.Update(projectScheduleConfiguration);
                                }
                            }
                        }

                        response.IsSuccess = true;
                        return response;
                    }

                    response.IsSuccess = false;
                    return response;
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
                    var configuration = _configurationRepository.FirstOrDefault(
                    c => c.EndDate == null,
                    c => c.ScheduleConfigurations);

                    _projectConfigurationRepository.CloseTemporalRecord();

                    ProjectConfiguration projectConfiguration = new()
                    {
                        ProjectId = projectId,

                        // Global Configuration

                        Description = configuration.Description,

                        // Extractor

                        ExtractorMinVariation = configuration.ExtractorMinVariation,

                        // Period

                        FromDateIS = configuration.FromDateIS,
                        ToDateIS = configuration.ToDateIS,
                        FromDateOS = configuration.FromDateOS,
                        ToDateOS = configuration.ToDateOS,

                        // Schedule

                        WithoutSchedule = configuration.WithoutSchedule,

                        // Currency

                        SymbolId = configuration.SymbolId,
                        TimeframeId = configuration.TimeframeId,

                        // Weka

                        TotalInstanceWeka = configuration.TotalInstanceWeka,
                        DepthWeka = configuration.DepthWeka,
                        TotalDecimalWeka = configuration.TotalDecimalWeka,
                        MinimalSeed = configuration.MinimalSeed,
                        MaximumSeed = configuration.MaximumSeed,
                        MaxRatioTree = configuration.MaxRatioTree,
                        NTotalTree = configuration.NTotalTree,

                        // Strategy Builder

                        SBMinTransactionsIS = configuration.SBMinTransactionsIS,
                        SBMinSuccessRatePercentIS = configuration.SBMinSuccessRatePercentIS,

                        SBMinTransactionsOS = configuration.SBMinTransactionsIS,
                        SBMinSuccessRatePercentOS = configuration.SBMinSuccessRatePercentOS,

                        SBMaxSuccessRateVariation = configuration.SBMaxSuccessRateVariation,

                        MaxProgressivenessVariation = configuration.MaxProgressivenessVariation,
                        IsProgressiveness = configuration.IsProgressiveness,

                        SBMaxCorrelationPercent = configuration.SBMaxCorrelationPercent,

                        SBWinningStrategyUPTarget = configuration.SBWinningStrategyUPTarget,
                        SBWinningStrategyDOWNTarget = configuration.SBWinningStrategyDOWNTarget,
                        SBTransactionsTarget = configuration.SBTransactionsTarget,

                        // Assembly Builder

                        ABTransactionsTarget = configuration.ABTransactionsTarget,
                        ABMinImprovePercent = configuration.ABMinImprovePercent,

                        ProjectScheduleConfigurations = new List<ProjectScheduleConfiguration>(),

                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                    };

                    if (configuration.ScheduleConfigurations?.Any() ?? false)
                    {
                        foreach (var sch in configuration.ScheduleConfigurations)
                        {
                            projectConfiguration.ProjectScheduleConfigurations.Add(new ProjectScheduleConfiguration
                            {
                                MarketRegionId = sch.MarketRegionId,
                                FromTimeInSeconds = sch.FromTimeInSeconds,
                                ToTimeInSeconds = sch.ToTimeInSeconds,

                                StartDate = DateTime.UtcNow,
                                EndDate = null,

                                CreatedById = _ownerId,
                                CreatedByUserName = _owner,
                                CreatedOn = DateTime.UtcNow,
                                IsDeleted = false,
                            });
                        }
                    }

                    _projectConfigurationRepository.Create(projectConfiguration);

                    var pc = projectConfiguration;

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

        private ResponseDTO CurrencyPairAndCurrencyPeriodMustBeSameValidation(ConfigurationBaseDTO configurationDTO, int marketDataId)
        {
            var response = new ResponseDTO { IsSuccess = true };

            if (configurationDTO != null && marketDataId > 0)
            {
                var md = MarketDataService.GetHistoricalData(marketDataId);

                if (md != null)
                {
                    var validation = ProjectDTO.CurrencyPairAndCurrencyPeriodMustBeSameValidation(
                    configurationDTO.SymbolId, configurationDTO.TimeframeId, md.SymbolId, md.TimeframeId);

                    if (!validation.IsSuccess)
                    {
                        return Mapper.Map<ResponseDTO>(validation);
                    }
                }
            }

            return response;
        }
    }
}
