using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellDownloadMarketDataFlyout.xaml
    /// </summary>
    public partial class ShellDownloadHistoricalDataFlyout : Flyout, IFlyoutView
    {
        public ShellDownloadHistoricalDataFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutDownloadHistoricalData;
    }
}
