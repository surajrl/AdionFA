using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum AssembledBuilderStatus
    {
        [Metadata(nameKey: "Not Started", descriptionKey: "Assembled Builder Not Started")]
        NotStarted = 0,

        [Metadata(nameKey: "Executing Extraction", descriptionKey: "Executing Extraction")]
        ExecutingExtraction,

        [Metadata(nameKey: "Extraction Completed", descriptionKey: "Extraction Completed")]
        ExtractionCompleted,

        [Metadata(nameKey: "Executing Weka", descriptionKey: "Executing Weka Tree")]
        ExecutingWeka,

        [Metadata(nameKey: "Weka Completed", descriptionKey: "Weka Tree Completed")]
        WekaCompleted,

        [Metadata(nameKey: "Executing Backtest", descriptionKey: "Executing Backtest")]
        ExecutingBacktest,

        [Metadata(nameKey: "Backtest Completed", descriptionKey: "Backtest Completed")]
        BacktestCompleted,

        [Metadata(nameKey: "Stopped", descriptionKey: "Stopped")]
        Stopped,

        [Metadata(nameKey: "Canceled", descriptionKey: "Canceled")]
        Canceled,

    }
}
