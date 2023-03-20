using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class NATR : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public NATR()
        {
            Type = IndicatorEnum.NATR;
        }
    }
}
