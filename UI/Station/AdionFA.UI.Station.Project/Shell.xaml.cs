using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Project.AssembledBuilder;
using AdionFA.UI.Station.Project.StrategyBuilder;
using AdionFA.UI.Station.Project.ViewModels;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace AdionFA.UI.Station.Project
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

                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(CorrelationNodesFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(SavedNodesFlyoutView));

                // Common

                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(NodesFlyoutView));
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(AssembledNodesFlyoutView));
            }
        }

        private void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
