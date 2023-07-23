using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum EntityTypeEnum
    {
        [Metadata(codeKey: "SETT", nameKey: "Setting")]
        Setting = 1,

        [Metadata(codeKey: "HD", nameKey: "Historical Data")]
        HistoricalData = 400,

        [Metadata(codeKey: "PROJECT", nameKey: "Project")]
        Project = 500,

        [Metadata(codeKey: "PROJECTCONFIG", nameKey: "Project Configuration")]
        ProjectConfiguration = 501,

        [Metadata(codeKey: "GLOBALCONFIG", nameKey: "Global Configuration")]
        GlobalConfiguration = 502,

        [Metadata(codeKey: "EXT", nameKey: "Extractor")]
        Extractor = 503,

        [Metadata(codeKey: "SB", nameKey: "Strategy Builder")]
        StrategyBuilder = 504,

        [Metadata(codeKey: "AB", nameKey: "Assembly Builder")]
        AssemblyBuilder = 505,

        [Metadata(codeKey: "CB", nameKey: "Crossing Builder")]
        CrossingBuilder = 506,
    }
}
