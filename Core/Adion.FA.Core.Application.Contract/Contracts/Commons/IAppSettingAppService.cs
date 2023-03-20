using Adion.FA.Core.Application.Contract.Contracts;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Common;
using System.Collections.Generic;

namespace Adion.FA.Core.Application.Contracts.Commons
{
    public interface IAppSettingAppService : IAppContractBase
    {
        IList<SettingDTO> GetAllAppSetting();
        SettingDTO GetSetting(int settingId, string keySetting = null);
        ResponseDTO CreateAppSetting(SettingDTO setting);
        ResponseDTO UpdateAppSetting(SettingDTO setting);
    }
}
