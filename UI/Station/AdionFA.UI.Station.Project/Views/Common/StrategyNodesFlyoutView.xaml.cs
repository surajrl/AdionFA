using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.Common;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.Common
{
    /// <summary>
    /// Interaction logic for StrategyNodesFlyoutView.xaml
    /// </summary>
    public partial class StrategyNodesFlyoutView : Flyout, IFlyoutView
    {
        public StrategyNodesFlyoutView(StrategyNodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleStrategyNodes;
    }
}
