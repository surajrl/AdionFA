using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
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
