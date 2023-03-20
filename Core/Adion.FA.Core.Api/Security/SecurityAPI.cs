using Adion.FA.Core.API.Contracts.Security;
using Adion.FA.Core.Application.Contracts.Security;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.Data.Persistence.EFCore;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Core;

namespace Adion.FA.Core.API.Security
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
