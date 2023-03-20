using Adion.FA.UI.Station.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace Adion.FA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for MarketHistoryDataView.xaml
    /// </summary>
    public partial class MarketDataView : UserControl
    {
        public MarketDataView(MarketDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
