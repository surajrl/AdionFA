using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellMarketHistoryDataFlyout.xaml
    /// </summary>
    public partial class ShellHistoricalDataFlyout : Flyout, IFlyoutView
    {
        public ShellHistoricalDataFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutHistoricalData;
    }
}
