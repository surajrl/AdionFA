using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Enums;
using Adion.FA.UI.Station.Module.Shell.Model;
using Adion.FA.UI.Station.Module.Shell.Services;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Adion.FA.UI.Station.Module.Shell.ViewModels
{
    public class ProjectTreeViewModel : ViewModelBase
    {
        #region Model

        public ProjectHierarchicalVM Hierarchy { get; }

        #endregion

        #region Ctor

        public ProjectTreeViewModel()
        {

        }

        public ProjectTreeViewModel(ProjectHierarchicalVM project) : this(project, null)
        {
        }

        public ProjectTreeViewModel(ProjectHierarchicalVM hierarchy, ProjectTreeViewModel parent)
        {
            Hierarchy = hierarchy;
            _parent = parent;
            _isPinned = Hierarchy?.Project?.IsFavorite ?? false;
            _isVisibility = VisibilityPropertyEnum.Visible.ToString();
            //_isExpanded = true;
            if (Hierarchy.Projects != null)
            {
                Children = new ObservableCollection<ProjectTreeViewModel>(
                        (from child in Hierarchy.Projects
                         select new ProjectTreeViewModel(child, this))?.ToList<ProjectTreeViewModel>());
            }

            Name = parent?.Name == "Root" ? hierarchy.Name : null;
        }

        #endregion

        #region Commands

        public DelegateCommand CommandPin => new DelegateCommand(async () =>
        {
            IShellServiceShell projectService = ContainerLocator.Current.Resolve<IShellServiceShell>();
            var pinnedResult = await projectService.PinnedProject(Hierarchy.Project.ProjectId, !IsPinned);
            if (pinnedResult)
            {
                IsPinned = !IsPinned;
                //ContainerLocator.Current.Resolve<IApplicationCommands>().PinnedProjectCommand.Execute(this);
                ContainerLocator.Current.Resolve<IApplicationCommands>().LoadProjectHierarchyCommand.Execute(null);
            }
        });

        public DelegateCommand ProjectStartCommand => new DelegateCommand(() =>
        {
            ContainerLocator.Current.Resolve<IApplicationCommands>().StartProcessProjectCommand.Execute(Hierarchy.Project.ProjectId);
        });

        #endregion

        #region Bindable Model

        string _isVisibility;
        public string IsVisibility
        {
            get => _isVisibility;
            set => SetProperty(ref _isVisibility, value);
        }

        bool _isPinned;
        public bool IsPinned
        {
            get => _isPinned;
            set => SetProperty(ref _isPinned, value);
        }

        bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                SetProperty<bool>(ref _isExpanded, value);

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        string name;
        public string Name
        {
            get => Parent?.Name == "Root" ? name : (Hierarchy?.Name ?? string.Empty);
            set => SetProperty(ref name, value);
        }

        public string ProjectStepIcon
        {
            get { return Hierarchy?.Project?.ProjectStepIcon ?? string.Empty; }
        }

        public string CurrentProjectStep
        {
            get { return Hierarchy?.Project?.CurrentProjectStep ?? string.Empty; }
        }

        public DateTime LastLoadOn
        {
            get { return Hierarchy?.Project?.LastLoadOn ?? DateTime.MinValue; }
        }

        public string WorkspacePath
        {
            get { return Hierarchy?.Project?.WorkspacePath ?? string.Empty; }
        }

        public string WorkspacePathCut
        {
            get { return Hierarchy?.Project?.WorkspacePathCut ?? string.Empty; }
        }

        ProjectTreeViewModel _parent;
        public ProjectTreeViewModel Parent
        {
            get => _parent;
        }

        public ObservableCollection<ProjectTreeViewModel> Children { get; } = new ObservableCollection<ProjectTreeViewModel>();
        
        #endregion
    }
}
