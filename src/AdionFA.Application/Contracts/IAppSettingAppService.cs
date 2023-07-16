using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Application.Contracts
{
    public interface IAppSettingAppService
    {
        IList<SettingDTO> GetAllAppSetting();
        SettingDTO GetSetting(int settingId, string keySetting = null);
        ResponseDTO CreateAppSetting(SettingDTO setting);
        ResponseDTO UpdateAppSetting(SettingDTO setting);
    }
}
