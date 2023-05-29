using AdionFA.Core.Application.Contract.Commons;
using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Contracts.Commons;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdionFA.Core.Application.Services.Commons
{
    public class ConfigurationAppService : AppServiceBase, IConfigurationAppService
    {
        [Inject]
        public IConfigurationDomainService ConfigurationDomainService { get; set; }

        public ConfigurationAppService() : base()
        {
        }

        public IList<ConfigurationDTO> GetAllConfiguration(bool includeGraph = false)
        {
            try
            {
                IList<Configuration> configurations = ConfigurationDomainService.GetAllConfiguration(includeGraph);
                IList<ConfigurationDTO> dto = Mapper.Map<IList<ConfigurationDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<ConfigurationAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ConfigurationDTO GetConfiguration(int? configurationId = null, bool includeGraph = false)
        {
            try
            {
                var gc = ConfigurationDomainService.GetConfiguration(configurationId, includeGraph);
                var dto = Mapper.Map<ConfigurationDTO>(gc);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<ConfigurationAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDto)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var configuration = Mapper.Map<Configuration>(configurationDto);

                ConfigurationDomainService.UpdateConfiguration(configuration);

                response.IsSuccess = true;

                if (response.IsSuccess)
                {
                    LogInfoUpdate<ConfigurationDTO>();
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<ConfigurationAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
