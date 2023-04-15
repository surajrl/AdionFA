using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Services;
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
