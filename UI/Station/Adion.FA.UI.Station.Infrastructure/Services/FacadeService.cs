using Adion.FA.Core.API.Contracts.Commons;
using Adion.FA.Core.API.Contracts.Markets;
using Adion.FA.Core.API.Contracts.Projects;
using Adion.FA.Core.API.Contracts.Security;
using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Prism.Ioc;

namespace Adion.FA.UI.Station.Infrastructure.Services
{
    public static class FacadeService
    {
        #region API

        public static IMarketDataAPI MarketDataAPI => IoC.Get<IMarketDataAPI>();
        public static IProjectAPI ProjectAPI => IoC.Get<IProjectAPI>();
        public static ISecurityAPI SecurityAPI => IoC.Get<ISecurityAPI>();
        public static ISharedAPI SharedAPI => IoC.Get<ISharedAPI>();
        public static IProjectDirectoryService DirectoryService => IoC.Get<IProjectDirectoryService>();

        #endregion

        #region Service Agent

        public static ISharedServiceAgent SharedServiceAgent => ContainerLocator.Current.Resolve<ISharedServiceAgent>();
        public static IProjectServiceAgent ProjectServiceAgent => ContainerLocator.Current.Resolve<IProjectServiceAgent>();
        public static IMarketDataServiceAgent MarketDataServiceAgent => ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
        public static ISecurityServiceAgent SecurityServiceAgent => ContainerLocator.Current.Resolve<ISecurityServiceAgent>();

        #endregion
    }
}
