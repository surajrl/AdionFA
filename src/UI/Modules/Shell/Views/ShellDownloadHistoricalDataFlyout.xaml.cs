using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
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
