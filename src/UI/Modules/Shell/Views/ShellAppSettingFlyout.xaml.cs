using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
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
