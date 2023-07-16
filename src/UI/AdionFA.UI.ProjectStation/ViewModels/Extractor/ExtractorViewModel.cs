using AdionFA.Application.Contracts;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.Features;
using AutoMapper;
using Ninject;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class ExtractorViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;
        private readonly IProjectAppService _projectService;
        private readonly IProjectDirectoryService _projectDirectoryService;

        public ExtractorViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _projectService = IoC.Kernel.Get<IProjectAppService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            ExtractorTemplates = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.ExtractorTrim)
            {
                try
                {
                    // Get the latest project configuration
                    ProjectConfiguration = _mapper.Map<ProjectConfigurationDTO, ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));

                    ExtractorPath = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                    ExtractorTemplates.Clear();

                    foreach (var template in _projectDirectoryService.GetFilesInPath(ExtractorPath))
                    {
                        ExtractorTemplates.Add(template.Name);
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }
            }
        });

        // View Bindings

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