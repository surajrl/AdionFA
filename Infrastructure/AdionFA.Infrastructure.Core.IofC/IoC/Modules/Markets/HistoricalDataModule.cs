using AdionFA.Core.API.Contracts.Markets;
using AdionFA.Core.API.Markets;
using AdionFA.Core.Application.Contracts.Markets;
using AdionFA.Core.Application.Services.Markets;
using AdionFA.Core.Domain.Contracts.Markets;
using AdionFA.Core.Domain.Services.Markets;
using AdionFA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Markets
{
    public class HistoricalDataModule : NinjectModule
    {
        public override void Load()
        {
            #region API

            Kernel.Bind(typeof(IHistoricalDataAPI)).To(typeof(HistoricalDataAPI));

            #endregion API

            #region Application

            Kernel.Bind(typeof(IHistoricalDataAppService)).To(typeof(HistoricalDataAppService));

            #endregion Application

            #region Domain

            Kernel.Bind(typeof(IHistoricalDataDomainService)).To(typeof(HistoricalDataDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            #endregion Domain
        }
    }
}