using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum StrategyBuilderStatusEnum
    {
        [Metadata("Not Started", descriptionKey: "")]
        NoStarted,

        [Metadata("Beginning", descriptionKey: "Preparing Start Of Execution")]
        Beginning,

        [Metadata("Executing Weka", descriptionKey: "Generating Weka Tree")]
        ExecutingWeka,

        [Metadata("Completed", descriptionKey: "Strategy Builder Completed")]
        Completed,

        [Metadata("Suspended", descriptionKey: "Strategy Builder Canceled")]
        Suspended,

        [Metadata("Error")]
        Error,
    }
}
