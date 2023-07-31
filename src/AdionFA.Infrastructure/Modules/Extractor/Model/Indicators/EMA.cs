using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class EMA : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public EMA()
        {
            Type = IndicatorEnum.EMA;
        }
    }
}
