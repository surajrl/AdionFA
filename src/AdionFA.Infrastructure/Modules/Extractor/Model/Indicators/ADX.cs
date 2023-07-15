using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Attributes;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class ADX : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public ADX()
        {
            Type = IndicatorEnum.ADX;
            OptInTimePeriod = 14;
        }
    }
}
