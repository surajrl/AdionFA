using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class VAR : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public VAR()
        {
            Type = IndicatorEnum.VAR;
            PriceType = (int)PriceTypeEnum.HIGH;
            OptInTimePeriod = 5;
        }
    }
}
