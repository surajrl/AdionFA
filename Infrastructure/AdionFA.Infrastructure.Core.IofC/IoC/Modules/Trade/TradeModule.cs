using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Services;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Trade
{
    public class TradeModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ITradeService>().To<TradeService>();
        }
    }
}
