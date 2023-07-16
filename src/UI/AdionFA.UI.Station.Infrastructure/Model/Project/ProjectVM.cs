using AdionFA.UI.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectVM : EntityBaseVM
    {
        public int ProjectId { get; set; }

        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        public IList<ProjectConfigurationVM> ProjectConfigurations { get; set; }
    }
}
