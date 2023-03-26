using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class DEMA : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public DEMA()
        {
            Type = IndicatorEnum.DEMA;
        }
    }
}
