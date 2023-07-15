using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
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
