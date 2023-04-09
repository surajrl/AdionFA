using AdionFA.Core.API.Contracts.Security;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.TransferObject.Core;
using AdionFA.UI.Station.Infrastructure.AutoMapper;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Core;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Services.AppServices
{
    public class SecurityServiceAgent : ISecurityServiceAgent
    {
        private readonly IMapper Mapper;

        public SecurityServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        public async Task<CoreUserVM> GetUserByUserName(string username)
        {
            try
            {
                CoreUserVM result = null;

                await Task.Run(() =>
                {
                    var api = IoC.Get<ISecurityAPI>();
                    var dto = api.GetUserByUserName(username);
                    result = Mapper.Map<CoreUserDTO, CoreUserVM>(dto);
                });

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
