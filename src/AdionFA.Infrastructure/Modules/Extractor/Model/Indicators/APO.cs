using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Attributes;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class APO : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInFastPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInSlowPeriod { get; set; }
        public int MAType { get; set; }

        public APO()
        {
            Type = IndicatorEnum.APO;
            OptInFastPeriod = 12;
            OptInSlowPeriod = 26;
            MAType = (int)MATypeEnum.SMA;
        }
    }
}
