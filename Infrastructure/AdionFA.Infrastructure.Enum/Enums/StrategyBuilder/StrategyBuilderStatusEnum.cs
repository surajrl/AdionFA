using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum StrategyBuilderStatusEnum
    {
        [Metadata(nameKey: "Not Started", descriptionKey: "")]
        NotStarted = 0,

        [Metadata(nameKey: "Executing", descriptionKey: "Strategy Builder Executing")]
        Executing,

        [Metadata(nameKey: "Completed", descriptionKey: "Strategy Builder Completed")]
        Completed,

        [Metadata(nameKey: "Stopped", descriptionKey: "Strategy Builder Stopped")]
        Stopped,

        [Metadata(nameKey: "Canceled", descriptionKey: "Strategy Builder Canceled")]
        Canceled,

        [Metadata(nameKey: "Error")]
        Error,
    }
}