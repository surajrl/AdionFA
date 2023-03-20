using Adion.FA.Infrastructure.Core.IofC.Modules.Security;
using Adion.FA.Infrastructure.Core.IofC.Modules.Projects;
using Adion.FA.Infrastructure.Core.IofC.Modules.Commons;
using Adion.FA.Infrastructure.Core.IofC.Modules.Markets;
using Adion.FA.Infrastructure.Core.IofC.Modules.Extractor;
using Adion.FA.Infrastructure.Core.IofC.Modules.Directories;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.IofC;
using Adion.FA.Infrastructure.Core.IofC.Modules.StrategyBuilder;
using Adion.FA.Infrastructure.Core.IofC.Modules.AssembledBuilder;
using Adion.FA.Infrastructure.Core.IofC.Modules.Trade;

namespace Adion.FA.Infrastructure.Core.IofCExt
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
            ioC.Load(new AssembledBuilderModule());
            ioC.Load(new MarketDataModule());
            ioC.Load(new ProjectModule());
            ioC.Load(new SecurityModule());
            ioC.Load(new TradeModule());
        }
    }
}
