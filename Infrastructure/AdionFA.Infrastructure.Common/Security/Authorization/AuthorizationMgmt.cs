using AdionFA.Infrastructure.Common.Extensions;
using AdionFA.Infrastructure.Common.Security.Attributes;
using System;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Security.Authorization
{
    public static class AuthorizationMgmt
    {
        public static Type AuthorizeCall(string ownerId, string owner, string sourceFilePath)
        {
            bool authorize = false;

            Type type = sourceFilePath.GetAssembleType() ?? throw new Exception("Invalid Member Call");

            if (type.GetCustomAttributes(typeof(AdionAnonymousAttribute), false).Any())
                authorize = true;

            if (!authorize && (string.IsNullOrEmpty(ownerId) || string.IsNullOrEmpty(owner)))
                throw new Exception("Invalid Owner");

            return type;
        }
    }
}
