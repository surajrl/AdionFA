using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
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
