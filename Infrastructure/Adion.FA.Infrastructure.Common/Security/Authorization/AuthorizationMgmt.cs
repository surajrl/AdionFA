using Adion.FA.Infrastructure.Common.Extensions;
using Adion.FA.Infrastructure.Common.Security.Attributes;
using System;
using System.Linq;

namespace Adion.FA.Infrastructure.Common.Security.Authorization
{
    public static class AuthorizationMgmt
    {
        public static Type AuthorizeCall(string tenantId, string ownerId, string owner, string sourceFilePath)
        {
            bool authorize = false;

            Type type = sourceFilePath.GetAssembleType() ?? throw new Exception("Invalid Member Call");

            if (type.GetCustomAttributes(typeof(AdionAnonymousAttribute), false).Any())
                authorize = true;

            if (!authorize && (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(ownerId) || string.IsNullOrEmpty(owner)))
                throw new Exception("Invalid Tenant or Owner");

            return type;
        }
    }
}
