using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectConfigurationDTO : ConfigurationBaseDTO
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }

        public IList<ProjectScheduleConfigurationDTO> ProjectScheduleConfigurations { get; set; }
    }
}
