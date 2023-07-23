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
    public class SettingService : AppServiceBase, ISettingService
    {
        private readonly IGenericRepository<Setting> _settingRepository;

        public SettingService(IGenericRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public SettingDTO GetSetting(int settingId)
        {
            try
            {
                var setting = _settingRepository.FirstOrDefault(setting => setting.SettingId == settingId);

                return Mapper.Map<SettingDTO>(setting);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDTO> UpdateSettingAsync(SettingDTO settingDTO)
        {
            try
            {
                var response = new ResponseDTO
                {
                    IsSuccess = false
                };

                var setting = Mapper.Map<Setting>(settingDTO);

                await _settingRepository.UpdateAsync(setting).ConfigureAwait(false);

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
