using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum EntityTypeEnum
    {
        [Metadata(codeKey: "SETT", nameKey: "Setting", descriptionKey: "Setting", resourceType: typeof(EnumResources))]
        Setting = 1,

        [Metadata(codeKey: "MKDT", nameKey: "MarketData", descriptionKey: "Market Data", resourceType: typeof(EnumResources))]
        MarketData = 400,

        [Metadata(codeKey: "PROJ", nameKey: "Project", descriptionKey: "Project", resourceType: typeof(EnumResources))]
        Project = 500,

        [Metadata(codeKey: "PROJCONFIG", nameKey: "ProjectConfiguration", descriptionKey: "Project Configuration", resourceType: typeof(EnumResources))]
        ProjectConfiguration = 501,

        [Metadata(codeKey: "CONFIG", nameKey: "ProjectGlobalConfiguration", descriptionKey: "Project Global Configuration", resourceType: typeof(EnumResources))]
        ProjectGlobalConfiguration = 502,

        [Metadata(codeKey: "EXT", nameKey: "Extractor", descriptionKey: "Extractor", resourceType: typeof(EnumResources))]
        Extractor = 503,

        [Metadata(codeKey: "SB", nameKey: "StrategyBuilder", descriptionKey: "Strategy Builder", resourceType: typeof(EnumResources))]
        StrategyBuilder = 504,

        [Metadata(codeKey: "AB", nameKey: "AssembledBuilder", descriptionKey: "Assembled Builder", resourceType: typeof(EnumResources))]
        AssembledBuilder = 505,
    }
}
