using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class PLUS_DM : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public PLUS_DM()
        {
            Type = IndicatorEnum.PLUS_DM;
            OptInTimePeriod = 14;
        }
    }
}
