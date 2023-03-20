using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Core.Domain.Contracts.Projects;
using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.Core.Domain.Exceptions;
using Adion.FA.Infrastructure.Core.Data.Repositories.Extension;
using Adion.FA.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Adion.FA.Core.Domain.Services.Projects
{
    public class ProjectDomainService : DomainServiceBase, IProjectDomainService
    {
        #region Repositories

        readonly IRepository<Project> ProjectRepository;

        readonly IRepository<ProjectGlobalConfiguration> ProjectGlobalConfigurationRepository;

        readonly IRepository<ProjectConfiguration> ProjectConfigurationRepository;

        readonly IRepository<ProjectScheduleConfiguration> ProjectScheduleConfigurationRepository;

        readonly IRepository<EntityServiceHost> EntityServiceHostRepository;

        #endregion

        #region Ctor

        public ProjectDomainService(string tenantId, string ownerId, string owner, 
            IRepository<Project> projectRepository,
            IRepository<ProjectGlobalConfiguration> projectGlobalConfigurationRepository,
            IRepository<ProjectConfiguration> projectConfigurationRepository,
            IRepository<ProjectScheduleConfiguration> projectScheduleConfigurationRepository,
            IRepository<EntityServiceHost> entityServiceHostRepository) : base(tenantId, ownerId, owner) 
        {
            ProjectRepository = projectRepository;
            ProjectGlobalConfigurationRepository = projectGlobalConfigurationRepository;
            ProjectConfigurationRepository = projectConfigurationRepository;
            ProjectScheduleConfigurationRepository = projectScheduleConfigurationRepository;
            EntityServiceHostRepository = entityServiceHostRepository;
        }

        #endregion

        #region Projects

        public IList<Project> GetAllProjects()
        {
            try
            {
                List<Project> all = ProjectRepository.GetAll(
                    p => p.ProjectConfigurations
                ).ToList();

                int[] pIds = all.Select(p => p.ProjectId).ToArray();

                var processHistory = from esh in EntityServiceHostRepository.GetAll(_esh => pIds.Contains(_esh.EntityId) && _esh.EntityTypeId == (int)EntityTypeEnum.Project)
                                     select esh;

                all.ForEach(p => 
                {
                    var esh = processHistory.FirstOrDefault(_esh => _esh.EntityId == p.ProjectId);
                    p.ProcessLastDate = esh?.UpdatedOn ?? esh?.CreatedOn ?? p.CreatedOn;
                    p.ProcessId = esh?.ProcessId ?? 0;
                });

                return all;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public Project GetProject(int projectId, bool includeGraph = false)
        {
            try
            {
                Expression<Func<Project, bool>> predicate = p => p.ProjectId == projectId;

                var includes = new List<Expression<Func<Project, dynamic>>> { };
                if (includeGraph)
                {
                    includes.Add(p => p.ProjectStep);
                    includes.Add(p => p.ExpertAdvisors);

                    Project p = ProjectRepository.FirstOrDefault(predicate, includes.ToArray());

                    var pconfig = GetProjectConfiguration(p.ProjectId, true);
                    p.ProjectConfigurations = new List<ProjectConfiguration>() 
                    {
                        pconfig
                    };

                    var entityHost = EntityServiceHostRepository.FirstOrDefault(
                        eh => eh.EntityId == p.ProjectId && eh.EntityTypeId == (int)EntityTypeEnum.Project);
                    //p.ProcessId = entityHost.ProcessId;

                    return p;
                }

                return ProjectRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateProject(Project project, int globalConfigurationId, int? marketDataId = null)
        {
            try
            {
                Project p = new Project
                {
                    ProjectName = project.ProjectName,
                    ProjectStepId = project.ProjectStepId,
                    ProjectConfigurations = new List<ProjectConfiguration>(),
                };

                #region Find ProjectGlobalConfiguration

                ProjectGlobalConfiguration configuration = ProjectGlobalConfigurationRepository.FirstOrDefault(
                    c => c.ProjectGlobalConfigurationId == globalConfigurationId,
                    c => c.ProjectGlobalScheduleConfigurations
                ) ?? throw new GlobalConfigurationNotFoundException();

                #endregion

                ProjectConfiguration projectConfiguration = new ProjectConfiguration
                {
                    MarketDataId = marketDataId,

                    #region From Global Configuration

                    Description = configuration.Description,

                    #region Extractor

                    Variation = configuration.Variation,

                    #endregion

                    #region Period

                    FromDateIS = configuration.FromDateIS,
                    ToDateIS = configuration.ToDateIS,
                    FromDateOS = configuration.FromDateOS,
                    ToDateOS = configuration.ToDateOS,

                    #endregion

                    #region Schedule

                    WithoutSchedule = configuration.WithoutSchedule,

                    #endregion

                    #region Currency

                    CurrencyPairId = configuration.CurrencyPairId,
                    CurrencyPeriodId = configuration.CurrencyPeriodId,
                    CurrencySpreadId = configuration.CurrencySpreadId,

                    #endregion

                    #region Weka

                    TotalInstanceWeka = configuration.TotalInstanceWeka,

                    DepthWeka = configuration.DepthWeka,
                    MinAdjustDepthWeka = configuration.MinAdjustDepthWeka,

                    TotalDecimalWeka = configuration.TotalDecimalWeka,
                    MinimalSeed = configuration.MinimalSeed,
                    MaximumSeed = configuration.MaximumSeed,

                    MaxRatioTree = configuration.MaxRatioTree,
                    MinAdjustMaxRatioTree = configuration.MinAdjustMaxRatioTree,

                    NTotalTree = configuration.NTotalTree,
                    MinAdjustNTotalTree = configuration.MinAdjustNTotalTree,

                    #endregion

                    #region Strategy Builder

                    MinTransactionCountIS = configuration.MinTransactionCountIS,
                    MinAdjustMinTransactionCountIS = configuration.MinAdjustMinTransactionCountIS,
                    MinPercentSuccessIS = configuration.MinPercentSuccessIS,
                    MinAdjustMinPercentSuccessIS = configuration.MinAdjustMinPercentSuccessIS,

                    MinTransactionCountOS = configuration.MinTransactionCountOS,
                    MinAdjustMinTransactionCountOS = configuration.MinAdjustMinTransactionCountOS,
                    MinPercentSuccessOS = configuration.MinPercentSuccessOS,
                    MinAdjustMinPercentSuccessOS = configuration.MinAdjustMinPercentSuccessOS,

                    VariationTransaction = configuration.VariationTransaction,
                    MinAdjustVariationTransaction = configuration.MinAdjustVariationTransaction,

                    Progressiveness = configuration.Progressiveness,
                    MinAdjustProgressiveness = configuration.MinAdjustProgressiveness,
                    IsProgressiveness = configuration.IsProgressiveness,

                    MaxPercentCorrelation = configuration.MaxPercentCorrelation,

                    WinningStrategyTotalUP = configuration.WinningStrategyTotalUP,
                    WinningStrategyTotalDOWN = configuration.WinningStrategyTotalDOWN,

                    AutoAdjustConfig = configuration.AutoAdjustConfig,
                    MaxAdjustConfig = configuration.MaxAdjustConfig,
                    AsynchronousMode = configuration.AsynchronousMode,

                    #endregion

                    #region Assembled Builder

                    TransactionTarget = configuration.TransactionTarget,
                    MinAssemblyPercent = configuration.MinAssemblyPercent,
                    TotalAssemblyIterations = configuration.TotalAssemblyIterations,

                    #endregion

                    ProjectScheduleConfigurations = new List<ProjectScheduleConfiguration>(),

                    #endregion

                    StartDate = DateTime.UtcNow,
                    EndDate = null,

                    TenantId = _tenantId,
                    CreatedById = _ownerId,
                    CreatedByUserName = _owner,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                    Inaccesible = false,
                };

                if (configuration.ProjectGlobalScheduleConfigurations?.Any() ?? false)
                {
                    foreach (var sch in configuration.ProjectGlobalScheduleConfigurations)
                    {
                        projectConfiguration.ProjectScheduleConfigurations.Add(new ProjectScheduleConfiguration 
                        {
                            MarketRegionId = sch.MarketRegionId,
                            FromTimeInSeconds = sch.FromTimeInSeconds,
                            ToTimeInSeconds = sch.ToTimeInSeconds,

                            StartDate = DateTime.UtcNow,
                            EndDate = null,

                            TenantId = _tenantId,
                            CreatedById = _ownerId,
                            CreatedByUserName = _owner,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false,
                            Inaccesible = false,
                        });
                    }                   
                }

                p.ProjectConfigurations.Add(projectConfiguration);
                ProjectRepository.Create(p);

                return p.ProjectId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool UpdateProject(Project project)
        {
            try
            {
                if (project.ProjectId > 0)
                {
                    ProjectRepository.Update(project);

                    project.ProjectConfigurations?.ToList().ForEach(pc =>
                    {
                        if (pc.ProjectConfigurationId > 0)
                            UpdateProjectConfiguration(pc);
                    });

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion

        #region Project Configuration

        public ProjectConfiguration GetProjectConfiguration(int projectId, bool includeGraph = false)
        {
            try
            {
                ProjectConfiguration pc = includeGraph ? ProjectConfigurationRepository.FirstOrDefault(
                    pc => pc.ProjectId == projectId && pc.EndDate == null,
                    pc => pc.ProjectScheduleConfigurations,
                    pc => pc.MarketData
                ) : ProjectConfigurationRepository.FirstOrDefault(
                    pc => pc.ProjectId == projectId && pc.EndDate == null
                );
                return pc;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateProjectConfiguration(ProjectConfiguration projectConfiguration)
        {
            try
            {
                ProjectConfigurationRepository.CloseTemporalRecord();

                projectConfiguration.StartDate = DateTime.UtcNow;
                
                ProjectConfigurationRepository.Create(projectConfiguration);

                return projectConfiguration.ProjectConfigurationId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool UpdateProjectConfiguration(ProjectConfiguration projectConfiguration)
        {
            try
            {
                if (projectConfiguration.ProjectConfigurationId > 0)
                {
                    ProjectConfigurationRepository.Update(projectConfiguration);

                    if ((projectConfiguration.ProjectScheduleConfigurations?.Count ?? 0) > 0)
                    {
                        foreach (var sch in projectConfiguration.ProjectScheduleConfigurations)
                        {
                            if (sch.ProjectScheduleConfigurationId > 0)
                                ProjectScheduleConfigurationRepository.Update(sch);
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ProjectConfiguration RestoreProjectConfiguration(int projectId)
        {
            try
            {
                #region Find ProjectGlobalConfiguration

                ProjectGlobalConfiguration configuration = ProjectGlobalConfigurationRepository.FirstOrDefault(
                    c => c.EndDate == null,
                    c => c.ProjectGlobalScheduleConfigurations) ?? throw new GlobalConfigurationNotFoundException();

                #endregion

                ProjectConfigurationRepository.CloseTemporalRecord();

                ProjectConfiguration projectConfiguration = new ProjectConfiguration
                {
                    ProjectId = projectId,

                    #region Global Configuration

                    Description = configuration.Description,

                    #region Extractor

                    Variation = configuration.Variation,

                    #endregion

                    #region Period

                    FromDateIS = configuration.FromDateIS,
                    ToDateIS = configuration.ToDateIS,
                    FromDateOS = configuration.FromDateOS,
                    ToDateOS = configuration.ToDateOS,

                    #endregion

                    #region Schedule

                    WithoutSchedule = configuration.WithoutSchedule,

                    #endregion

                    #region Currency

                    CurrencyPairId = configuration.CurrencyPairId,
                    CurrencyPeriodId = configuration.CurrencyPeriodId,
                    CurrencySpreadId = configuration.CurrencySpreadId,

                    #endregion

                    #region Weka

                    TotalInstanceWeka = configuration.TotalInstanceWeka,

                    DepthWeka = configuration.DepthWeka,
                    MinAdjustDepthWeka = configuration.MinAdjustDepthWeka,

                    TotalDecimalWeka = configuration.TotalDecimalWeka,
                    MinimalSeed = configuration.MinimalSeed,
                    MaximumSeed = configuration.MaximumSeed,

                    MaxRatioTree = configuration.MaxRatioTree,
                    MinAdjustMaxRatioTree = configuration.MinAdjustMaxRatioTree,

                    NTotalTree = configuration.NTotalTree,
                    MinAdjustNTotalTree = configuration.MinAdjustNTotalTree,

                    #endregion

                    #region Strategy Builder

                    MinTransactionCountIS = configuration.MinTransactionCountIS,
                    MinAdjustMinTransactionCountIS = configuration.MinAdjustMinTransactionCountIS,
                    MinPercentSuccessIS = configuration.MinPercentSuccessIS,
                    MinAdjustMinPercentSuccessIS = configuration.MinAdjustMinPercentSuccessIS,

                    MinTransactionCountOS = configuration.MinTransactionCountOS,
                    MinAdjustMinTransactionCountOS = configuration.MinAdjustMinTransactionCountOS,
                    MinPercentSuccessOS = configuration.MinPercentSuccessOS,
                    MinAdjustMinPercentSuccessOS = configuration.MinAdjustMinPercentSuccessOS,

                    VariationTransaction = configuration.VariationTransaction,
                    MinAdjustVariationTransaction = configuration.MinAdjustVariationTransaction,

                    Progressiveness = configuration.Progressiveness,
                    MinAdjustProgressiveness = configuration.MinAdjustProgressiveness,
                    IsProgressiveness = configuration.IsProgressiveness,

                    MaxPercentCorrelation = configuration.MaxPercentCorrelation,

                    WinningStrategyTotalUP = configuration.WinningStrategyTotalUP,
                    WinningStrategyTotalDOWN = configuration.WinningStrategyTotalDOWN,

                    AutoAdjustConfig = configuration.AutoAdjustConfig,
                    MaxAdjustConfig = configuration.MaxAdjustConfig,
                    AsynchronousMode = configuration.AsynchronousMode,

                    #endregion

                    #region Assembled Builder

                    TransactionTarget = configuration.TransactionTarget,
                    MinAssemblyPercent = configuration.MinAssemblyPercent,
                    TotalAssemblyIterations = configuration.TotalAssemblyIterations,

                    #endregion

                    ProjectScheduleConfigurations = new List<ProjectScheduleConfiguration>(),

                    #endregion

                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                };

                if (configuration.ProjectGlobalScheduleConfigurations?.Any() ?? false)
                {
                    foreach (var sch in configuration.ProjectGlobalScheduleConfigurations)
                    {
                        projectConfiguration.ProjectScheduleConfigurations.Add(new ProjectScheduleConfiguration
                        {
                            MarketRegionId = sch.MarketRegionId,
                            FromTimeInSeconds = sch.FromTimeInSeconds,
                            ToTimeInSeconds = sch.ToTimeInSeconds,

                            StartDate = DateTime.UtcNow,
                            EndDate = null,

                            TenantId = _tenantId,
                            CreatedById = _ownerId,
                            CreatedByUserName = _owner,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false,
                            Inaccesible = false,
                        });
                    }
                }

                ProjectConfigurationRepository.Create(projectConfiguration);

                return projectConfiguration;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Process

        public bool UpdateProcessId(int entityId, int entityTypeId, long? processId)
        {
            try
            {
                var esh = EntityServiceHostRepository.FirstOrDefault(
                    _esh => _esh.EntityId == entityId &&
                            _esh.EntityTypeId == entityTypeId);

                if (esh is null)
                {
                    esh = new EntityServiceHost
                    {
                        EntityTypeId= entityTypeId,
                        EntityId= entityId,
                    };
                }

                esh.ProcessId = processId ?? 0;
                esh.UpdatedById = _ownerId;

                if (esh.EntityServiceHostId == 0)
                    EntityServiceHostRepository.Create(esh);
                else
                    EntityServiceHostRepository.Update(esh);

                return true;
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
