using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Attributes;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class ADXR : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public ADXR()
        {
            Type = IndicatorEnum.ADXR;
            OptInTimePeriod = 14;
        }
    }
}
