using Adion.FA.UI.Station.Modules.Trader.ViewModels;
using System.Windows.Controls;

namespace Adion.FA.UI.Station.Modules.Trader
{
    /// <summary>
    /// Interaction logic for TraderView.xaml
    /// </summary>
    public partial class TraderView : UserControl
    {
        public TraderView(TraderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
