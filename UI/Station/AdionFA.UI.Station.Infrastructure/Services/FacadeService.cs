using AdionFA.Core.API.Contracts.Commons;
using AdionFA.Core.API.Contracts.MarketData;
using AdionFA.Core.API.Contracts.Projects;

using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.IofC;

using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;

using Prism.Ioc;

namespace AdionFA.UI.Station.Infrastructure.Services
{
    public static class FacadeService
    {
        // API

        public static IMarketDataAPI MarketDataAPI => IoC.Get<IMarketDataAPI>();
        public static IProjectAPI ProjectAPI => IoC.Get<IProjectAPI>();
        public static ISharedAPI SharedAPI => IoC.Get<ISharedAPI>();
        public static IProjectDirectoryService DirectoryService => IoC.Get<IProjectDirectoryService>();

        // Service Agent

        public static ISharedServiceAgent SharedServiceAgent => ContainerLocator.Current.Resolve<ISharedServiceAgent>();
        public static IProjectServiceAgent ProjectServiceAgent => ContainerLocator.Current.Resolve<IProjectServiceAgent>();
        public static IMarketDataServiceAgent MarketDataServiceAgent => ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
    }
}
