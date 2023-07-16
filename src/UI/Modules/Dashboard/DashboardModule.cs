using AdionFA.UI.Infrastructure;
using AdionFA.UI.Module.Dashboard.Services;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AdionFA.UI.Module.Dashboard
{
    public class DashboardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(FlyoutRegions.AppSettingRegion, typeof(AppSettingView));
            regionManager.RegisterViewWithRegion(AppRegions.CreateProjectView_SettingModule, typeof(CreateProjectView));
            regionManager.RegisterViewWithRegion(AppRegions.ConfigurationView_SettingModule, typeof(ConfigurationView));
            regionManager.RegisterViewWithRegion(AppRegions.HistoricalDataView_SettingModule, typeof(HistoricalDataView));
            regionManager.RegisterViewWithRegion(AppRegions.UploadHistoricalDataView_SettingModule, typeof(UploadHistoricalDataView));
            regionManager.RegisterViewWithRegion(AppRegions.DownloaHistoricalDataView_SettingModule, typeof(DownloadHistoricalDataView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISettingService, SettingService>();
        }
    }
}
