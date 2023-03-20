using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace Adion.FA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellMarketHistoryDataFlyout.xaml
    /// </summary>
    public partial class ShellMarketDataFlyout : Flyout, IFlyoutView
    {
        public ShellMarketDataFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutMarketData;
    }
}
