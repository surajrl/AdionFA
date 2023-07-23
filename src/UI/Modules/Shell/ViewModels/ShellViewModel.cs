using AdionFA.Application.Contracts;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Enums;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.Module.Model;
using AutoMapper;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            HistoricalTimeGrouping = EnumUtil.ToEnumerable<HistoricalTimeGroupingEnum>().Select(htg => htg.Name).ToList();

            applicationCommands.LoadProjectHierarchyCommand.RegisterCommand(UpdateProjectHierarchyCommand);
            applicationCommands.PinnedProjectCommand.RegisterCommand(UpdateProjectHierarchyInMemoryCommand);
        }

        public ICommand UpdateProjectHierarchyInMemoryCommand => new DelegateCommand<ProjectTreeViewModel>((ProjectTreeViewModel node) =>
        {
            var todayTime = DateTime.UtcNow;
            var lastWeekTime = todayTime.AddDays(-7);
            var lastMonthTime = todayTime.AddMonths(-1);

            node.Parent.Children.Remove(node);

            if (!string.IsNullOrWhiteSpace(SearchableText) || (SectionsSelected?.Any() ?? false))
            {
                FilterProjectHierarchy(SearchableText, SectionsSelected);
            }
        });

        public ICommand UpdateProjectHierarchyCommand => new DelegateCommand(() =>
        {
            PopulateViewModel();
            if (!string.IsNullOrWhiteSpace(SearchableText) || (SectionsSelected?.Any() ?? false))
            {
                FilterProjectHierarchy(SearchableText, SectionsSelected);
            }
        });

        public ICommand FilterProjectHierarchyCommand => new DelegateCommand<string>((string parameter) =>
        {
            FilterProjectHierarchy(parameter, SectionsSelected);
        });

        private void FilterProjectHierarchy(string filter, IList<string> restrictions)
        {
            var applyRestriction = ProjectHierarchy.Where(
                h => (restrictions?.Count ?? 0) == 0 || restrictions.Any(r => r.Replace(" ", string.Empty).ToLower() == h.Name.Replace(" ", string.Empty).ToLower())
            ).Select(h => h.Name).ToList();
            foreach (var treeNodeParent in ProjectHierarchy.ToList())
            {
                if (!applyRestriction.Contains(treeNodeParent.Name))
                {
                    treeNodeParent.IsVisibility = VisibilityPropertyEnum.Collapsed.ToString();
                }
                else
                {
                    var count = 0;
                    foreach (var node in treeNodeParent.Children)
                    {
                        if (!node.Name.Replace(" ", string.Empty).ToLower().Contains(filter.Replace(" ", string.Empty).ToLower().ToString()))
                        {
                            node.IsVisibility = VisibilityPropertyEnum.Collapsed.ToString();
                            count++;
                        }
                        else
                        {
                            node.IsVisibility = VisibilityPropertyEnum.Visible.ToString();
                        }
                    }

                    if (treeNodeParent.Children.Count == count)
                    {
                        treeNodeParent.IsVisibility = VisibilityPropertyEnum.Collapsed.ToString();
                    }
                    else
                    {
                        treeNodeParent.IsVisibility = VisibilityPropertyEnum.Visible.ToString();
                    }
                }
            }
        }

        private void PopulateViewModel()
        {
            ProjectHierarchy?.Clear();
            var root = new ProjectHierarchicalVM
            {
                Name = "Root",
                Projects = new ObservableCollection<ProjectHierarchicalVM>()
            };

            var projects = _projectService.GetAllProject(false);
            var projectsHierarchiacal = projects.Select(project => new ProjectHierarchicalVM
            {
                Project = _mapper.Map<ProjectDTO, ProjectVM>(project),
                Name = project.ProjectName
            });

            if (projects.Count > 0)
            {
                var todayTime = DateTime.UtcNow;
                var lastWeekTime = todayTime.AddDays(-7);
                var lastMonthTime = todayTime.AddMonths(-1);

                root.Projects.Add(new ProjectHierarchicalVM
                {
                    Name = HistoricalTimeGroupingEnum.Pinned.GetMetadata().Name,
                    Projects = new(projectsHierarchiacal)
                });
            }

            var children = new ProjectTreeViewModel(root).Children;
            ProjectHierarchy.AddRange(children);
            if (ProjectHierarchy.Any())
                ProjectHierarchy.FirstOrDefault(p => !p.IsExpanded).IsExpanded = true;
        }

        // View Bindings

        private string _searchableText;
        public string SearchableText
        {
            get => _searchableText;
            set => SetProperty(ref _searchableText, value);
        }

        private IList<string> _sectionsSelected;
        public IList<string> SectionsSelected
        {
            get => _sectionsSelected;
            set => SetProperty(ref _sectionsSelected, value);
        }

        public List<string> HistoricalTimeGrouping { get; }
        public ObservableCollection<ProjectTreeViewModel> ProjectHierarchy { get; } = new ObservableCollection<ProjectTreeViewModel>();
    }
}