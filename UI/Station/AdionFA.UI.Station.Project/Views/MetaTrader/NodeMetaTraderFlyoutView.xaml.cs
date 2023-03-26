using AdionFA.UI.Station.Project.ViewModels.MetaTrader;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.MetaTrader
{
    /// <summary>
    /// Interaction logic for NodeMetaTraderView.xaml
    /// </summary>
    public partial class NodeMetaTraderFlyoutView : Flyout, IFlyoutView
    {
        public NodeMetaTraderFlyoutView(NodeMetaTraderFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleNodeMetaTrader;
    }
}
