using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Project.Model.Extractor;
using AdionFA.UI.Station.Project.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.Station.Project.Views
{
    /// <summary>
    /// Interaction logic for ExtractorView.xaml
    /// </summary>
    public partial class ExtractorView : UserControl
    {
        public ExtractorView()
        {
            InitializeComponent();
        }

        private void addTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                IProjectDirectoryService directoryService = new ProjectDirectoryService();
                foreach (var filename in openFileDialog.FileNames)
                {
                    var fi = new FileInfo(filename);
                    directoryService.CopyCSVFileTo(fi, ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                    ((ExtractorViewModel)DataContext).ExtractionProcessList.Add(new ExtractionProcessModel
                    {
                        TemplateName = fi.Name,
                        Status = ExtractorStatusEnum.NoStarted.GetMetadata().Name,
                        Path = fi.FullName
                    });
                }
            }
        }

        private void replaceTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                IProjectDirectoryService directoryService = new ProjectDirectoryService();
                if (directoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory()))
                {
                    var fileModels = new List<ExtractionProcessModel>();
                    foreach (var filename in openFileDialog.FileNames)
                    {
                        var fi = new FileInfo(filename);
                        directoryService.CopyCSVFileToAsync(fi, ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                        fileModels.Add(new ExtractionProcessModel
                        {
                            TemplateName = fi.Name,
                            Status = ExtractorStatusEnum.NoStarted.GetMetadata().Name,
                            Path = fi.FullName
                        });
                    }

                    ((ExtractorViewModel)DataContext).ExtractionProcessList = new ObservableCollection<ExtractionProcessModel>();
                    ((ExtractorViewModel)DataContext).ExtractionProcessList.AddRange(fileModels);
                }
            }
        }

        private void openExtractorPathBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(ProcessArgs.ProjectName.ProjectExtractorDirectory()))
            {
                Process.Start("explorer.exe", ProcessArgs.ProjectName.ProjectExtractorDirectory());
            }
            else
            {
                MessageBox.Show("Directory not exist!");
            }
        }
    }
}
