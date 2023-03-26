using AdionFA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectGlobalConfigurationVM : ConfigurationBaseVM
    {
        public int ProjectGlobalConfigurationId { get; set; }

        public IList<ProjectGlobalScheduleConfigurationVM> ProjectGlobalScheduleConfigurations { get; set; }
    }
}
