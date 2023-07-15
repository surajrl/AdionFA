using AdionFA.API.Contracts;
using AdionFA.Application.Contract.Commons;
using AdionFA.Application.Contracts.Commons;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Persistance.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using Ninject;
using System.Collections.Generic;

namespace AdionFA.API
{
    public class SharedAPI : ISharedAPI
    {
        // App Settings

        public IList<SettingDTO> GetAllAppSetting()
        {
            using var service = IoC.Kernel.Get<IAppSettingAppService>();
            return service.GetAllAppSetting();
        }

        public SettingDTO GetSetting(int settingId, string keySetting = null)
        {
            using var service = IoC.Kernel.Get<IAppSettingAppService>();
            return service.GetSetting(settingId, keySetting);
        }

        public ResponseDTO CreateAppSetting(SettingDTO settingDTO)
        {
            using (var service = IoC.Kernel.Get<IAppSettingAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateAppSetting(settingDTO);
            }
        }

        public ResponseDTO UpdateAppSetting(SettingDTO settingDTO)
        {
            using (var service = IoC.Kernel.Get<IAppSettingAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateAppSetting(settingDTO);
            }
        }

        // Configuration

        public IList<ConfigurationDTO> GetAllConfiguration(bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IConfigurationAppService>();
            return service.GetAllConfiguration(includeGraph);
        }

        public ConfigurationDTO GetConfiguration(int? configurationId = null, bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IConfigurationAppService>();
            return service.GetConfiguration(configurationId, includeGraph);
        }

        public ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDTO)
        {
            using (var service = IoC.Kernel.Get<IConfigurationAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateConfiguration(configurationDTO);
            }
        }
    }
}
