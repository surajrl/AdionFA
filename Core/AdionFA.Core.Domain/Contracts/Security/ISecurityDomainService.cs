using AdionFA.Core.Domain.Aggregates.Core;

namespace AdionFA.Core.Domain.Contracts.Security
{
    public interface ISecurityDomainService
    {
        #region User
        CoreUser GetUserByUserName(string username);
        string CreateUser(CoreUser user);
        #endregion
    }
}
