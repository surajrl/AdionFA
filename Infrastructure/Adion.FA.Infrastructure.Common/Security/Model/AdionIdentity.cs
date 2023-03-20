using Adion.FA.Infrastructure.Common.Attributes;
using System.Security.Claims;

namespace Adion.FA.Infrastructure.Common.Security.Model
{
    public class AdionIdentity : ClaimsIdentity
    {
        [IoCArgument]
        public string TenantId { get; }

        [IoCArgument]
        public string OwnerId { get; }

        [IoCArgument]
        public string Owner { get; }

        #region Ctor
        public AdionIdentity()
        {
        }

        public AdionIdentity(string tenantId, string ownerId, string owner)
        {
            TenantId = tenantId;
            OwnerId = ownerId;
            Owner = owner;
        }
        #endregion
    }

    public class AnonymousIdentity : AdionIdentity
    {
        public AnonymousIdentity() : base()
        {
        }
    }
}
