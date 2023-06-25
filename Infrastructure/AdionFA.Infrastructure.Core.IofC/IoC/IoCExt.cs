using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.IofC;
using AdionFA.Infrastructure.Core.IofC.Modules.AssemblyBuilder;
using AdionFA.Infrastructure.Core.IofC.Modules.Commons;
using AdionFA.Infrastructure.Core.IofC.Modules.Directories;
using AdionFA.Infrastructure.Core.IofC.Modules.Extractor;
using AdionFA.Infrastructure.Core.IofC.Modules.Markets;
using AdionFA.Infrastructure.Core.IofC.Modules.Projects;
using AdionFA.Infrastructure.Core.IofC.Modules.StrategyBuilder;
using AdionFA.Infrastructure.Core.IofC.Modules.Trade;

namespace AdionFA.Infrastructure.Core.IofCExt
{
    public static class IoCExt
    {
        public static void Setup(this IoC ioC)
        {
            ioC.Load(new ModuleBase());
            ioC.Load(new CommonModule());
            ioC.Load(new DirectoryModule());
            ioC.Load(new ExtractorModule());
            ioC.Load(new StrategyBuilderModule());
            ioC.Load(new AssemblyBuilderModule());
            ioC.Load(new HistoricalDataModule());
            ioC.Load(new ProjectModule());
            ioC.Load(new TradeModule());
        }
    }
}
