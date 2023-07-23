using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;

namespace AdionFA.TransferObject.Project
{
    public class ProjectDTO : EntityBaseDTO
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string WorkspacePath { get; set; }

        public int HistoricalDataId { get; set; }
        public HistoricalDataDTO HistoricalData { get; set; }

        public ProjectConfigurationDTO ProjectConfiguration { get; set; }
    }
}
