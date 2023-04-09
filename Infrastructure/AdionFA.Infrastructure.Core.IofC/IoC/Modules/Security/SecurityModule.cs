using AdionFA.Core.API.Contracts.Security;
using AdionFA.Core.API.Security;
using AdionFA.Core.Application.Services.Security;
using AdionFA.Core.Application.Contracts.Security;
using AdionFA.Core.Domain.Contracts.Security;
using AdionFA.Core.Domain.Services.Security;
using AdionFA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Security
{
    public class SecurityModule : NinjectModule
    {
        public override void Load()
        {
            #region API
            Kernel.Bind(typeof(ISecurityAPI)).To(typeof(SecurityAPI));
            #endregion API

            #region Application
            Kernel.Bind(typeof(ISecurityAppService)).To(typeof(SecurityAppService));
            #endregion Application

            #region Domain
            Kernel.Bind(typeof(ISecurityDomainService)).To(typeof(SecurityDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
            #endregion Domain
        }
    }
}
