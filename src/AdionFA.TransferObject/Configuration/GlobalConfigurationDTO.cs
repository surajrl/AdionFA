using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Common
{
    public class GlobalConfigurationDTO : ConfigurationBaseDTO
    {
        public int GlobalConfigurationId { get; set; }

        public ICollection<GlobalScheduleConfigurationDTO> GlobalScheduleConfigurations { get; set; }
    }
}
