using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum MarketRegionEnum
    {
        [Metadata("America", "America", resourceType: typeof(EnumResources))]
        America = 1,

        [Metadata("Europe", "Europe", resourceType: typeof(EnumResources))]
        Europe = 2,

        [Metadata("Asia", "Asia", resourceType: typeof(EnumResources))]
        Asia = 3
    }
}
