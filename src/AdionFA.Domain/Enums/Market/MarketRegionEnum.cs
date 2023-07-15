using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum MarketRegionEnum
    {
        [Metadata("America", "America")]
        America = 1,

        [Metadata("Europe", "Europe")]
        Europe = 2,

        [Metadata("Asia", "Asia")]
        Asia = 3
    }
}
