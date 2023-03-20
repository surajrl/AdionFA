using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;
using System.ComponentModel;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class STOCHRSI : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        [IndicatorPeriod]
        public int OptInFastKPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInFastDPeriod { get; set; }
        public int OptInFastDMAType { get; set; }
        public int STOCHRSIOutput { get; set; }

        public STOCHRSI()
        {
            Type = IndicatorEnum.STOCHRSI;
            OptInTimePeriod = 14;
            OptInFastKPeriod = 5;
            OptInFastDPeriod = 3;
            OptInFastDMAType = (int)MATypeEnum.SMA;
        }
    }

    public enum STOCHRSIOutputEnum
    {
        [Description("OUTPUT1")]
        FastK = 1,
        [Description("OUTPUT2")]
        FastD = 2,
    }
}
