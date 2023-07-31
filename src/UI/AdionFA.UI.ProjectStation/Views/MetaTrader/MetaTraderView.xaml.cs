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
            if (((MetaTraderViewModel)DataContext).TestStrategyNode)
            {
                ((MetaTraderViewModel)DataContext).TestNodes = false;
                ((MetaTraderViewModel)DataContext).TestAssemblyNode = false;
            }
        }

        private void TestAssemblyNodeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (((MetaTraderViewModel)DataContext).TestAssemblyNode)
            {
                ((MetaTraderViewModel)DataContext).TestNodes = false;
                ((MetaTraderViewModel)DataContext).TestStrategyNode = false;
            }
        }

        private void TestNodesToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (((MetaTraderViewModel)DataContext).TestNodes)
            {
                ((MetaTraderViewModel)DataContext).TestAssemblyNode = false;
                ((MetaTraderViewModel)DataContext).TestStrategyNode = false;
            }
        }
    }
}
