using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class PLUS_DI : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public PLUS_DI()
        {
            Type = IndicatorEnum.PLUS_DI;
            OptInTimePeriod = 14;
        }
    }
}
