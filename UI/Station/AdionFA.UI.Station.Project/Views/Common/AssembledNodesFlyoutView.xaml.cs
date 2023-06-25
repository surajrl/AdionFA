using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.Common;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.AssemblyBuilder
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
