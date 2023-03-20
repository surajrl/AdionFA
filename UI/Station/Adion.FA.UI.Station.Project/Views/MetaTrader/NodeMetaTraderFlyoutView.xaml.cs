using Adion.FA.UI.Station.Project.ViewModels.MetaTrader;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace Adion.FA.UI.Station.Project.MetaTrader
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
