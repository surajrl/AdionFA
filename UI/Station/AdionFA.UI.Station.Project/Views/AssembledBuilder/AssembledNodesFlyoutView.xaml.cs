using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.AssembledBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.AssembledBuilder
{
    /// <summary>
    /// Interaction logic for AssembledNodesFlyoutView.xaml
    /// </summary>
    public partial class AssembledNodesFlyoutView : Flyout, IFlyoutView
    {
        public AssembledNodesFlyoutView(AssembledNodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleAssembledNodes;
    }
}
