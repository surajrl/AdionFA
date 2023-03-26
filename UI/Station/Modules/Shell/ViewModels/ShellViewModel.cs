using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Enums;
using AdionFA.UI.Station.Module.Shell.Model;
using AdionFA.UI.Station.Module.Shell.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace AdionFA.UI.Station.Module.Shell.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        #region Services

        private readonly IShellServiceShell _shellService;

        #endregion Services

        #region Ctor

        public ShellViewModel(
            IShellServiceShell shellServices,
            IApplicationCommands applicationCommands)
        {
            _shellService = shellServices;

            PopulateViewModel();

            HistoricalTimeGrouping = EnumUtil.ToEnumerable<HistoricalTimeGroupingEnum>().Select(htg => htg.Name).ToList();

            applicationCommands.LoadProjectHierarchyCommand.RegisterCommand(UpdateProjectHierarchyCommand);
            applicationCommands.PinnedProjectCommand.RegisterCommand(UpdateProjectHierarchyInMemoryCommand);
        }

        #endregion Ctor

        #region Commands

        #region UpdateProjectHierarchyCommand

        public ICommand UpdateProjectHierarchyInMemoryCommand => new DelegateCommand<ProjectTreeViewModel>((ProjectTreeViewModel node) =>
        {
            DateTime todayTime = DateTime.UtcNow;
            DateTime lastWeekTime = todayTime.AddDays(-7);
            DateTime lastMonthTime = todayTime.AddMonths(-1);

            //if (node.IsPinned)
            //{
            //    var pinnedParentNode = ProjectHierarchy.FirstOrDefault(ph => ph.Name == HistoricalTimeGroupingEnum.Pinned.ToString());
            //    if (pinnedParentNode != null)
            //    {
            //        pinnedParentNode.Children.Add(node);
            //    }
            //    else
            //    {
            //        var root = node.Parent.Parent;
            //        if (root != null)
            //        {
            //            root.Children.Add(new ProjectTreeViewModel
            //            {
            //                Children = new ObservableCollection<ProjectTreeViewModel> { node }
            //            });
            //        }
            //    }
            //}

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

        #endregion UpdateProjectHierarchyCommand

        #region Filter Into Project Hierarchy

        public ICommand FilterProjectHierarchyCommand => new DelegateCommand<string>((string parameter) =>
        {
            FilterProjectHierarchy(parameter, SectionsSelected);
        });

        private void FilterProjectHierarchy(string filter, IList<string> restrictions)
        {
            List<string> applyRestriction = ProjectHierarchy.Where(
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
                    int count = 0;
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

        #endregion Filter Into Project Hierarchy

        #endregion Commands

        private async void PopulateViewModel()
        {
            #region ProjectHierarchy

            ProjectHierarchy?.Clear();
            var root = new ProjectHierarchicalVM
            {
                Name = "Root",
                Projects = new ObservableCollection<ProjectHierarchicalVM>()
            };

            var projects = await _shellService.GetAllProjects();
            if (projects.Count > 0)
            {
                DateTime todayTime = DateTime.UtcNow;
                DateTime lastWeekTime = todayTime.AddDays(-7);
                DateTime lastMonthTime = todayTime.AddMonths(-1);

                var loadFavoriteProject = projects.Where(p => p.IsFavorite).Select(
                    p => new ProjectHierarchicalVM { Name = p.Name, Project = p }).ToList();
                if (loadFavoriteProject.Count > 0)
                {
                    root.Projects.Add(new ProjectHierarchicalVM
                    {
                        Name = HistoricalTimeGroupingEnum.Pinned.GetMetadata().Name,
                        Projects = new ObservableCollection<ProjectHierarchicalVM>(loadFavoriteProject),
                    });
                }

                var loadedTodayProject = projects.Where(p => p.LastLoadOn.Date == todayTime.Date && !p.IsFavorite).Select(
                    p => new ProjectHierarchicalVM { Name = p.Name, Project = p }).ToList();
                if (loadedTodayProject.Count > 0)
                {
                    root.Projects.Add(new ProjectHierarchicalVM
                    {
                        Name = HistoricalTimeGroupingEnum.Today.GetMetadata().Name,
                        Projects = new ObservableCollection<ProjectHierarchicalVM>(loadedTodayProject)
                    });
                }

                var loadedLastWeekProject = projects.Where(p => p.LastLoadOn.Date < todayTime.Date && p.LastLoadOn.Date >= lastWeekTime.Date && !p.IsFavorite)
                    .Select(p => new ProjectHierarchicalVM { Name = p.Name, Project = p }).ToList();
                if (loadedLastWeekProject.Count > 0)
                {
                    root.Projects.Add(new ProjectHierarchicalVM
                    {
                        Name = HistoricalTimeGroupingEnum.LastWeek.GetMetadata().Name,
                        Projects = new ObservableCollection<ProjectHierarchicalVM>(loadedLastWeekProject)
                    });
                }

                var loadedLastMonthProject = projects.Where(p => p.LastLoadOn.Date < lastWeekTime.Date && p.LastLoadOn.Date >= lastMonthTime.Date && !p.IsFavorite)
                    .Select(p => new ProjectHierarchicalVM { Name = p.Name, Project = p }).ToList();
                if (loadedLastMonthProject.Count > 0)
                {
                    root.Projects.Add(new ProjectHierarchicalVM
                    {
                        Name = HistoricalTimeGroupingEnum.LastMonth.GetMetadata().Name,
                        Projects = new ObservableCollection<ProjectHierarchicalVM>(loadedLastMonthProject)
                    });
                }

                var olderProject = projects.Where(p => p.LastLoadOn.Date < lastMonthTime.Date && !p.IsFavorite)
                    .Select(p => new ProjectHierarchicalVM { Name = p.Name, Project = p }).ToList();
                if (olderProject.Count > 0)
                {
                    root.Projects.Add(new ProjectHierarchicalVM
                    {
                        Name = HistoricalTimeGroupingEnum.Older.GetMetadata().Name,
                        Projects = new ObservableCollection<ProjectHierarchicalVM>(olderProject)
                    });
                }
            }

            var children = new ProjectTreeViewModel(root).Children;
            ProjectHierarchy.AddRange(children);
            if (ProjectHierarchy.Any())
                ProjectHierarchy.FirstOrDefault(p => !p.IsExpanded).IsExpanded = true;

            #endregion ProjectHierarchy
        }

        #region Bindable Model

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

        #endregion Bindable Model
    }
}