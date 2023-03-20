using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts;
using Adion.FA.UI.Station.Project.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace Adion.FA.UI.Station.Project.StrategyBuilder
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
