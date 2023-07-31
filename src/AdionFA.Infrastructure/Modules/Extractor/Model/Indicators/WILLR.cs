using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class WILLR : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public WILLR()
        {
            Type = IndicatorEnum.WILLR;
            OptInTimePeriod = 14;
        }
    }
}
