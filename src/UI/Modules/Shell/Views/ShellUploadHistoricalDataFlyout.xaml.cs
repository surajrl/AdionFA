using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
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
