using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
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
