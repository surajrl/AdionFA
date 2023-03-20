using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace Adion.FA.UI.Station.Module.Shell.Views
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
