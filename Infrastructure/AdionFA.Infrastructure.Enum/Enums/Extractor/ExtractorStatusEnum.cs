using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum ExtractorStatusEnum
    {
        [Metadata(nameKey: "Not Started", descriptionKey: "")]
        NoStarted = 0,

        [Metadata(nameKey: "Beginning", descriptionKey: "Preparing Start Of Execution")]
        Beginning = 1,

        [Metadata(nameKey: "Executing", descriptionKey: "Running Extraction")]
        Executing = 2,

        [Metadata(nameKey: "Completed", descriptionKey: "Extraction Completed")]
        Completed = 3,

        [Metadata(nameKey: "Suspended", descriptionKey: "Extraction Canceled")]
        Suspended = 4,

        [Metadata(nameKey: "Error")]
        Error = 5
    }
}