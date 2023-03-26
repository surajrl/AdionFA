using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Market;

namespace AdionFA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectGlobalScheduleConfigurationVM : TimeSensitiveBaseVM
    {
        public int ProjectGlobalScheduleConfigurationId { get; set; }

        public int ProjectGlobalConfigurationId { get; set; }
        public ProjectGlobalConfigurationVM ProjectGlobalConfiguration { get; set; }

        public int? MarketRegionId { get; set; }
        public MarketRegionVM MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
