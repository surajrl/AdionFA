using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
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
