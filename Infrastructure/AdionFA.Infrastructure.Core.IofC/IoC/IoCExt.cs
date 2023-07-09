using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.IofC;

namespace AdionFA.Infrastructure.Core.IofCExt
{
    public static class IoCExt
    {
        public static void Setup(this IoC ioC)
        {
            IoC.Load(new ModuleBase());
        }
    }
}
