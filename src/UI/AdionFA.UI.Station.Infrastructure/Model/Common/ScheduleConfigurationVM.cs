using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;

namespace AdionFA.UI.Station.Infrastructure.Model.Common
{
    public class ScheduleConfigurationVM : TimeSensitiveBaseVM
    {
        public int ScheduleConfigurationId { get; set; }

        public int ConfigurationId { get; set; }
        public ConfigurationVM Configuration { get; set; }

        public int? MarketRegionId { get; set; }
        public MarketRegionVM MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}