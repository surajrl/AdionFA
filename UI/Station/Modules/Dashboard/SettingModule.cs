using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Module.Dashboard.Services;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Adion.FA.UI.Station.Module.Dashboard
{
    public class SettingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(AppRegions.CreateProjectView_SettingModule, typeof(CreateProjectView));
            regionManager.RegisterViewWithRegion(AppRegions.ProjectGlobalConfigView_SettingModule, typeof(ProjectGlobalConfigView));
            regionManager.RegisterViewWithRegion(AppRegions.MarketDataView_SettingModule, typeof(MarketDataView));
            regionManager.RegisterViewWithRegion(AppRegions.UploadMarketDataView_SettingModule, typeof(UploadMarketDataView));
            regionManager.RegisterViewWithRegion(FlyoutRegions.AppSettingRegion, typeof(AppSettingView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISettingService, SettingService>();
        }
    }
}
