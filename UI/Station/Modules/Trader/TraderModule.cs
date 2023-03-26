using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Modules.Trader.Infrastructure;
using AdionFA.UI.Station.Modules.Trader.Services;
using AdionFA.UI.Station.Modules.Trader.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AdionFA.UI.Station.Modules.Trader
{
    public class TraderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager?.RegisterViewWithRegion(AppRegions.TraderView_TraderModule, typeof(TraderView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<TradeGenerator>();
            containerRegistry.RegisterSingleton<IStaticData, StaticData>();
            containerRegistry.RegisterSingleton<IMarketDataService, MarketDataService>();
            containerRegistry.RegisterSingleton<IWindowFactory, WindowFactory>();
            containerRegistry.RegisterSingleton<ITraderService, TraderService>();

            //View Models
            containerRegistry.RegisterSingleton<TraderPositionViewModel>();
        }
    }
}
