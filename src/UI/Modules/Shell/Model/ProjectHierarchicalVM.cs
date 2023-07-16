using AdionFA.UI.Infrastructure.Model.Project;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Module.Model
{
    public class ProjectHierarchicalVM
    {
        public string Name { get; set; }

        public ProjectVM Project { get; set; }

        public ObservableCollection<ProjectHierarchicalVM> Projects { get; set; }
    }
}
