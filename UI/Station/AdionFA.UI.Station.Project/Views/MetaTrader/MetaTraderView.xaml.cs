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

        private void TestAssembledNodeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!((MetaTraderViewModel)DataContext).TestNodes)
            {
                return;
            }

            ((MetaTraderViewModel)DataContext).TestNodes = !((MetaTraderViewModel)DataContext).TestAssembledNode;
        }

        private void TestNodesToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!((MetaTraderViewModel)DataContext).TestAssembledNode)
            {
                return;
            }

            ((MetaTraderViewModel)DataContext).TestAssembledNode = !((MetaTraderViewModel)DataContext).TestNodes;
        }
    }
}
