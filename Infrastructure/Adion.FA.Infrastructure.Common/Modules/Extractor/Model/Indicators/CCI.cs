using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
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
