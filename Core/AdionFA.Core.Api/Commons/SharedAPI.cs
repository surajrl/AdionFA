using AdionFA.Core.API.Contracts.Commons;
using AdionFA.Core.Application.Contracts.Commons;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Core.API.Commons
{
    public class SharedAPI : ISharedAPI
    {
        // Service Host

        public EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId)
        {
            using var service = IoC.Get<ISharedAppService>();
            return service.GetEntityServiceHost(entityTypeId, entityId);
        }

        // App Settings

        public IList<SettingDTO> GetAllAppSetting()
        {
            using var service = IoC.Get<IAppSettingAppService>();
            return service.GetAllAppSetting();
        }

        public SettingDTO GetSetting(int settingId, string keySetting = null)
        {
            using var service = IoC.Get<IAppSettingAppService>();
            return service.GetSetting(settingId, keySetting);
        }

        public ResponseDTO CreateAppSetting(SettingDTO setting)
        {
            using (var service = IoC.Get<IAppSettingAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateAppSetting(setting);
            }
        }

        public ResponseDTO UpdateAppSetting(SettingDTO setting)
        {
            using (var service = IoC.Get<IAppSettingAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateAppSetting(setting);
            }
        }
    }
}
