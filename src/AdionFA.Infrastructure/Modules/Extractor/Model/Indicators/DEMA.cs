using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Attributes;

namespace AdionFA.Infrastructure.Extractor.Model
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
