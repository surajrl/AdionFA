using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Core.API.Contracts.Commons
{
    public interface ISharedAPI
    {
        // ServiceHost

        EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId);

        // Settings

        IList<SettingDTO> GetAllAppSetting();
        SettingDTO GetSetting(int settingId, string keySetting = null);
        ResponseDTO CreateAppSetting(SettingDTO setting);
        ResponseDTO UpdateAppSetting(SettingDTO setting);
    }
}
