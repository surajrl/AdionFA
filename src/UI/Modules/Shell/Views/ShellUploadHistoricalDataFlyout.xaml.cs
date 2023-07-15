using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellUploadMarketDataFlyout.xaml
    /// </summary>
    public partial class ShellUploadHistoricalDataFlyout : Flyout, IFlyoutView
    {
        public ShellUploadHistoricalDataFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutUploadHistoricalData;
    }
}
