using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum BuilderProcessStatus
    {
        [Metadata(Name = "Strategy Builder Not Started")]
        SBNotStarted = 0,

        [Metadata(Name = "Assembly Builder Not Started")]
        ABNotStarted,

        [Metadata(Name = "Correlation Builder Not Started")]
        CBNotStarted,

        [Metadata(Name = "Executing Extraction")]
        ExecutingExtraction,

        [Metadata(Name = "Extraction Completed")]
        ExtractionCompleted,

        [Metadata(Name = "Executing Weka")]
        ExecutingWeka,

        [Metadata(Name = "Weka Completed")]
        WekaCompleted,

        [Metadata(Name = "Executing Backtest")]
        ExecutingBacktest,

        [Metadata(Name = "Backtest Completed")]
        BacktestCompleted,

        [Metadata(Name = "Executing Correlation Analysis")]
        ExecutingCorrelation,

        [Metadata(Name = "Strategy Builder Completed")]
        SBCompleted,

        [Metadata(Name = "Assembly Builder Completed")]
        ABCompleted,

        [Metadata(Name = "Correlation Builder Completed")]
        CBCompleted,

        [Metadata(Name = "Stopped")]
        Stopped,

        [Metadata(Name = "Canceled")]
        Canceled,
    }
}