using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using AdionFA.UI.ProjectStation.ViewModels.Common;
using MahApps.Metro.Controls;

namespace AdionFA.UI.ProjectStation.Common
{
    /// <summary>
    /// Interaction logic for NodesFlyoutView.xaml
    /// </summary>
    public partial class NodesFlyoutView : Flyout, IFlyoutView
    {
        public NodesFlyoutView(NodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleNodes;
    }
}
