using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Market;

namespace Adion.FA.UI.Station.Infrastructure.Model.Project
{
    public class ProjectScheduleConfigurationVM : TimeSensitiveBaseVM
    {
        public int ProjectScheduleConfigurationId { get; set; }

        public int ProjectConfigurationId { get; set; }
        public ProjectConfigurationVM ProjectConfiguration { get; set; }

        public int? MarketRegionId { get; set; }
        public MarketRegionVM MarketRegion { get; set; }

        public int? FromTimeInSeconds { get; set; }
        public int? ToTimeInSeconds { get; set; }
    }
}
