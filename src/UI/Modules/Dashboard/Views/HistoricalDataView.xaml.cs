using AdionFA.UI.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace AdionFA.UI.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for HistoricalDataView.xaml
    /// </summary>
    public partial class HistoricalDataView : UserControl
    {
        public HistoricalDataView(HistoricalDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
