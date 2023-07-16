using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Enums;
using AdionFA.UI.Module.Model;
using Prism.Commands;
using Prism.Ioc;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdionFA.UI.Module.ViewModels
{
    public class ProjectTreeViewModel : ViewModelBase
    {
        public ProjectHierarchicalVM Hierarchy { get; }

        public ProjectTreeViewModel(ProjectHierarchicalVM project) : this(project, null)
        {
        }

        public ProjectTreeViewModel(ProjectHierarchicalVM hierarchy, ProjectTreeViewModel parent)
        {
            Hierarchy = hierarchy;
            Parent = parent;
            _isVisibility = VisibilityPropertyEnum.Visible.ToString();

            if (Hierarchy.Projects != null)
            {
                Children = new ObservableCollection<ProjectTreeViewModel>(
                        (from child in Hierarchy.Projects
                         select new ProjectTreeViewModel(child, this))?.ToList<ProjectTreeViewModel>());
            }

            Name = parent?.Name == "Root"
                ? hierarchy.Name
                : null;
        }

        public DelegateCommand ProjectStartCommand => new(() =>
        {
            ContainerLocator.Current.Resolve<IApplicationCommands>().StartProcessProjectCommand.Execute(Hierarchy.Project.ProjectId);
        });

        // View Bindings

        private string _isVisibility;
        public string IsVisibility
        {
            get => _isVisibility;
            set => SetProperty(ref _isVisibility, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                SetProperty<bool>(ref _isExpanded, value);

                // Expand all the way up to the root.
                if (_isExpanded && Parent != null)
                {
                    Parent.IsExpanded = true;
                }
            }
        }

        private string name;
        public string Name
        {
            get => Parent?.Name == "Root" ? name : (Hierarchy?.Name ?? string.Empty);
            set => SetProperty(ref name, value);
        }


        public ProjectTreeViewModel Parent { get; set; }

        public ObservableCollection<ProjectTreeViewModel> Children { get; } = new ObservableCollection<ProjectTreeViewModel>();
    }
}
