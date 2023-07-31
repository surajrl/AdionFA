using AdionFA.Domain.Enums;
using System.ComponentModel;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class MAMA : IndicatorBase
    {
        public int PriceType { get; set; }
        public double OptInFastLimit { get; set; }
        public double OptInSlowLimit { get; set; }
        public int MAMAOutput { get; set; }

        public MAMA()
        {
            Type = IndicatorEnum.MAMA;
        }
    }

    public enum MAMAOutputEnum
    {
        [Description("OUTPUT1")]
        OutMama,

        [Description("OUTPUT1")]
        OutFama,
    }
}
