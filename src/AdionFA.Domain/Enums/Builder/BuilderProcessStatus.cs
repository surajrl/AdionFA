using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum BuilderProcessStatus
    {

        [Metadata(Name = "Executing Extraction")]
        ExecutingExtraction = 0,

        [Metadata(Name = "Extraction Completed")]
        ExtractionCompleted,

        [Metadata(Name = "Executing Weka")]
        ExecutingWeka,

        [Metadata(Name = "Weka Completed")]
        WekaCompleted,

        [Metadata(Name = "Node Builder Not Started")]
        NBNotStarted,

        [Metadata(Name = "Node Builder Completed")]
        NBCompleted,

        [Metadata(Name = "Assembly Builder Not Started")]
        ABNotStarted,

        [Metadata(Name = "Assembly Builder Completed")]
        ABCompleted,

        [Metadata(Name = "Crossing Builder Not Started")]
        CBNotStarted,

        [Metadata(Name = "Crossing Builder Completed")]
        CBCompleted,

        [Metadata(Name = "Executing Backtest")]
        ExecutingBacktest,

        [Metadata(Name = "Backtest Completed")]
        BacktestCompleted,

        [Metadata(Name = "Executing Correlation Analysis")]
        ExecutingCorrelation,
    }
}