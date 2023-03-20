using Adion.FA.Core.API.Contracts.Commons;
using Adion.FA.Core.Application.Contracts.Commons;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Common;
using System.Collections.Generic;

namespace Adion.FA.Core.API.Commons
{
    public class SharedAPI : ISharedAPI
    {
        #region ServiceHost

        public EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId)
        {
            using (var service = IoC.Get<ISharedAppService>())
                return service.GetEntityServiceHost(entityTypeId, entityId);
        }
        
        #endregion

        #region Settings

        public IList<SettingDTO> GetAllAppSetting()
        {
            using (var service = IoC.Get<IAppSettingAppService>())
                return service.GetAllAppSetting();
        }

        public SettingDTO GetSetting(int settingId, string keySetting = null)
        {
            using (var service = IoC.Get<IAppSettingAppService>())
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
        
        #endregion
    }
}
