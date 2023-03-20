using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Module.Shell.Services;
using Adion.FA.UI.Station.Module.Shell.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Adion.FA.UI.Station.Module.Shell
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
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellProjectGlobalconfigFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellMarketDataFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellUploadMarketDataFlyout));
            regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ShellTraderFlyout));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IShellServiceShell, ShellServiceShell>();
        }
    }
}
