using AdionFA.UI.ProjectStation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.ProjectStation.Views
{
    /// <summary>
    /// Interaction logic for AssemblyBuilderView.xaml
    /// </summary>
    public partial class AssemblyBuilderView : UserControl
    {
        public AssemblyBuilderView()
        {
            InitializeComponent();
        }

        private void MultiAssemblyToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ((AssemblyBuilderViewModel)DataContext).MultiAssemblyMode = !((AssemblyBuilderViewModel)DataContext).MultiAssemblyMode;
        }
    }
}
