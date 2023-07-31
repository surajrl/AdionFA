using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class KAMA : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public KAMA()
        {
            Type = IndicatorEnum.KAMA;
        }
    }
}
