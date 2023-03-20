using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Contracts;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Services;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.AssembledBuilder
{
    public class AssembledBuilderModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAssembledBuilderService>().To<AssembledBuilderService>();
        }
    }
}
