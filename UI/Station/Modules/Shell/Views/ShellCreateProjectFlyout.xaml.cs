using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Module.Shell.Views
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
