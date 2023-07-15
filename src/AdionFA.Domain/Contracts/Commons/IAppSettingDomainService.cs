using AdionFA.Domain.Entities;
using System.Collections.Generic;

namespace AdionFA.Domain.Contracts.Commons
{
    public interface IAppSettingDomainService
    {
        IList<Setting> GetAllAppSetting();
        Setting GetSetting(int settingId, string keySetting = null);
        int CreateAppSetting(Setting setting);
        bool UpdateAppSetting(Setting setting);
    }
}
