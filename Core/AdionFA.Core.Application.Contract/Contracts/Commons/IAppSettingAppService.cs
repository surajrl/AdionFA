using AdionFA.Core.Application.Contract.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Core.Application.Contracts.Commons
{
    public interface IAppSettingAppService : IAppContractBase
    {
        IList<SettingDTO> GetAllAppSetting();
        SettingDTO GetSetting(int settingId, string keySetting = null);
        ResponseDTO CreateAppSetting(SettingDTO setting);
        ResponseDTO UpdateAppSetting(SettingDTO setting);
    }
}
