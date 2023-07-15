using AdionFA.UI.Station.Infrastructure;
using MahApps.Metro.Controls;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AdionFA.UI.Station
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell(IRegionManager regionManager)
        {
            InitializeComponent();
            
            if (regionManager != null)
            {
                SetRegionManager(regionManager, this.rightWindowCommandsRegion, FlyoutRegions.RightWindowCommandsRegion);
                SetRegionManager(regionManager, this.flyoutsControlRegion, FlyoutRegions.FlyoutRegion);
            }
        }

        void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
