using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.MetaTrader;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.MetaTrader
{
    /// <summary>
    /// Interaction logic for AssembledNodeMetaTraderFlyoutView.xaml
    /// </summary>
    public partial class AssembledNodeMetaTraderFlyoutView : Flyout, IFlyoutView
    {
        public AssembledNodeMetaTraderFlyoutView(AssembledNodeMetaTraderFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleAssembledNodeMetaTrader;
    }
}
