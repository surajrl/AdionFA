using AdionFA.Core.Application.Contracts.Commons;
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
    public class AppSettingAppService : AppServiceBase, IAppSettingAppService
    {
        [Inject]
        public IAppSettingDomainService AppSettingDomainService { get; set; }

        public AppSettingAppService() : base()
        {
        }

        // App Setting

        public IList<SettingDTO> GetAllAppSetting()
        {
            try
            {
                var settings = AppSettingDomainService.GetAllAppSetting();
                var dtos = Mapper.Map<IList<SettingDTO>>(settings);

                return dtos;
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
                var setting = AppSettingDomainService.GetSetting(settingId, keySetting);
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
                var entityId = AppSettingDomainService.CreateAppSetting(entity);

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
                    response.IsSuccess = AppSettingDomainService.UpdateAppSetting(entity);
                else
                    response.IsSuccess = AppSettingDomainService.CreateAppSetting(entity) > 0;

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
