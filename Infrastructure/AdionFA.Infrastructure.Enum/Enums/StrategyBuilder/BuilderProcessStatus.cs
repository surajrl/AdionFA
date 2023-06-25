using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum BuilderProcessStatus
    {
        [Metadata(descriptionKey: "Strategy Builder Not Started")]
        SBNotStarted = 0,

        [Metadata(descriptionKey: "Assembly Builder Not Started")]
        ABNotStarted,

        [Metadata(descriptionKey: "Correlation Builder Not Started")]
        CBNotStarted,

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

        [Metadata(descriptionKey: "Assembly Builder Completed")]
        ABCompleted,

        [Metadata(descriptionKey: "Correlation Builder Completed")]
        CBCompleted,

        [Metadata(descriptionKey: "Stopped")]
        Stopped,

        [Metadata(descriptionKey: "Canceled")]
        Canceled,
    }
}