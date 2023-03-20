using Adion.FA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace Adion.FA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectGlobalConfigurationVM : ConfigurationBaseVM
    {
        public int ProjectGlobalConfigurationId { get; set; }

        public IList<ProjectGlobalScheduleConfigurationVM> ProjectGlobalScheduleConfigurations { get; set; }
    }
}
