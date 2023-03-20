using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;
using System.ComponentModel;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class MACD : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInFastPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInSlowPeriod { get; set; }

        [IndicatorPeriod]
        public int OptInSignalPeriod { get; set; }
        public int MACDOutput { get; set; }

        public MACD()
        {
            Type = IndicatorEnum.MACD;
            OptInFastPeriod = 12;
            OptInSlowPeriod = 26;
            OptInSignalPeriod = 9;
        }
    }

    public enum MACDOutputEnum
    {
        [Description("OUTPUT1")]
        Macd = 1,
        [Description("OUTPUT2")]
        MacdSignal = 2,
        [Description("OUTPUT3")]
        MacdHist = 3,
    }
}
