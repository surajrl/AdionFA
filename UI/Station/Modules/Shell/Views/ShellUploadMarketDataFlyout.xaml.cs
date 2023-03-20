using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace Adion.FA.UI.Station.Module.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellUploadMarketDataFlyout.xaml
    /// </summary>
    public partial class ShellUploadMarketDataFlyout : Flyout, IFlyoutView
    {
        public ShellUploadMarketDataFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutUploadMarketData;
    }
}
