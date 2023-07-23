using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectScheduleConfigurationVM : EntityBaseVM
    {
        public int ProjectScheduleConfigurationId { get; set; }

        public int ProjectConfigurationId { get; set; }
        public ProjectConfigurationVM ProjectConfiguration { get; set; }

        public int MarketRegionId { get; set; }
        public MarketRegionVM MarketRegion { get; set; }

        public int FromTimeInSeconds { get; set; }
        public int ToTimeInSeconds { get; set; }
    }
}