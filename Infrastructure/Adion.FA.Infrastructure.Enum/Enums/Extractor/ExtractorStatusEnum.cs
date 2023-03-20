using Adion.FA.Infrastructure.Enums.Attributes;

namespace Adion.FA.Infrastructure.Enums
{
    public enum ExtractorStatusEnum
    {
        [Metadata("Not Started", descriptionKey:"")]
        NoStarted,

        [Metadata("Beginning", descriptionKey: "Preparing Start Of Execution")]
        Beginning,

        [Metadata("Executing", descriptionKey: "Running Extraction")]
        Executing,

        [Metadata("Completed", descriptionKey: "Extraction Completed")]
        Completed,

        [Metadata("Suspended", descriptionKey: "Extraction Canceled")]
        Suspended,

        [Metadata("Error")]
        Error,
    }
}
