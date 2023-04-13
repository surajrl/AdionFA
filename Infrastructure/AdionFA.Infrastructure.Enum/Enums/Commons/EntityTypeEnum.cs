using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum EntityTypeEnum
    {
        // Common 1 - 99

        [Metadata("SETT", "Setting", descriptionKey: "Setting", resourceType: typeof(EnumResources))]
        Setting = 1,

        // Market 400 - 499

        [Metadata("MKDT", "MarketData", descriptionKey: "Market Data", resourceType: typeof(EnumResources))]
        MarketData = 400,

        // Project 500 - 599

        [Metadata("PROJ", "Project", descriptionKey: "Project", resourceType: typeof(EnumResources))]
        Project = 500,

        [Metadata("PROJCONFIG", "ProjectConfiguration", descriptionKey: "Project Configuration", resourceType: typeof(EnumResources))]
        ProjectConfiguration = 501,

        [Metadata("CONFIG", "ProjectGlobalConfiguration", descriptionKey: "Project Global Configuration", resourceType: typeof(EnumResources))]
        ProjectGlobalConfiguration = 502,

        [Metadata("EXT", "Extractor", descriptionKey: "Extractor", resourceType: typeof(EnumResources))]
        Extractor = 503,

        [Metadata("STRBUILD", "StrategyBuilder", descriptionKey: "Strategy Builder", resourceType: typeof(EnumResources))]
        StrategyBuilder = 504,

        [Metadata("ASSBUILD", "AssembledBuilder", descriptionKey: "Assembled Builder", resourceType: typeof(EnumResources))]
        AssembledBuilder = 505,
    }
}
