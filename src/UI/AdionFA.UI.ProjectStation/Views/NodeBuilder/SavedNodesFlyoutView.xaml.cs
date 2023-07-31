using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using AdionFA.UI.ProjectStation.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.ProjectStation.StrategyBuilder
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