using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Contracts.Commons;
using AdionFA.Core.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdionFA.Core.Domain.Services.Commons
{
    public class AppSettingDomainService : DomainServiceBase, IAppSettingDomainService
    {
        private readonly IRepository<Setting> _settingRepository;

        public AppSettingDomainService(string ownerId, string owner,
            IRepository<Setting> settingRepository)
            : base(ownerId, owner)
        {
            _settingRepository = settingRepository;
        }

        // App Settings

        public IList<Setting> GetAllAppSetting()
        {
            try
            {
                return _settingRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public Setting GetSetting(int settingId, string keySetting = null)
        {
            try
            {
                Setting setting = _settingRepository.FirstOrDefault(
                    s => s.SettingId == settingId && (keySetting == null || s.Code == keySetting)
                );

                return setting;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public int CreateAppSetting(Setting setting)
        {
            try
            {
                _settingRepository.Create(setting);

                return setting.SettingId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool UpdateAppSetting(Setting setting)
        {
            try
            {
                _settingRepository.Update(setting);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
