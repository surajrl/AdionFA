using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Attributes;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class DX : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public DX()
        {
            Type = IndicatorEnum.DX;
            OptInTimePeriod = 14;
        }
    }
}
