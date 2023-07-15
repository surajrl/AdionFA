using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum EntityTypeEnum
    {
        [Metadata(codeKey: "SETT", nameKey: "Setting", descriptionKey: "Setting")]
        Setting = 1,

        [Metadata(codeKey: "MKDT", nameKey: "MarketData", descriptionKey: "Market Data")]
        MarketData = 400,

        [Metadata(codeKey: "PROJ", nameKey: "Project", descriptionKey: "Project")]
        Project = 500,

        [Metadata(codeKey: "PROJCONFIG", nameKey: "ProjectConfiguration", descriptionKey: "Project Configuration")]
        ProjectConfiguration = 501,

        [Metadata(codeKey: "CONFIG", nameKey: "Configuration", descriptionKey: "Configuration")]
        Configuration = 502,

        [Metadata(codeKey: "EXT", nameKey: "Extractor", descriptionKey: "Extractor")]
        Extractor = 503,

        [Metadata(codeKey: "SB", nameKey: "StrategyBuilder", descriptionKey: "Strategy Builder")]
        StrategyBuilder = 504,

        [Metadata(codeKey: "AB", nameKey: "AssemblyBuilder", descriptionKey: "Assembly Builder")]
        AssemblyBuilder = 505,

        [Metadata(codeKey: "CB", nameKey: "CrossingBuilder", descriptionKey: "Crossing Builder")]
        CrossingBuilder = 506,
    }
}
