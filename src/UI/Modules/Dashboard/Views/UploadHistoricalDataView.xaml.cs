using AdionFA.UI.Module.Dashboard.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Controls;

namespace AdionFA.UI.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for UploadMarketDataView.xaml
    /// </summary>
    public partial class UploadHistoricalDataView : UserControl
    {
        public UploadHistoricalDataView(UploadHistoricalDataViewModel viewModel)
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
