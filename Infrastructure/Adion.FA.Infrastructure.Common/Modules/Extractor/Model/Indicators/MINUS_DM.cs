using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
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
