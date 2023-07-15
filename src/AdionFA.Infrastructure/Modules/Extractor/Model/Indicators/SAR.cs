using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
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
