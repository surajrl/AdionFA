using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellConfigurationFlyout.xaml
    /// </summary>
    public partial class ShellConfigurationFlyout : Flyout, IFlyoutView
    {
        public ShellConfigurationFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutConfiguration;
    }
}
