using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class PPO : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInFastPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInSlowPeriod { get; set; }
        public int MAType { get; set; }

        public PPO()
        {
            Type = IndicatorEnum.PPO;
            OptInFastPeriod = 12;
            OptInSlowPeriod = 26;
        }
    }
}
