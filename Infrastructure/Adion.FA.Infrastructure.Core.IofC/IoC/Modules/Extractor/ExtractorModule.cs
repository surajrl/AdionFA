using Adion.FA.Infrastructure.Common.Extractor.Contracts;
using Adion.FA.Infrastructure.Common.Extractor.Services;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Extractor
{
    public class ExtractorModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IExtractorService>().To<ExtractorService>();
        }
    }
}
