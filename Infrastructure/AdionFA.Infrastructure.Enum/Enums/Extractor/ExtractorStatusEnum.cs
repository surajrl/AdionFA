using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum ExtractorStatusEnum
    {
        [Metadata("Not Started", descriptionKey: "")]
        NoStarted = 0,

        [Metadata("Beginning", descriptionKey: "Preparing Start Of Execution")]
        Beginning = 1,

        [Metadata("Executing", descriptionKey: "Running Extraction")]
        Executing = 2,

        [Metadata("Completed", descriptionKey: "Extraction Completed")]
        Completed = 3,

        [Metadata("Suspended", descriptionKey: "Extraction Canceled")]
        Suspended = 4,

        [Metadata("Error")]
        Error = 5
    }
}
