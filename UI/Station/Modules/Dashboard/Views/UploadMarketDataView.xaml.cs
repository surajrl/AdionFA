using Adion.FA.UI.Station.Module.Dashboard.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Controls;

namespace Adion.FA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for UploadMarketDataView.xaml
    /// </summary>
    public partial class UploadMarketDataView : UserControl
    {
        public UploadMarketDataView(UploadMarketDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void finPathFileBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                txtPathFile.Text = Path.GetFullPath(openFileDialog.FileName);
        }
    }
}
