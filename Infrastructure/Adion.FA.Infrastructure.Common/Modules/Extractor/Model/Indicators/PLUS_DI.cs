using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class PLUS_DI : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public PLUS_DI()
        {
            Type = IndicatorEnum.PLUS_DI;
            OptInTimePeriod = 14;
        }
    }
}
