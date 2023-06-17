using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Contracts.Commons;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.Core.Data.Repositories.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Core.Domain.Services.Commons
{
    public class ConfigurationDomainService : DomainServiceBase, IConfigurationDomainService
    {
        private readonly IRepository<Configuration> _configurationRepository;
        private readonly IRepository<ScheduleConfiguration> _scheduleConfigurationRepository;

        public ConfigurationDomainService(string ownerId, string owner,
            IRepository<Configuration> projectGlobalConfigurationRepository,
            IRepository<ScheduleConfiguration> projectGlobalScheduleConfigurationRepository)
            : base(ownerId, owner)
        {
            _configurationRepository = projectGlobalConfigurationRepository;
            _scheduleConfigurationRepository = projectGlobalScheduleConfigurationRepository;
        }

        public IList<Configuration> GetAllConfiguration(bool includeGraph = false)
        {
            try
            {
                IList<Configuration> result = includeGraph
                    ? _configurationRepository.GetAll(
                        gc => gc.Symbol,
                        gc => gc.Timeframe,
                        gc => gc.ScheduleConfigurations).ToList()
                        : _configurationRepository.GetAll().ToList();

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public Configuration GetConfiguration(int? configurationId = null, bool includeGraph = false)
        {
            try
            {
                Expression<Func<Configuration, bool>> predicate =
                    gc => gc.ConfigurationId == configurationId || (gc.EndDate ?? DateTime.MinValue) == DateTime.MinValue;

                var configuration = includeGraph
                    ? _configurationRepository.FirstOrDefault(predicate,
                    gc => gc.Symbol,
                    gc => gc.Timeframe,
                    gc => gc.ScheduleConfigurations)
                    : _configurationRepository.FirstOrDefault(predicate);

                return configuration;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateConfiguration(Configuration configuration)
        {
            try
            {
                var dt = DateTime.UtcNow;

                // Close temporal record
                _configurationRepository.CloseTemporalRecord();

                // Create
                _configurationRepository.Create(configuration);

                return configuration.ConfigurationId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public void UpdateConfiguration(Configuration configuration)
        {
            try
            {
                configuration.UpdatedById = OwnerId;
                configuration.UpdatedByUserName = Owner;
                configuration.UpdatedOn = DateTime.UtcNow;

                _configurationRepository.Update(configuration);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // Schedule Configuration

        public ScheduleConfiguration GetScheduleConfiguration(int marketRegionId)
        {
            try
            {
                return GetConfiguration(includeGraph: true)?.ScheduleConfigurations.FirstOrDefault(
                        schedule => schedule.MarketRegionId == marketRegionId
                    );
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateScheduleConfiguration(ScheduleConfiguration scheduleConfiguration)
        {
            try
            {
                // Close temporal record
                _scheduleConfigurationRepository.CloseTemporalRecord();

                // Create
                _scheduleConfigurationRepository.Create(scheduleConfiguration);

                return scheduleConfiguration.ConfigurationId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public void UpdateScheduleConfiguration(ScheduleConfiguration scheduleConfiguration)
        {
            try
            {
                scheduleConfiguration.UpdatedById = OwnerId;
                scheduleConfiguration.UpdatedByUserName = Owner;
                scheduleConfiguration.UpdatedOn = DateTime.UtcNow;

                CreateScheduleConfiguration(scheduleConfiguration);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}