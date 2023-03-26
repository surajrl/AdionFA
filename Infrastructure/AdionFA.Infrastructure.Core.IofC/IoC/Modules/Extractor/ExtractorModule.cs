using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Services;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Extractor
{
    public class ExtractorModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IExtractorService>().To<ExtractorService>();
        }
    }
}
