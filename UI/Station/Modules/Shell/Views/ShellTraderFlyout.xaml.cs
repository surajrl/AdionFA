using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellTraderFlyout.xaml
    /// </summary>
    public partial class ShellTraderFlyout : Flyout, IFlyoutView
    {
        public ShellTraderFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutTrader;
    }
}
