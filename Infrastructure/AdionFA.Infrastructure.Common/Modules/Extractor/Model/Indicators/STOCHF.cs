using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Enums;
using System.ComponentModel;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class STOCHF : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInFastKPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInFastDPeriod { get; set; }
        public int OptInFastDMAType { get; set; }
        public int STOCHFOutput { get; set; }

        public STOCHF()
        {
            Type = IndicatorEnum.STOCHF;
            OptInFastKPeriod = 5;
            OptInFastDPeriod = 3;
            OptInFastDMAType = (int)MATypeEnum.SMA;
        }
    }

    public enum STOCHFOutputEnum
    {
        [Description("OUTPUT1")]
        FastK = 1,
        [Description("OUTPUT2")]
        FastD = 2,
    }
}
