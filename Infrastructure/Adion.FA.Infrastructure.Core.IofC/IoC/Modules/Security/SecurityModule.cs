using Adion.FA.Core.API.Contracts.Security;
using Adion.FA.Core.API.Security;
using Adion.FA.Core.Application.Contracts.Security;
using Adion.FA.Core.Application.Services.Security;
using Adion.FA.Core.Domain.Contracts.Security;
using Adion.FA.Core.Domain.Services.Security;
using Adion.FA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Security
{
    public class SecurityModule : NinjectModule
    {
        public override void Load()
        {
            #region API
            Kernel.Bind(typeof(ISecurityAPI)).To(typeof(SecurityAPI));
            #endregion

            #region Application
            Kernel.Bind(typeof(ISecurityAppService)).To(typeof(SecurityAppService));
            #endregion

            #region Domain
            Kernel.Bind(typeof(ISecurityDomainService)).To(typeof(SecurityDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
            #endregion
        }
    }
}
