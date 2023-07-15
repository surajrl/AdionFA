using AdionFA.API.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using AdionFA.UI.Station.Infrastructure.AutoMapper;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AutoMapper;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Infrastructure.Services.AppServices
{
    public class SharedServiceAgent : ISharedServiceAgent
    {
        public readonly IMapper Mapper;

        public SharedServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        public async Task<IList<SettingVM>> GetAllAppSetting()
        {
            try
            {
                IList<SettingDTO> all = Array.Empty<SettingDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Kernel.Get<ISharedAPI>().GetAllAppSetting();
                });

                return Mapper.Map<IList<SettingDTO>, IList<SettingVM>>(all);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public SettingVM GetSetting(int settingId, string keySetting = null)
        {
            try
            {
                SettingVM vm = null;

                var dto = IoC.Kernel.Get<ISharedAPI>().GetSetting(settingId, keySetting);

                vm = Mapper.Map<SettingDTO, SettingVM>(dto);

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<SettingVM> GetSettingAsync(int settingId, string keySetting = null)
        {
            try
            {
                SettingVM vm = null;

                await Task.Run(() =>
                {
                    var dto = IoC.Kernel.Get<ISharedAPI>().GetSetting(settingId, keySetting);
                    vm = Mapper.Map<SettingDTO, SettingVM>(dto);
                });

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> CreateAppSetting(SettingVM setting)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    var dto = Mapper.Map<SettingVM, SettingDTO>(setting);

                    result = IoC.Kernel.Get<ISharedAPI>().CreateAppSetting(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result); ;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateAppSetting(SettingVM setting)
        {
            try
            {
                var result = new ResponseDTO { IsSuccess = false };

                await Task.Run(() =>
                {
                    var dto = Mapper.Map<SettingVM, SettingDTO>(setting);
                    result = IoC.Kernel.Get<ISharedAPI>().UpdateAppSetting(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(result); ;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
