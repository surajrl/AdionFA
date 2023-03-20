using Adion.FA.Core.API.Contracts.Security;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.TransferObject.Core;
using Adion.FA.UI.Station.Infrastructure.AutoMapper;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Core;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Infrastructure.Services.AppServices
{
    public class SecurityServiceAgent : ISecurityServiceAgent
    {
        #region Mapper
        private readonly IMapper Mapper;
        #endregion

        #region Ctor
        public SecurityServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }
        #endregion

        #region User
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
        #endregion
    }
}
