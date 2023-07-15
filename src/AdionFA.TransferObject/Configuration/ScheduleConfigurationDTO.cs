using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;

namespace AdionFA.TransferObject.Common
{
    public class ScheduleConfigurationDTO : TimeSensitiveBaseDTO
    {
        public int ScheduleConfigurationId { get; set; }

        public int ConfigurationId { get; set; }
        public ConfigurationDTO Configuration { get; set; }

        public int? MarketRegionId { get; set; }
        public MarketRegionDTO MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
