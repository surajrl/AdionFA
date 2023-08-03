using AdionFA.Application.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Project;
using AutoMapper;
using Ninject;
using Prism.Commands;
using Serilog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace AdionFA.UI.Module.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly ILogger _logger;

        public ShellViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = IoC.Kernel.Get<IProjectService>();
            _logger = IoC.Kernel.Get<ILogger>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            PopulateViewModel();

            applicationCommands.LoadProjectsCommand.RegisterCommand(LoadProjectCommand);
        }

        public ICommand LoadProjectCommand => new DelegateCommand(PopulateViewModel);

        public ICommand ProjectStartCommand => new DelegateCommand<int?>(projectId =>
        {
            var project = IoC.Kernel.Get<IProjectService>().GetProject(projectId ?? 0, false);

            var process = new Process
            {
                StartInfo = new()
                {
                    FileName = "AdionFA.UI.ProjectStation.exe",
                    Arguments = $"{projectId}_AdionFA.UI.ProjectStation_{project.ProjectName}",
                }
            };

            if (process.Start())
            {
                _logger.Information($"ProcessService.StartProcessProject() :: Project {projectId} started.");
            };
        });

        private void PopulateViewModel()
        {
            AllProjects.Clear();

            var allProjects = _projectService.GetAllProject(true);

            AllProjects.AddRange(_mapper.Map<IList<ProjectVM>>(allProjects));
        }

        // View Bindings

        public ObservableCollection<ProjectVM> AllProjects { get; } = new();
    }
}