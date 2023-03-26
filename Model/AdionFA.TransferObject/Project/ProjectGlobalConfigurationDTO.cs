using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectGlobalConfigurationDTO : ConfigurationBaseDTO
    {
        public int ProjectGlobalConfigurationId { get; set; }

        public IList<ProjectGlobalScheduleConfigurationDTO> ProjectGlobalScheduleConfigurations { get; set; }
    }
}
