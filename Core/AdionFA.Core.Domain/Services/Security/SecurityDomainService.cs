using AdionFA.Core.Domain.Aggregates.Core;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Core.Domain.Contracts.Security;
using System;
using System.Diagnostics;

namespace AdionFA.Core.Domain.Services.Security
{
    public class SecurityDomainService : DomainServiceBase, ISecurityDomainService
    {
        #region Repositories
        public ISecurityRepository<CoreUser> UserRepository;
        #endregion

        #region Ctor
        public SecurityDomainService(string tenantId, string ownerId, string owner,
            ISecurityRepository<CoreUser> userRepository) : base(tenantId, ownerId, owner)
        {
            UserRepository = userRepository;
        }
        #endregion

        #region User
        public CoreUser GetUserByUserName(string username)
        {
            try
            {
                CoreUser user = UserRepository.FirstOrDefault(_u => _u.UserName == username);
                return user;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public string CreateUser(CoreUser user)
        {
            try
            {
                UserRepository.Create(user);
                return user.UserId;
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
