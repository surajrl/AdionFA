namespace AdionFA.Infrastructure.IofC
{
    public static class IoCExt
    {
        public static void Setup(this IoC ioC)
        {
            IoC.Load(new ModuleBase());
        }
    }
}
