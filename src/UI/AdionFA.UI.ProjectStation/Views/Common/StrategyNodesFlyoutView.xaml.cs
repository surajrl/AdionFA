using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using AdionFA.UI.ProjectStation.ViewModels.Common;
using MahApps.Metro.Controls;

namespace AdionFA.UI.ProjectStation.Common
{
    /// <summary>
    /// Interaction logic for StrategyNodesFlyoutView.xaml
    /// </summary>
    public partial class StrategyNodesFlyoutView : Flyout, IFlyoutView
    {
        public StrategyNodesFlyoutView(NodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleStrategyNodes;
    }
}
