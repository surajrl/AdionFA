using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Services;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.StrategyBuilder
{
    public class StrategyBuilderModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IStrategyBuilderService>().To<StrategyBuilderService>();
        }
    }
}
