using AdionFA.Application.Contracts;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdionFA.Application.Services.Commons
{
    public class GlobalConfigurationService : AppServiceBase, IGlobalConfigurationService
    {
        private readonly IGenericRepository<GlobalConfiguration> _globalConfigurationRepository;

        public GlobalConfigurationService(IGenericRepository<GlobalConfiguration> projectConfigurationRepository)
            : base()
        {
            _globalConfigurationRepository = projectConfigurationRepository;
        }

        public GlobalConfigurationDTO GetGlobalConfiguration()
        {
            try
            {
                var globalConfiguration = _globalConfigurationRepository.FirstOrDefault(
                    null,
                    globalConfiguration => globalConfiguration.GlobalScheduleConfigurations);

                return Mapper.Map<GlobalConfigurationDTO>(globalConfiguration);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDTO> UpdateGlobalConfigurationAsync(GlobalConfigurationDTO globalConfigurationDTO)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false
                };

                var globalConfiguration = Mapper.Map<GlobalConfiguration>(globalConfigurationDTO);

                await _globalConfigurationRepository.UpdateAsync(globalConfiguration).ConfigureAwait(false);

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
