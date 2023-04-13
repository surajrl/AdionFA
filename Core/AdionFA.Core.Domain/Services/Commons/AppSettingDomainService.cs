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
        public IRepository<Setting> SettingRepository { get; set; }

        public AppSettingDomainService(string tenantId, string ownerId, string owner,
            IRepository<Setting> settingRepository) : base(tenantId, ownerId, owner)
        {
            SettingRepository = settingRepository;
        }

        // Setting

        public IList<Setting> GetAllAppSetting()
        {
            try
            {
                return SettingRepository.GetAll().ToList();
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
                Setting setting = SettingRepository.FirstOrDefault(
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
                SettingRepository.Create(setting);

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
                SettingRepository.Update(setting);
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
