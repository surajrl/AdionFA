using Adion.FA.Infrastructure.Enums;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class SAR : IndicatorBase
    {
        public double OptInAcceleration { get; set; }
        public double OptInMaximum { get; set; }

        public SAR()
        {
            Type = IndicatorEnum.SAR;
        }
    }
}
