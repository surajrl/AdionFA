using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.StrategyBuilder
{
    /// <summary>
    /// Interaction logic for WekaTreeFlyoutView.xaml
    /// </summary>
    public partial class WekaTreeFlyoutView : Flyout, IFlyoutView
    {
        public WekaTreeFlyoutView(WekaTreeFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleWekaTree;
    }
}
