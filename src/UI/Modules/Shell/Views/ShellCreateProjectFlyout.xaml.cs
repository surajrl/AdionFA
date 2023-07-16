using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Module.Views
{
    /// <summary>
    /// Interaction logic for ShellCreateProjectFlyout.xaml
    /// </summary>
    public partial class ShellCreateProjectFlyout : Flyout, IFlyoutView
    {
        public ShellCreateProjectFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutRegions.FlyoutCreateProject;
    }
}
