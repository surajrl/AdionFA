using AdionFA.Application.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Project;
using AutoMapper;
using Ninject;
using Prism.Commands;
using Prism.Ioc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.Module.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;

        public ShellViewModel(IApplicationCommands applicationCommands)
        {
            _projectService = IoC.Kernel.Get<IProjectService>();

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            PopulateViewModel();

            applicationCommands.LoadProjectHierarchyCommand.RegisterCommand(UpdateProjectHierarchyCommand);
        }

        public ICommand UpdateProjectHierarchyCommand => new DelegateCommand(PopulateViewModel);

        public ICommand ProjectStartCommand => new DelegateCommand<int?>(projectId =>
        {
            ContainerLocator.Current.Resolve<IApplicationCommands>().StartProcessProjectCommand.Execute(projectId);
        });

        private void PopulateViewModel()
        {
            AllProjects.Clear();

            var allProjects = _projectService.GetAllProject(false);

            AllProjects.AddRange(_mapper.Map<IList<ProjectVM>>(allProjects));
        }

        // View Bindings

        public ObservableCollection<ProjectVM> AllProjects { get; } = new();
    }
}