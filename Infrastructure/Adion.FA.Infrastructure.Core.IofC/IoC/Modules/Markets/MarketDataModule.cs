using Adion.FA.Core.API.Contracts.Markets;
using Adion.FA.Core.API.Markets;
using Adion.FA.Core.Application.Contracts.Markets;
using Adion.FA.Core.Application.Services.Markets;
using Adion.FA.Core.Domain.Contracts.Markets;
using Adion.FA.Core.Domain.Services.Markets;
using Adion.FA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Markets
{
    public class MarketDataModule : NinjectModule
    {
        public override void Load()
        {
            #region API
            Kernel.Bind(typeof(IMarketDataAPI)).To(typeof(MarketDataAPI));
            #endregion

            #region Application
            Kernel.Bind(typeof(IMarketDataAppService)).To(typeof(MarketDataAppService));
            #endregion

            #region Domain
            Kernel.Bind(typeof(IMarketDataDomainService)).To(typeof(MarketDataDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
            #endregion
        }
    }
}
