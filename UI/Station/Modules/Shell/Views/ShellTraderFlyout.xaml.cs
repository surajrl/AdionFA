using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace Adion.FA.UI.Station.Module.Shell.Views
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
