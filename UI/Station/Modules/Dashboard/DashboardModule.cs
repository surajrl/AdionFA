using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Module.Dashboard.Services;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AdionFA.UI.Station.Module.Dashboard
{
    public class DashboardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(AppRegions.CreateProjectView_SettingModule, typeof(CreateProjectView));
            regionManager.RegisterViewWithRegion(AppRegions.ProjectGlobalConfigView_SettingModule, typeof(ProjectGlobalConfigView));
            regionManager.RegisterViewWithRegion(AppRegions.HistoricalDataView_SettingModule, typeof(HistoricalDataView));
            regionManager.RegisterViewWithRegion(AppRegions.HistoricalDataMTView_SettingModule, typeof(HistoricalDataView));
            regionManager.RegisterViewWithRegion(AppRegions.UploadHistoricalDataView_SettingModule, typeof(UploadHistoricalDataView));
            regionManager.RegisterViewWithRegion(AppRegions.DownloaHistoricalDataView_SettingModule, typeof(DownloadHistoricalDataView));
            regionManager.RegisterViewWithRegion(FlyoutRegions.AppSettingRegion, typeof(AppSettingView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISettingService, SettingService>();
            containerRegistry.RegisterSingleton<IMetaTraderService, MetaTraderService>();
        }
    }
}
