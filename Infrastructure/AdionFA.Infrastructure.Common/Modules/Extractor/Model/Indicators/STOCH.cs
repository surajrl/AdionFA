using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class STOCH : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInFastKPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInSlowKPeriod { get; set; }

        public int OptInSlowKMAType { get; set; }

        [IndicatorPeriod]
        public int OptInSlowDPeriod { get; set; }
        public int OptInSlowDMAType { get; set; }

        public STOCH()
        {
            Type = IndicatorEnum.STOCH;
            OptInFastKPeriod = 5;
            OptInSlowKPeriod = 3;
            OptInSlowKMAType = (int)MATypeEnum.SMA;
            OptInSlowDPeriod = 3;
            OptInSlowDMAType = (int)MATypeEnum.SMA;
        }
    }
}
