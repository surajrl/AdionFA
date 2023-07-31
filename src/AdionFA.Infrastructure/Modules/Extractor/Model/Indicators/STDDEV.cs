using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class STDDEV : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }
        public double OptInNbDev { get; set; }

        public STDDEV()
        {
            Type = IndicatorEnum.STDDEV;
            OptInTimePeriod = 5;
            OptInNbDev = 1;
        }
    }
}
