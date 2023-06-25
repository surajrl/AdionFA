using AdionFA.UI.Station.Project.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.Station.Project.Views
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

        private void TestAssemblyNodeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!((MetaTraderViewModel)DataContext).TestNodes)
            {
                return;
            }

            ((MetaTraderViewModel)DataContext).TestNodes = !((MetaTraderViewModel)DataContext).TestAssemblyNode;
        }

        private void TestNodesToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!((MetaTraderViewModel)DataContext).TestAssemblyNode)
            {
                return;
            }

            ((MetaTraderViewModel)DataContext).TestAssemblyNode = !((MetaTraderViewModel)DataContext).TestNodes;
        }
    }
}
