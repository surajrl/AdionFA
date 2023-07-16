using AdionFA.Application.Contracts;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Application.Services.Commons
{
    public class ConfigurationAppService : AppServiceBase, IConfigurationAppService
    {
        private readonly IGenericRepository<Configuration> _configurationRepository;

        public ConfigurationAppService(IGenericRepository<Configuration> projectConfigurationRepository)
            : base()
        {
            _configurationRepository = projectConfigurationRepository;
        }

        public IList<ConfigurationDTO> GetAllConfiguration(bool includeGraph = false)
        {
            try
            {
                IList<Configuration> configurations = includeGraph
                    ? _configurationRepository.GetAll(
                        configuration => configuration.Symbol,
                        configuration => configuration.Timeframe,
                        configuration => configuration.ScheduleConfigurations).ToList()
                        : _configurationRepository.GetAll().ToList();

                var dto = Mapper.Map<IList<ConfigurationDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ConfigurationDTO GetConfiguration(int? configurationId = null, bool includeGraph = false)
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

                var configurationDTO = Mapper.Map<ConfigurationDTO>(configuration);

                return configurationDTO;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDTO)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var configuration = Mapper.Map<Configuration>(configurationDTO);

                configuration.UpdatedById = Id;
                configuration.UpdatedByUserName = Username;
                configuration.UpdatedOn = DateTime.UtcNow;

                _configurationRepository.Update(configuration);

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
