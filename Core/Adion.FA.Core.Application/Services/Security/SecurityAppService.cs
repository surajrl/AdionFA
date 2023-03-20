using Adion.FA.Core.Application.Contracts.Security;
using Adion.FA.Core.Domain.Aggregates.Core;
using Adion.FA.Core.Domain.Contracts.Security;
using Adion.FA.Infrastructure.Common.Security.Attributes;
using Ninject;
using System;
using System.Diagnostics;
using Adion.FA.TransferObject.Core;
using Adion.FA.TransferObject.Base;

namespace Adion.FA.Core.Application.Services.Security
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
