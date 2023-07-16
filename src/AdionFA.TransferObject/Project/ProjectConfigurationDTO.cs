using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectConfigurationDTO : ConfigurationBaseDTO
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }

        public int? HistoricalDataId { get; set; }
        public HistoricalDataDTO HistoricalData { get; set; }

        public string WorkspacePath { get; set; }

        public IList<ProjectScheduleConfigurationDTO> ProjectScheduleConfigurations { get; set; }
    }
}
