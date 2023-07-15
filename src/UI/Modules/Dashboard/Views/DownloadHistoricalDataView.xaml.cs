using AdionFA.UI.Station.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace AdionFA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for DownloadMarketDataView.xaml
    /// </summary>
    public partial class DownloadHistoricalDataView : UserControl
    {
        public DownloadHistoricalDataView(DownloadHistoricalDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
