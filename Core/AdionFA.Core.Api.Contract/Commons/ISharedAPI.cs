using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Core.API.Contracts.Commons
{
    public interface ISharedAPI
    {
        // ServiceHost

        EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId);

        // App Settings

        IList<SettingDTO> GetAllAppSetting();
        SettingDTO GetSetting(int settingId, string keySetting = null);
        ResponseDTO CreateAppSetting(SettingDTO setting);
        ResponseDTO UpdateAppSetting(SettingDTO setting);

        // Configuration

        IList<ConfigurationDTO> GetAllConfiguration(bool includeGraph = false);
        ConfigurationDTO GetConfiguration(int? configurationId = null, bool includeGraph = false);
        ResponseDTO UpdateConfiguration(ConfigurationDTO configuration);
    }
}
