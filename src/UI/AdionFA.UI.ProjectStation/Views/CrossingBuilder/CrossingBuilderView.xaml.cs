using AdionFA.UI.ProjectStation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.ProjectStation.Views
{
    /// <summary>
    /// Interaction logic for CrossingBuilderView.xaml
    /// </summary>
    public partial class CrossingBuilderView : UserControl
    {
        public CrossingBuilderView()
        {
            InitializeComponent();
        }

        private void LoadFromCrossingBuilderToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is CrossingBuilderViewModel vm)
            {
                if (vm.LoadFromCrossingBuilder)
                {
                    vm.LoadFromNodeBuilder = false;
                    vm.LoadFromAssemblyBuilder = false;
                }
            }
        }

        private void LoadFromNodeBuilderToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is CrossingBuilderViewModel vm)
            {
                if (vm.LoadFromNodeBuilder)
                {
                    vm.LoadFromAssemblyBuilder = false;
                    vm.LoadFromCrossingBuilder = false;
                }
            }
        }

        private void LoadFromAssemblyBuilderToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is CrossingBuilderViewModel vm)
            {
                if (vm.LoadFromAssemblyBuilder)
                {
                    vm.LoadFromNodeBuilder = false;
                    vm.LoadFromCrossingBuilder = false;
                }
            }
        }

        private void MultiAssemblyToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is CrossingBuilderViewModel vm)
            {
                if (vm.IsMultiAssemblyMode)
                {
                    assemblyMode.Content = "Multi Assembly ON";
                }
                else
                {
                    assemblyMode.Content = "Single Assembly ON";
                }
            }
        }
    }
}
