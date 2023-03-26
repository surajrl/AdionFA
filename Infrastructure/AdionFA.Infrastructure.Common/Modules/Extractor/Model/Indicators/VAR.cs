using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class VAR : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public VAR()
        {
            Type = IndicatorEnum.VAR;
            PriceType = (int)PriceTypeEnum.HIGH_PRICE;
            OptInTimePeriod = 5;
        }
    }
}
