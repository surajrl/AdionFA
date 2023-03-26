using AdionFA.Core.Application.Contracts.Security;
using AdionFA.Core.Domain.Aggregates.Core;
using AdionFA.Core.Domain.Contracts.Security;
using AdionFA.Infrastructure.Common.Security.Attributes;
using Ninject;
using System;
using System.Diagnostics;
using AdionFA.TransferObject.Core;
using AdionFA.TransferObject.Base;

namespace AdionFA.Core.Application.Services.Security
{
    [AdionAnonymous]
    public class SecurityAppService : AppServiceBase, ISecurityAppService
    {
        #region Domain Services

        [Inject]
        public ISecurityDomainService SecurityDomainService { get; set; }

        #endregion

        #region Ctor

        public SecurityAppService() : base()
        {
        }

        #endregion

        #region User

        public CoreUserDTO GetUserByUserName(string username)
        {
            try
            {
                CoreUser entity = SecurityDomainService.GetUserByUserName(username);
                CoreUserDTO user = Mapper.Map<CoreUserDTO>(entity);
                return user;
            }
            catch (Exception ex)
            {
                LogException<SecurityAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO CreateUser(CoreUserDTO user)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                CoreUser entity = Mapper.Map<CoreUser>(user);
                
                response.IsSuccess = SecurityDomainService.CreateUser(entity) != null;

                if (response.IsSuccess)
                {
                    LogInfoCreate<CoreUser>();
                }

                return response;
            }
            catch (Exception ex)
            {
                LogException<SecurityAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
