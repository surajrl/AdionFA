using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum MarketEnum
    {
        [Metadata(codeKey: "Forex", nameKey: "Forex", resourceType: typeof(EnumResources))]
        Forex = 1,
    }
}
