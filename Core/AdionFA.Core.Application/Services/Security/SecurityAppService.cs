using AdionFA.Core.Domain.Aggregates.Core;
using AdionFA.Core.Domain.Contracts.Security;
using AdionFA.Core.Application.Contracts.Security;

using AdionFA.Infrastructure.Common.Security.Attributes;

using AdionFA.TransferObject.Core;
using AdionFA.TransferObject.Base;

using Ninject;

using System;
using System.Diagnostics;

namespace AdionFA.Core.Application.Services.Security
{
    [AdionAnonymous]
    public class SecurityAppService : AppServiceBase, ISecurityAppService
    {
        #region Domain Services

        [Inject]
        public ISecurityDomainService SecurityDomainService { get; set; }

        #endregion Domain Services

        #region Ctor

        public SecurityAppService() : base()
        {
        }

        #endregion Ctor

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

        #endregion User
    }
}
