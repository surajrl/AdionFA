using AdionFA.UI.Station.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace AdionFA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for CreateProjectView.xaml
    /// </summary>
    public partial class CreateProjectView : UserControl
    {
        public CreateProjectView(CreateProjectViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
