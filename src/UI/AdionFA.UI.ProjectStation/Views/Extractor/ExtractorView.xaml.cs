using AdionFA.Infrastructure.Directories.Services;
using AdionFA.Infrastructure.Managements;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.ViewModels;
using Microsoft.Win32;
using Prism.Events;
using Prism.Ioc;
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
                var directoryService = new ProjectDirectoryService();
                foreach (var filename in openFileDialog.FileNames)
                {
                    var fileInfo = new FileInfo(filename);
                    directoryService.CopyCSVFileTo(fileInfo, ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                    ((ExtractorViewModel)DataContext).ExtractorTemplates.Add(fileInfo.Name);
                }

                ContainerLocator.Current.Resolve<IEventAggregator>().GetEvent<ExtractorTemplatesUpdatedEvent>().Publish(true);
            }
        }

        private void replaceTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var directoryService = new ProjectDirectoryService();
                if (directoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), isBackup: false))
                {
                    var filenames = new List<string>();
                    foreach (var filename in openFileDialog.FileNames)
                    {
                        var fileInfo = new FileInfo(filename);
                        directoryService.CopyCSVFileTo(fileInfo, ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                        filenames.Add(fileInfo.Name);
                    }

                    ((ExtractorViewModel)DataContext).ExtractorTemplates.Clear();
                    ((ExtractorViewModel)DataContext).ExtractorTemplates.AddRange(filenames);

                    ContainerLocator.Current.Resolve<IEventAggregator>().GetEvent<ExtractorTemplatesUpdatedEvent>().Publish(true);
                }
            }
        }

        private void openExtractorPathBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory()))
            {
                Process.Start("explorer.exe", ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
            }
            else
            {
                MessageBox.Show("Directory not exist!");
            }
        }
    }
}
