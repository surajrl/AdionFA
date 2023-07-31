using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class T3 : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }
        public double OptInVFactor { get; set; }

        public T3() => Type = IndicatorEnum.T3;
    }
}
