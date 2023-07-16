using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
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
