using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class MINUS_DI : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public MINUS_DI()
        {
            Type = IndicatorEnum.MINUS_DI;
            OptInTimePeriod = 14;
        }
    }
}
