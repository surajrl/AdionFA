using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class ULTOSC : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod1 { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod2 { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod3 { get; set; }

        public ULTOSC()
        {
            Type = IndicatorEnum.ULTOSC;
            OptInTimePeriod1 = 7;
            OptInTimePeriod2 = 14;
            OptInTimePeriod3 = 28;
        }
    }
}
