using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.Features;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class ExtractorViewModel : MenuItemViewModel
    {
        private readonly IProjectServiceAgent _projectService;
        private readonly IProjectDirectoryService _projectDirectoryService;

        public ExtractorViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            ExtractorTemplates = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.Extractor.Replace(" ", string.Empty))
            {
                PopulateViewModel();
            }
        });

        public async void PopulateViewModel()
        {
            try
            {
                // Get the latest project configuration
                var project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);
                ProjectConfiguration = project?.ProjectConfigurations.FirstOrDefault();

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