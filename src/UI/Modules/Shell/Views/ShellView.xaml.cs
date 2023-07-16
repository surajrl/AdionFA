using AdionFA.UI.Module.ViewModels;
using System.Windows.Controls;

namespace AdionFA.UI.Module
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl
    {
        public ShellView(ShellViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
