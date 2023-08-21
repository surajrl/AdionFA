using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using AdionFA.UI.ProjectStation.ViewModels.Common;
using MahApps.Metro.Controls;

namespace AdionFA.UI.ProjectStation.Common
{
    /// <summary>
    /// Interaction logic for SingleNodesFlyoutView.xaml
    /// </summary>
    public partial class SingleNodesFlyoutView : Flyout, IFlyoutView
    {
        public SingleNodesFlyoutView(SingleNodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleNodes;
    }
}
