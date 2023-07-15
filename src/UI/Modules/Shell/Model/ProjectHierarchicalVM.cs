using System.Collections.ObjectModel;

namespace AdionFA.UI.Station.Module.Shell.Model
{
    public class ProjectHierarchicalVM
    {
        public string Name { get; set; }

        public ProjectVM Project { get; set; }

        public ObservableCollection<ProjectHierarchicalVM> Projects { get; set; }
    }
}
