using AdionFA.Core.API.Contracts.MarketData;
using AdionFA.Core.API.MarketData;
using AdionFA.Core.Application.Contracts.MarketData;
using AdionFA.Core.Application.Services.Markets;
using AdionFA.Core.Domain.Contracts.MarketData;
using AdionFA.Core.Domain.Services.MarketData;
using AdionFA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Markets
{
    public class HistoricalDataModule : NinjectModule
    {
        public override void Load()
        {
            // API

            Kernel.Bind(typeof(IMarketDataAPI)).To(typeof(MarketDataAPI));

            // Application

            Kernel.Bind(typeof(IMarketDataAppService)).To(typeof(MarketDataAppService));

            // Domain

            Kernel.Bind(typeof(IMarketDataDomainService)).To(typeof(MarketDataDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
        }
    }
}
