using Adion.FA.UI.Station.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace Adion.FA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for AppSettingView.xaml
    /// </summary>
    public partial class AppSettingView : StackPanel
    {
        public AppSettingView(AppSettingViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void TextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPathWorkspace.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
