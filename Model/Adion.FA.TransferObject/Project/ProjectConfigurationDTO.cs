using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Market;
using System.Collections.Generic;

namespace Adion.FA.TransferObject.Project
{
    public class ProjectConfigurationDTO : ConfigurationBaseDTO
    {
        public int ProjectConfigurationId { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }

        public int? MarketDataId { get; set; }
        public MarketDataDTO MarketData { get; set; }

        public bool IsFavorite { get; set; }
        public string WorkspacePath { get; set; }

        public IList<ProjectScheduleConfigurationDTO> ProjectScheduleConfigurations { get; set; }
    }
}
