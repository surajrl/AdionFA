using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellSettingFlyout.xaml
    /// </summary>
    public partial class ShellProjectGlobalconfigFlyout : Flyout, IFlyoutView
    {
        public ShellProjectGlobalconfigFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectGlobalConfig;
    }
}
