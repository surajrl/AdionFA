using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class MINUS_DM : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public MINUS_DM()
        {
            Type = IndicatorEnum.MINUS_DM;
            OptInTimePeriod = 14;
        }
    }
}
