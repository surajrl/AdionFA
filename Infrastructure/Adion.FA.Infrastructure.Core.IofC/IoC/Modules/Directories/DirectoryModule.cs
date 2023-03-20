using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Directories.Services;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Directories
{
    public class DirectoryModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IProjectDirectoryService>().To<ProjectDirectoryService>();
        }
    }
}
