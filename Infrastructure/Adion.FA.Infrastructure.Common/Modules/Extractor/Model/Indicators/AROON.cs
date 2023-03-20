using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Enums;
using System.ComponentModel;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class AROON : IndicatorBase
    {
        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }
        public int AROONDownUp { get; set; }
        public AROON()
        {
            Type = IndicatorEnum.AROON;
            OptInTimePeriod = 14;
        }
    }

    public enum AROONDownUpEnum
    {
        [Description("OUTPUT1")]
        DOWN = 2,
        [Description("OUTPUT2")]
        UP = 1,
    }
}
