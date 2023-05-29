using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Project.MetaTrader;
using AdionFA.UI.Station.Project.StrategyBuilder;
using AdionFA.UI.Station.Project.ViewModels;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace AdionFA.UI.Station.Project
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
                SetRegionManager(regionManager, flyoutsControlRegion, FlyoutRegions.FlyoutRegion);
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(WekaTreeFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(SavedNodesFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(CorrelationFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(NodeMetaTraderFlyoutView));
            }
        }

        private void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
