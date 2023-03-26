using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.StrategyBuilder
{
    /// <summary>
    /// Interaction logic for CorrelationFlyoutView.xaml
    /// </summary>
    public partial class CorrelationFlyoutView : Flyout, IFlyoutView
    {
        public CorrelationFlyoutView(CorrelationFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleCorrelation;
    }
}
