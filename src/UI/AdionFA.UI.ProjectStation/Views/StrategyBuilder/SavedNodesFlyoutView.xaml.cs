using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.StrategyBuilder
{
    /// <summary>
    /// Interaction logic for SavedNodesFlyoutView.xaml
    /// </summary>
    public partial class SavedNodesFlyoutView : Flyout, IFlyoutView
    {
        public SavedNodesFlyoutView(SavedNodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleSavedNodes;
    }
}