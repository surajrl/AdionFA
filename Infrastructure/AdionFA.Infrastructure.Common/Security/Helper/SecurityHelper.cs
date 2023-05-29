using AdionFA.Infrastructure.Common.Attributes;
using AdionFA.Infrastructure.Common.Extensions;
using AdionFA.Infrastructure.Common.Security.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace AdionFA.Infrastructure.Common.Security.Helper
{
    public static class SecurityHelper
    {
        private static AdionPrincipal Principal => Thread.CurrentPrincipal as AdionPrincipal;

        public static AdionIdentity Identity => Principal?.Identity;

        public static string DefaultOwnerId = "0000";
        public static string DefaultOwner = "admin";

        public static Dictionary<string, string> IdentityToArguments()
        {
            var arguments = new Dictionary<string, string>();

            List<PropertyInfo> properties = typeof(AdionIdentity).GetFilteredProperties<IoCArgumentAttribute>(true).ToList();
            properties.ForEach(p =>
            {
                var pValue = p.GetValue(Identity);
                if (pValue != null)
                    arguments.Add(p.Name.ToLower(), pValue?.ToString());
            });

            return arguments;
        }
    }
}
