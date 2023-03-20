using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface ISharedServiceAgent
    {
        #region ServiceHost
        public Task<EntityServiceHostVM> GetEntityServiceHost(int entityTypeId, int entityId);
        #endregion

        #region Setting
        Task<IList<SettingVM>> GetAllAppSetting();
        SettingVM GetSetting(int settingId, string keySetting = null);
        Task<SettingVM> GetSettingAsync(int settingId, string keySetting = null);
        Task<ResponseVM> CreateAppSetting(SettingVM setting);
        Task<ResponseVM> UpdateAppSetting(SettingVM setting);
        #endregion
    }
}
