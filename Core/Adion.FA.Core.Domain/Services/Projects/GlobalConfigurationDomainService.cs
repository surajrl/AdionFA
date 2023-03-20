using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Core.Domain.Contracts.Projects;
using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.Infrastructure.Core.Data.Repositories.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Adion.FA.Core.Domain.Services.Projects
{
    public class GlobalConfigurationDomainService : DomainServiceBase, IGlobalConfigurationDomainService
    {
        #region Repositories

        public IRepository<ProjectGlobalConfiguration> ProjectGlobalConfigurationRepository { get; set; }
        public IRepository<ProjectGlobalScheduleConfiguration> ProjectGlobalScheduleConfigurationRepository { get; set; }

        #endregion

        #region Ctor

        public GlobalConfigurationDomainService(string tenantId, string ownerId, string owner,
            IRepository<ProjectGlobalConfiguration> projectGlobalConfigurationRepository,
            IRepository<ProjectGlobalScheduleConfiguration> projectGlobalScheduleConfigurationRepository) : base(tenantId, ownerId, owner)
        {
            ProjectGlobalConfigurationRepository = projectGlobalConfigurationRepository;
            ProjectGlobalScheduleConfigurationRepository = projectGlobalScheduleConfigurationRepository;
        }

        #endregion

        #region Project Global Configurations

        public IList<ProjectGlobalConfiguration> GetAllProjectGlobalConfigurations(bool includeGraph = false)
        {
            try
            {
                IList<ProjectGlobalConfiguration> result = includeGraph ? ProjectGlobalConfigurationRepository.GetAll(
                    gc => gc.CurrencyPair,
                    gc => gc.CurrencyPeriod,
                    gc => gc.CurrencySpread,
                    gc => gc.ProjectGlobalScheduleConfigurations
                ).ToList() : ProjectGlobalConfigurationRepository.GetAll().ToList();

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ProjectGlobalConfiguration GetProjectGlobalConfiguration(int? configurationId = null, bool includeGraph = false)
        {
            try
            {
                Expression<Func<ProjectGlobalConfiguration, bool>> predicate =
                    gc => gc.ProjectGlobalConfigurationId == configurationId || (gc.EndDate ?? DateTime.MinValue) == DateTime.MinValue;

                ProjectGlobalConfiguration configuration = includeGraph 
                    ? ProjectGlobalConfigurationRepository.FirstOrDefault(predicate,
                        gc => gc.CurrencyPair,
                        gc => gc.CurrencyPeriod,
                        gc => gc.CurrencySpread,
                        gc => gc.ProjectGlobalScheduleConfigurations
                    ) : ProjectGlobalConfigurationRepository.FirstOrDefault(predicate);

                return configuration;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateProjectGlobalConfiguration(ProjectGlobalConfiguration configuration)
        {
            try
            {
                var dt = DateTime.UtcNow;

                //Close Temporal Record
                ProjectGlobalConfigurationRepository.CloseTemporalRecord();

                ProjectGlobalConfigurationRepository.Create(configuration);

                return configuration.ProjectGlobalConfigurationId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public void UpdateProjectGlobalConfiguration(ProjectGlobalConfiguration configuration)
        {
            try
            {
                configuration.UpdatedById = OwnerId;
                configuration.UpdatedByUserName = Owner;
                configuration.UpdatedOn = DateTime.UtcNow;

                ProjectGlobalConfigurationRepository.Update(configuration);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion

        #region Schedule Configuration

        public ProjectGlobalScheduleConfiguration GetProjectGlobalScheduleConfiguration(int marketRegionId)
        {
            try
            {
                return GetProjectGlobalConfiguration(includeGraph: true)?.ProjectGlobalScheduleConfigurations.FirstOrDefault(
                        schedule => schedule.MarketRegionId == marketRegionId
                    );
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateProjectGlobalScheduleConfiguration(ProjectGlobalScheduleConfiguration scheduleConfiguration)
        {
            try
            {
                //Close temporal record
                ProjectGlobalScheduleConfigurationRepository.CloseTemporalRecord();

                //Create
                ProjectGlobalScheduleConfigurationRepository.Create(scheduleConfiguration);

                return scheduleConfiguration.ProjectGlobalScheduleConfigurationId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public void UpdateProjectGlobalScheduleConfiguration(ProjectGlobalScheduleConfiguration scheduleConfiguration)
        {
            try
            {
                scheduleConfiguration.UpdatedById = OwnerId;
                scheduleConfiguration.UpdatedByUserName = Owner;
                scheduleConfiguration.UpdatedOn = DateTime.UtcNow;

                CreateProjectGlobalScheduleConfiguration(scheduleConfiguration);
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
