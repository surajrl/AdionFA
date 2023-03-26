using AdionFA.Core.API.Contracts.Commons;
using AdionFA.Core.API.Contracts.Markets;
using AdionFA.Core.API.Contracts.Projects;
using AdionFA.Core.API.Contracts.Security;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using Prism.Ioc;

namespace AdionFA.UI.Station.Infrastructure.Services
{
    public static class FacadeService
    {
        #region API

        public static IHistoricalDataAPI MarketDataAPI => IoC.Get<IHistoricalDataAPI>();
        public static IProjectAPI ProjectAPI => IoC.Get<IProjectAPI>();
        public static ISecurityAPI SecurityAPI => IoC.Get<ISecurityAPI>();
        public static ISharedAPI SharedAPI => IoC.Get<ISharedAPI>();
        public static IProjectDirectoryService DirectoryService => IoC.Get<IProjectDirectoryService>();

        #endregion

        #region Service Agent

        public static ISharedServiceAgent SharedServiceAgent => ContainerLocator.Current.Resolve<ISharedServiceAgent>();
        public static IProjectServiceAgent ProjectServiceAgent => ContainerLocator.Current.Resolve<IProjectServiceAgent>();
        public static IHistoricalDataServiceAgent MarketDataServiceAgent => ContainerLocator.Current.Resolve<IHistoricalDataServiceAgent>();
        public static ISecurityServiceAgent SecurityServiceAgent => ContainerLocator.Current.Resolve<ISecurityServiceAgent>();

        #endregion
    }
}
