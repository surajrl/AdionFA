using Adion.FA.Core.Domain.Aggregates.Core;

namespace Adion.FA.Core.Domain.Contracts.Security
{
    public interface ISecurityDomainService
    {
        #region User
        CoreUser GetUserByUserName(string username);
        string CreateUser(CoreUser user);
        #endregion
    }
}
