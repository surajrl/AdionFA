using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
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
