using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellAppSettingFlyout.xaml
    /// </summary>
    public partial class ShellAppSettingFlyout : Flyout, IFlyoutView
    {
        public ShellAppSettingFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutAppSetting;
    }
}
