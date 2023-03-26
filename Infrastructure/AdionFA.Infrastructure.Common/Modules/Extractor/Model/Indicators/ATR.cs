using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class ATR : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public ATR()
        {
            Type = IndicatorEnum.ATR;
            OptInTimePeriod = 14;
        }
    }
}
