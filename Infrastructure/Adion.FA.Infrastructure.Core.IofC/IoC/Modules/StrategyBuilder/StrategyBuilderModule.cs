using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Contracts;
using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Services;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.StrategyBuilder
{
    public class StrategyBuilderModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IStrategyBuilderService>().To<StrategyBuilderService>();
        }
    }
}
