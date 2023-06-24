using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum StrategyBuilderStatus
    {
        [Metadata(descriptionKey: "Strategy Builder Not Started")]
        SBNotStarted = 0,

        [Metadata(descriptionKey: "Assembled Builder Not Started")]
        ABNotStarted,

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

        [Metadata(descriptionKey: "Executing Correlation Analysis")]
        ExecutingCorrelation,

        [Metadata(descriptionKey: "Strategy Builder Completed")]
        SBCompleted,

        [Metadata(descriptionKey: "Assembled Builder Completed")]
        ABCompleted,

        [Metadata(descriptionKey: "Stopped")]
        Stopped,

        [Metadata(descriptionKey: "Canceled")]
        Canceled,
    }
}