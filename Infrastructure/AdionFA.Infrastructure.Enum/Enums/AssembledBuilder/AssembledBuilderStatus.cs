using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum AssembledBuilderStatus
    {
        [Metadata(descriptionKey: "Assembled Builder Not Started")]
        NotStarted = 0,

        [Metadata(descriptionKey: "Executing Extraction")]
        ExecutingExtraction,

        [Metadata(descriptionKey: "Extraction Completed")]
        ExtractionCompleted,

        [Metadata(descriptionKey: "Executing Weka")]
        ExecutingWeka,

        [Metadata(descriptionKey: "Weka Completed")]
        WekaCompleted,

        [Metadata(descriptionKey: "Executing Backtest")]
        ExecutingBacktest,

        [Metadata(descriptionKey: "Backtest Completed")]
        BacktestCompleted,

        [Metadata(descriptionKey: "Stopped")]
        Stopped,

        [Metadata(descriptionKey: "Canceled")]
        Canceled,

    }
}
