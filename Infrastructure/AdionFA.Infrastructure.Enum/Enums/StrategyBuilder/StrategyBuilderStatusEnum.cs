using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum StrategyBuilderStatusEnum
    {
        [Metadata(nameKey: "Not Started", descriptionKey: "")]
        NoStarted = 0,

        [Metadata(nameKey: "Beginning", descriptionKey: "Preparing Start Of Execution")]
        Beginning = 1,

        [Metadata(nameKey: "Executing Weka", descriptionKey: "Generating Weka Tree")]
        ExecutingWeka = 2,

        [Metadata(nameKey: "Weka Completed")]
        WekaCompleted = 3,

        [Metadata(nameKey: "Executing Backtests")]
        ExecutingBacktests = 4,

        [Metadata(nameKey: "Completed", descriptionKey: "Strategy Builder Completed")]
        StrategyBuilderCompleted = 5,

        [Metadata(nameKey: "Suspended", descriptionKey: "Strategy Builder Canceled")]
        Suspended = 6,

        [Metadata(nameKey: "Error")]
        Error = 7,
    }
}