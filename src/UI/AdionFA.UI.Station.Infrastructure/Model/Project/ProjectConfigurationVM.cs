using AdionFA.UI.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectConfigurationVM : ConfigurationBaseVM
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectVM Project { get; set; }

        public IList<ProjectScheduleConfigurationVM> ProjectScheduleConfigurations { get; set; }
    }
}