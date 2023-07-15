using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Module.Shell.Services;
using AdionFA.UI.Station.Module.Shell.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AdionFA.UI.Station.Module.Shell
{
    public class ShellModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager?.RegisterViewWithRegion(AppRegions.ShellModule, typeof(ShellView));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.RightWindowCommandsRegion, typeof(ShellAppSettingWindowCommands));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellAppSettingFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellCreateProjectFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellConfigurationFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellHistoricalDataFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellUploadHistoricalDataFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellDownloadHistoricalDataFlyout));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IShellServiceShell, ShellServiceShell>();
        }
    }
}
