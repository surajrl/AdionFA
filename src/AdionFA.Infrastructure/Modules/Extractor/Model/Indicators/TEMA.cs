using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class TEMA : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public TEMA() => Type = IndicatorEnum.TEMA;
    }
}
