using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class MINUS_DI : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public MINUS_DI()
        {
            Type = IndicatorEnum.MINUS_DI;
            OptInTimePeriod = 14;
        }
    }
}
