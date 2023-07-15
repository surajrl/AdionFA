using AdionFA.Application.Contracts.Commons;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdionFA.Application.Services.Commons
{
    public class AppSettingAppService : AppServiceBase, IAppSettingAppService
    {
        private readonly IGenericRepository<Setting> _settingRepository;

        public AppSettingAppService(IGenericRepository<Setting> settingRepository) : base()
        {
            _settingRepository = settingRepository;
        }

        // App Setting

        public IList<SettingDTO> GetAllAppSetting()
        {
            try
            {
                var settings = _settingRepository.GetAll().ToList();
                var settingDTO = Mapper.Map<IList<SettingDTO>>(settings);

                return settingDTO;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SettingDTO GetSetting(int settingId, string keySetting = null)
        {
            try
            {
                var setting = _settingRepository.FirstOrDefault(setting =>
                setting.SettingId == settingId
                && (keySetting == null || keySetting == setting.Code));

                var dto = Mapper.Map<SettingDTO>(setting);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateAppSetting(SettingDTO setting)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var entity = Mapper.Map<Setting>(setting);
                _settingRepository.Create(entity);
                var entityId = setting.SettingId;

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateAppSetting(SettingDTO setting)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                var entity = Mapper.Map<Setting>(setting);

                if (setting.SettingId > 0)
                {
                    _settingRepository.Update(entity);
                }
                else
                {
                    _settingRepository.Create(entity);
                    response.IsSuccess = entity.SettingId > 0;
                }

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
