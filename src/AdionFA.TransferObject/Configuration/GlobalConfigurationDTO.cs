using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Configuration;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Common
{
    public class GlobalConfigurationDTO : ConfigurationBaseDTO
    {
        public int GlobalConfigurationId { get; set; }

        // Builder configuration

        public int NodeBuilderConfigurationId { get; set; }
        public NodeBuilderConfigurationDTO NodeBuilderConfiguration { get; set; }

        public int AssemblyBuilderConfigurationId { get; set; }
        public AssemblyBuilderConfigurationDTO AssemblyBuilderConfiguration { get; set; }

        public int CrossingBuilderConfigurationId { get; set; }
        public CrossingBuilderConfigurationDTO CrossingBuilderConfiguration { get; set; }

        // Navigation

        public ICollection<GlobalScheduleConfigurationDTO> GlobalScheduleConfigurations { get; set; }
    }
}
