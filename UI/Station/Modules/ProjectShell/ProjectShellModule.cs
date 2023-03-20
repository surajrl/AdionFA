using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Adion.FinancialAutomat.UI.Station.Modules.ProjectShell
{
    public class ProjectShellModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
