using System.Security.Claims;

namespace AdionFA.Infrastructure.Common.Security.Model
{
    public class AdionPrincipal : ClaimsPrincipal
    {
        private AdionIdentity _identity;

        public new AdionIdentity Identity
        {
            get { return _identity ?? new AnonymousIdentity(); }
            set { _identity = value; }
        }
    }
}
