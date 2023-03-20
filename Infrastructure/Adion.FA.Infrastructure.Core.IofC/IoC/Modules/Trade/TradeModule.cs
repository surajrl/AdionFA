using Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts;
using Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Services;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Trade
{
    public class TradeModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ITradeService>().To<TradeService>();
        }
    }
}
