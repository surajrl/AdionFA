using Adion.FA.Infrastructure.Enums.Attributes;
using Adion.FA.Infrastructure.I18n.Resources;

namespace Adion.FA.Infrastructure.Enums
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
