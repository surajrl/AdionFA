using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.ProjectStation.Common;
using AdionFA.UI.ProjectStation.StrategyBuilder;
using AdionFA.UI.ProjectStation.ViewModels;
using MahApps.Metro.Controls;
using Ninject;
using Prism.Regions;
using Serilog;
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
            IoC.Kernel.Get<ILogger>().Information("Shell.ctor() :: Call.");

            InitializeComponent();
            IoC.Kernel.Get<ILogger>().Information("Shell.ctor() :: InitializeComponent().");

            DataContext = vm;

            if (regionManager != null)
            {
                SetRegionManager(regionManager, flyoutsControlRegion, FlyoutRegions.FlyoutRegion);

                // Node Builder

                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(SavedNodesFlyoutView));
                IoC.Kernel.Get<ILogger>().Information("Shell.ctor() :: regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(SavedNodesFlyoutView)).");

                // Common

                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(NodesFlyoutView));
                IoC.Kernel.Get<ILogger>().Information("Shell.ctor() :: regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(NodesFlyoutView)).");
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(AssemblyNodesFlyoutView));
                IoC.Kernel.Get<ILogger>().Information("Shell.ctor() :: regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(AssemblyNodesFlyoutView)).");
                regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(StrategyNodesFlyoutView));
                IoC.Kernel.Get<ILogger>().Information("Shell.ctor() :: regionManager?.RegisterViewWithRegion(FlyoutRegions.FlyoutRegion, typeof(StrategyNodesFlyoutView)).");
            }
        }

        private static void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
