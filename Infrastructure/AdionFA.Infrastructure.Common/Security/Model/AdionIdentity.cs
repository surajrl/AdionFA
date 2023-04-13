using AdionFA.Infrastructure.Common.Attributes;
using System.Security.Claims;

namespace AdionFA.Infrastructure.Common.Security.Model
{
    public class AdionIdentity : ClaimsIdentity
    {
        [IoCArgument]
        public string TenantId { get; }

        [IoCArgument]
        public string OwnerId { get; }

        [IoCArgument]
        public string Owner { get; }

        public AdionIdentity()
        {
        }

        public AdionIdentity(string tenantId, string ownerId, string owner)
        {
            TenantId = tenantId;
            OwnerId = ownerId;
            Owner = owner;
        }
    }

    public class AnonymousIdentity : AdionIdentity
    {
        public AnonymousIdentity() : base()
        {
        }
    }
}
