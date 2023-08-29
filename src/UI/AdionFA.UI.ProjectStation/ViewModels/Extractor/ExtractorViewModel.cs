using AdionFA.Application.Contracts;
using AdionFA.Domain.Properties;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Directories.Services;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.EventAggregator;
using AdionFA.UI.ProjectStation.Features;
using AutoMapper;
using MahApps.Metro.Controls.Dialogs;
using Ninject;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class ExtractorViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;

        private readonly IEventAggregator _eventAggregator;

        private readonly IProjectService _projectService;
        private readonly IProjectDirectoryService _projectDirectoryService;

        public ExtractorViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _projectService = IoC.Kernel.Get<IProjectService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(canExecute => CanExecute = canExecute);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            ExtractorTemplates = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.ExtractorTrim)
            {
                ProjectConfiguration = _mapper.Map<ProjectConfigurationDTO, ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));

                ExtractorPath = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                ExtractorTemplates.Clear();

                foreach (var template in _projectDirectoryService.GetFilesInPath(ExtractorPath, "*.csv"))
                {
                    ExtractorTemplates.Add(template.Name);
                }
            }
        });

        public ICommand AddExtractorTemplateCommand => new DelegateCommand(async () =>
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var directoryService = new ProjectDirectoryService();
                foreach (var filename in openFileDialog.FileNames)
                {
                    var fileInfo = new FileInfo(filename);

                    // Check if the file already exists
                    if (ExtractorTemplates.Contains(fileInfo.Name))
                    {
                        var overwrite = await MessageHelper.ShowMessageInputAsync(this,
                            Resources.Extractor,
                            $"A template with the name {fileInfo.Name} already exists, do you want to overwrite it?")
                            .ConfigureAwait(true);

                        if (overwrite != MessageDialogResult.Affirmative)
                        {
                            continue;
                        }
                        else
                        {
                            ExtractorTemplates.Remove(fileInfo.Name);
                        }
                    }

                    directoryService.CopyCSVFileTo(fileInfo, ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                    ExtractorTemplates.Add(fileInfo.Name);
                }
            }
        });

        public ICommand ReplaceExtractorTemplateCommand => new DelegateCommand(() =>
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var directoryService = new ProjectDirectoryService();
                if (directoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory(), "*.csv", false, SearchOption.AllDirectories))
                {
                    var filenames = new List<string>();
                    foreach (var filename in openFileDialog.FileNames)
                    {
                        var fileInfo = new FileInfo(filename);
                        directoryService.CopyCSVFileTo(fileInfo, ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                        filenames.Add(fileInfo.Name);
                    }

                    ExtractorTemplates.Clear();
                    ExtractorTemplates.AddRange(filenames);
                }
            }
        });


        // View Bindings

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }

        private string _extractorPath;
        public string ExtractorPath
        {
            get => _extractorPath;
            set => SetProperty(ref _extractorPath, value);
        }

        public ObservableCollection<string> ExtractorTemplates { get; set; }
    }
}