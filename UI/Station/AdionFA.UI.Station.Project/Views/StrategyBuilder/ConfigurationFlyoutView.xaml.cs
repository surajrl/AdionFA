using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Project.ViewModels.StrategyBuilder;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.StrategyBuilder
{
    /// <summary>
    /// Interaction logic for ConfigurationFlyoutView.xaml
    /// </summary>
    public partial class ConfigurationFlyoutView : Flyout, IFlyoutView
    {
        public ConfigurationFlyoutView(ConfigurationFlyoutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public string FlyoutName => FlyoutRegions.FlyoutProjectModuleAutoConfiguration;
    }
}
