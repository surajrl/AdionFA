using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Services;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.AssembledBuilder
{
    public class AssembledBuilderModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAssembledBuilderService>().To<AssembledBuilderService>();
        }
    }
}
