using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class CCI : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public CCI()
        {
            Type = IndicatorEnum.CCI;
            OptInTimePeriod = 14;
        }
    }
}
