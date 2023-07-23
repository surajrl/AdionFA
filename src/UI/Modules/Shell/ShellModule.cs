using AdionFA.UI.Infrastructure;
using AdionFA.UI.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AdionFA.UI.Module
{
    public class ShellModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager?.RegisterViewWithRegion(AppRegions.ShellModule, typeof(ShellView));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.RightWindowCommandsRegion, typeof(ShellAppSettingWindowCommands));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellAppSettingFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellCreateProjectFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellGlobalConfigurationFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellHistoricalDataFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellUploadHistoricalDataFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellDownloadHistoricalDataFlyout));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // ...
        }
    }
}
