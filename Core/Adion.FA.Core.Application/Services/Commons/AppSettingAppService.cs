using Adion.FA.Core.Application.Contracts.Commons;
using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Contracts.Commons;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Adion.FA.TransferObject.Common;
using Adion.FA.TransferObject.Base;

namespace Adion.FA.Core.Application.Services.Commons
{
    public class AppSettingAppService : AppServiceBase, IAppSettingAppService
    {
        #region Domain Services

        [Inject]
        public IAppSettingDomainService AppSettingDomainService { get; set; }
        
        #endregion

        #region Ctor
        
        public AppSettingAppService() : base()
        {
        }
        
        #endregion

        #region App Setting
        
        public IList<SettingDTO> GetAllAppSetting()
        {
            try
            {
                IList<Setting> settings = AppSettingDomainService.GetAllAppSetting();
                IList<SettingDTO> dtos = Mapper.Map<IList<SettingDTO>>(settings);

                return dtos;
            }
            catch (Exception ex)
            {
                LogException<AppSettingAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SettingDTO GetSetting(int settingId, string keySetting = null)
        {
            try
            {
                Setting setting = AppSettingDomainService.GetSetting(settingId, keySetting);
                SettingDTO dto = Mapper.Map<SettingDTO>(setting);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<AppSettingAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateAppSetting(SettingDTO setting)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                Setting entity = Mapper.Map<Setting>(setting);
                int entityId = AppSettingDomainService.CreateAppSetting(entity);

                if (entityId > 0)
                {
                    LogInfoCreate<SettingDTO>();
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<AppSettingAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateAppSetting(SettingDTO setting)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                Setting entity = Mapper.Map<Setting>(setting);


                if (setting.SettingId > 0)
                    response.IsSuccess = AppSettingDomainService.UpdateAppSetting(entity);
                else
                    response.IsSuccess = AppSettingDomainService.CreateAppSetting(entity) > 0;

                if (response.IsSuccess)
                {
                    LogInfoUpdate<SettingDTO>();
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<AppSettingAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        
        #endregion
    }
}
