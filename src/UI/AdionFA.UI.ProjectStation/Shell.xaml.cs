using AdionFA.UI.Infrastructure;
using AdionFA.UI.ProjectStation.Common;
using AdionFA.UI.ProjectStation.StrategyBuilder;
using AdionFA.UI.ProjectStation.ViewModels;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace AdionFA.UI.ProjectStation
{
    /// <summary>
    /// Interaction logic for Shell.xaml
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

                // Strategy Builder

                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(SavedNodesFlyoutView));

                // Common

                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(NodesFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(AssemblyNodesFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(StrategyNodesFlyoutView));
            }
        }

        private static void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
