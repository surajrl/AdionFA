using AdionFA.API.Contracts;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;

using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using Ninject;
using Prism.Ioc;

namespace AdionFA.UI.Station.Infrastructure.Services
{
    public static class FacadeService
    {
        // API

        public static IMarketDataAPI MarketDataAPI => IoC.Kernel.Get<IMarketDataAPI>();
        public static IProjectAPI ProjectAPI => IoC.Kernel.Get<IProjectAPI>();
        public static ISharedAPI SharedAPI => IoC.Kernel.Get<ISharedAPI>();
        public static IProjectDirectoryService DirectoryService => IoC.Kernel.Get<IProjectDirectoryService>();

        // Service Agent

        public static ISharedServiceAgent SharedServiceAgent => ContainerLocator.Current.Resolve<ISharedServiceAgent>();
        public static IProjectServiceAgent ProjectServiceAgent => ContainerLocator.Current.Resolve<IProjectServiceAgent>();
        public static IMarketDataServiceAgent MarketDataServiceAgent => ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
    }
}
