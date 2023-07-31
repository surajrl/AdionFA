using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MarketData;

namespace AdionFA.TransferObject.Common
{
    public class GlobalScheduleConfigurationDTO : EntityBaseDTO
    {
        public int GlobalScheduleConfigurationId { get; set; }

        public int GlobalConfigurationId { get; set; }
        public GlobalConfigurationDTO GlobalConfiguration { get; set; }

        public int MarketRegionId { get; set; }
        public MarketRegionDTO MarketRegion { get; set; }

        public int FromTimeInSeconds { get; set; }
        public int ToTimeInSeconds { get; set; }
    }
}
