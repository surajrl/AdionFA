using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
{
    /// <summary>
    /// Interaction logic for ShellGlobalConfigurationFlyout.xaml
    /// </summary>
    public partial class ShellGlobalConfigurationFlyout : Flyout, IFlyoutView
    {
        public ShellGlobalConfigurationFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutGlobalConfiguration;
    }
}
