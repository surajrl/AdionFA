using AdionFA.TransferObject.Common;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.AutoMapper;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AdionFA.Infrastructure.Common.IofC;
using System.Collections.Generic;
using System.Linq;
using AdionFA.TransferObject.Base;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.Core.API.Contracts.Commons;

namespace AdionFA.UI.Station.Infrastructure.Services.AppServices
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

        public async Task<EntityServiceHostVM> GetEntityServiceHost(int entityTypeId, int entityId)
        {
            try
            {
                EntityServiceHostVM vm = null;

                await Task.Run(() =>
                {
                    EntityServiceHostDTO dto = IoC.Get<ISharedAPI>().GetEntityServiceHost(entityTypeId, entityId);

                    vm = Mapper.Map<EntityServiceHostDTO, EntityServiceHostVM>(dto);
                });

                return vm;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<IList<SettingVM>> GetAllAppSetting()
        {
            try
            {
                IList<SettingDTO> all = Array.Empty<SettingDTO>().ToList();

                await Task.Run(() =>
                {
                    all = IoC.Get<ISharedAPI>().GetAllAppSetting();
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

                var dto = IoC.Get<ISharedAPI>().GetSetting(settingId, keySetting);

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
                    var dto = IoC.Get<ISharedAPI>().GetSetting(settingId, keySetting);
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
                    SettingDTO dto = Mapper.Map<SettingVM, SettingDTO>(setting);

                    result = IoC.Get<ISharedAPI>().CreateAppSetting(dto);
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
                    SettingDTO dto = Mapper.Map<SettingVM, SettingDTO>(setting);
                    result = IoC.Get<ISharedAPI>().UpdateAppSetting(dto);
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
