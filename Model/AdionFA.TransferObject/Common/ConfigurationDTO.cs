using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Common
{
    public class ConfigurationDTO : ConfigurationBaseDTO
    {
        public int ConfigurationId { get; set; }
        public IList<ScheduleConfigurationDTO> ScheduleConfigurations { get; set; }

    }
}
