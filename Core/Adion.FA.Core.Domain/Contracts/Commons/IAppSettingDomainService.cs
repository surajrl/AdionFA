﻿using Adion.FA.Core.Domain.Aggregates.Common;
using System.Collections.Generic;

namespace Adion.FA.Core.Domain.Contracts.Commons
{
    public interface IAppSettingDomainService
    {
        IList<Setting> GetAllAppSetting();
        Setting GetSetting(int settingId, string keySetting = null);
        int CreateAppSetting(Setting setting);
        bool UpdateAppSetting(Setting setting);
    }
}
