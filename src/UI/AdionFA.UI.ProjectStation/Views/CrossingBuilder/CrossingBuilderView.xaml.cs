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

        private void LoadFromNodeBuilderToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is CrossingBuilderViewModel vm)
            {
                if (vm.LoadFromNodeBuilder)
                {
                    vm.LoadFromAssemblyBuilder = false;
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
                }
            }
        }

        private void MultiAssemblyToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is CrossingBuilderViewModel vm)
            {
                if (vm.IsMultiAssemblyMode)
                {
                    assemblyMode.Content = "Multi Assembly";
                }
                else
                {
                    assemblyMode.Content = "Single Assembly";
                }
            }
        }
    }
}
