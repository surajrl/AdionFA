using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
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
