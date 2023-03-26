using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Directories
{
    public class DirectoryModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IProjectDirectoryService>().To<ProjectDirectoryService>();
        }
    }
}
