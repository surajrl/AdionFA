using AdionFA.Core.API.Contracts.Security;
using AdionFA.Core.Application.Contracts.Security;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.Data.Persistence.EFCore;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Core;

namespace AdionFA.Core.API.Security
{
    public class SecurityAPI : ISecurityAPI
    {
        #region User
        
        public CoreUserDTO GetUserByUserName(string username)
        {
            using (var service = IoC.Get<ISecurityAppService>())
                return service.GetUserByUserName(username);
        }

        public ResponseDTO CreateUser(CoreUserDTO user)
        {
            using (var service = IoC.Get<ISecurityAppService>())
            using (var t = service.Transaction<AdionSecurityDbContext>())
            {
                return service.CreateUser(user);
            }
        }

        #endregion
    }
}
