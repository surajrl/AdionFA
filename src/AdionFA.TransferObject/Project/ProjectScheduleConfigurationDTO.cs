using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;

namespace AdionFA.TransferObject.Project
{
    public class ProjectScheduleConfigurationDTO : TimeSensitiveBaseDTO
    {
        public int ProjectScheduleConfigurationId { get; set; }

        public int ProjectConfigurationId { get; set; }
        public ProjectConfigurationDTO ProjectConfiguration { get; set; }

        public int? MarketRegionId { get; set; }
        public MarketRegionDTO MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
