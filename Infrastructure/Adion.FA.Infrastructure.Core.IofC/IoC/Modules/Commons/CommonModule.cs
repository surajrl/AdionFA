using Adion.FA.Core.API.Commons;
using Adion.FA.Core.API.Contracts.Commons;
using Adion.FA.Core.Application.Contracts.Commons;
using Adion.FA.Core.Application.Services.Commons;
using Adion.FA.Core.Domain.Contracts.Commons;
using Adion.FA.Core.Domain.Services.Commons;
using Adion.FA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Commons
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            #region API
            Kernel.Bind(typeof(ISharedAPI)).To(typeof(SharedAPI));
            #endregion

            #region Application
            Kernel.Bind(typeof(ISharedAppService)).To(typeof(SharedAppService));
            Kernel.Bind(typeof(IAppSettingAppService)).To(typeof(AppSettingAppService));
            #endregion

            #region Domain
            Kernel.Bind(typeof(IAppSettingDomainService)).To(typeof(AppSettingDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
            #endregion
        }
    }
}
