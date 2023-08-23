using AdionFA.UI.ProjectStation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.ProjectStation.Views
{
    /// <summary>
    /// Interaction logic for WekaDecisionTreesView.xaml
    /// </summary>
    public partial class MetaTraderView : UserControl
    {
        public MetaTraderView()
        {
            InitializeComponent();
        }

        private void TestStrategyNodeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is MetaTraderViewModel vm)
            {
                if (vm.TestStrategyNode)
                {
                    vm.TestNodes = false;
                    vm.TestAssemblyNode = false;
                }
            }
        }

        private void TestAssemblyNodeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is MetaTraderViewModel vm)
            {
                if (vm.TestAssemblyNode)
                {
                    vm.TestNodes = false;
                    vm.TestStrategyNode = false;
                }
            }
        }

        private void TestNodesToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is MetaTraderViewModel vm)
            {
                if (vm.TestNodes)
                {
                    vm.TestAssemblyNode = false;
                    vm.TestStrategyNode = false;
                }
            }
        }

        private void MultiAssemblyToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext is MetaTraderViewModel vm)
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
