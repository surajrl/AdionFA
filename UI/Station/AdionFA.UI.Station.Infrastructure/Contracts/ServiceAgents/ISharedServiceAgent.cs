using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface ISharedServiceAgent
    {
        #region ServiceHost

        public Task<EntityServiceHostVM> GetEntityServiceHost(int entityTypeId, int entityId);

        #endregion ServiceHost

        // Setting

        Task<IList<SettingVM>> GetAllAppSetting();
        SettingVM GetSetting(int settingId, string keySetting = null);
        Task<SettingVM> GetSettingAsync(int settingId, string keySetting = null);
        Task<ResponseVM> CreateAppSetting(SettingVM setting);
        Task<ResponseVM> UpdateAppSetting(SettingVM setting);
    }
}
