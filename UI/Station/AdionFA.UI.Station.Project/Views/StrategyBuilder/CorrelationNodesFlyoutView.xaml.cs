using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.StrategyBuilder
{
    /// <summary>
    /// Interaction logic for CorrelationNodesFlyoutView.xaml
    /// </summary>
    public partial class CorrelationNodesFlyoutView : Flyout, IFlyoutView
    {
        public CorrelationNodesFlyoutView(CorrelationNodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleCorrelationNodes;
    }
}
