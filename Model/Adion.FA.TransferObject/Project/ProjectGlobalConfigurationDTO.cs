using Adion.FA.TransferObject.Base;
using System.Collections.Generic;

namespace Adion.FA.TransferObject.Project
{
    public class ProjectGlobalConfigurationDTO : ConfigurationBaseDTO
    {
        public int ProjectGlobalConfigurationId { get; set; }

        public IList<ProjectGlobalScheduleConfigurationDTO> ProjectGlobalScheduleConfigurations { get; set; }
    }
}
