using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class CMO : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public CMO()
        {
            Type = IndicatorEnum.CMO;
            OptInTimePeriod = 14;
        }
    }
}
