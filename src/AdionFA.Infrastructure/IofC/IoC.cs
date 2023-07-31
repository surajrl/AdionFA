using Ninject;
using Ninject.Modules;

namespace AdionFA.Infrastructure.IofC
{
    public sealed class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static void Load(NinjectModule module)
        {
            Kernel.Load(module);
        }
    }
}
