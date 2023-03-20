using Adion.FinancialAutomat.UI.Station.Modules.Infrastructure;
using Adion.FinancialAutomat.UI.Station.Modules.Tools.Services;
using Adion.FinancialAutomat.UI.Station.Modules.Tools.ViewModels;
using Adion.FinancialAutomat.UI.Station.Modules.Tools.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Adion.FinancialAutomat.UI.Station.Modules.Tools
{
    public class ToolsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager?.RegisterViewWithRegion(AppRegions.ToolsView_ToolsModule, typeof(ToolsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterSingleton<IToolsService, ToolsService>();

            //View Models
            containerRegistry.RegisterSingleton<ToolsViewModel>();
        }
    }
}
