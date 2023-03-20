using Adion.FA.Infrastructure.Common.Attributes;
using Adion.FA.Infrastructure.Common.Extensions;
using Adion.FA.Infrastructure.Common.Security.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Adion.FA.Infrastructure.Common.Security.Helper
{
    public static class SecurityHelper
    {
        private static AdionPrincipal Principal => Thread.CurrentPrincipal as AdionPrincipal;

        public static AdionIdentity Identity => Principal?.Identity;

        public static string DefaultTenantId = "22222222-2222-2222-2222-222222222222";
        public static string DefaultOwnerId = "11111111-1111-1111-11111111111111111";
        public static string DefaultOwner = "sysadmin";

        public static Dictionary<string, string> IdentityToArguments()
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            List<PropertyInfo> properties = typeof(AdionIdentity).GetFilteredProperties<IoCArgumentAttribute>(true).ToList();
            properties.ForEach(p =>
            {
                var pValue = p.GetValue(Identity);
                if(pValue != null)
                    arguments.Add(p.Name.ToLower(), pValue?.ToString());    
            });

            return arguments;
        }
    }
}
