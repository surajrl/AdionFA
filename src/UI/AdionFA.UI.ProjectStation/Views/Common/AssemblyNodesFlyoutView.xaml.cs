using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using AdionFA.UI.ProjectStation.ViewModels.Common;
using MahApps.Metro.Controls;

namespace AdionFA.UI.ProjectStation.Common
{
    /// <summary>
    /// Interaction logic for AssemblyNodesFlyoutView.xaml
    /// </summary>
    public partial class AssemblyNodesFlyoutView : Flyout, IFlyoutView
    {
        public AssemblyNodesFlyoutView(AssemblyNodesFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleAssemblyNodes;
    }
}
