using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;

namespace AdionFA.TransferObject.Project
{
    public class ProjectGlobalScheduleConfigurationDTO : TimeSensitiveBaseDTO
    {
        public int ProjectGlobalScheduleConfigurationId { get; set; }

        public int ProjectGlobalConfigurationId { get; set; }
        public ProjectGlobalConfigurationDTO ProjectGlobalConfiguration { get; set; }

        public int? MarketRegionId { get; set; }
        public MarketRegionDTO MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
