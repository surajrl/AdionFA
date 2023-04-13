using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum StrategyBuilderStatusEnum
    {
        [Metadata("Not Started", descriptionKey: "")]
        NoStarted = 0,

        [Metadata("Beginning", descriptionKey: "Preparing Start Of Execution")]
        Beginning = 1,

        [Metadata("Executing Weka", descriptionKey: "Generating Weka Tree")]
        ExecutingWeka = 2,

        [Metadata("Completed", descriptionKey: "Strategy Builder Completed")]
        Completed = 3,

        [Metadata("Suspended", descriptionKey: "Strategy Builder Canceled")]
        Suspended = 4,

        [Metadata("Error")]
        Error = 5,
    }
}
