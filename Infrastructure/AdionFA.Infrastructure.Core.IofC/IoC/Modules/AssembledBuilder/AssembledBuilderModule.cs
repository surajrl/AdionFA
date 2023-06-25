using AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Services;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.AssemblyBuilder
{
    public class AssemblyBuilderModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAssemblyBuilderService>().To<AssemblyBuilderService>();
        }
    }
}
