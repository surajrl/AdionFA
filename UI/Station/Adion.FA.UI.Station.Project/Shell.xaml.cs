using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Project.MetaTrader;
using Adion.FA.UI.Station.Project.StrategyBuilder;
using Adion.FA.UI.Station.Project.ViewModels;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace Adion.FA.UI.Station.Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell(IRegionManager regionManager, MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            if (regionManager != null)
            {
                SetRegionManager(regionManager, this.flyoutsControlRegion, FlyoutRegions.FlyoutRegion);
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(WekaTreeFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(ConfigurationFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(CorrelationFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(NodeMetaTraderFlyoutView));
            }
        }

        void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
