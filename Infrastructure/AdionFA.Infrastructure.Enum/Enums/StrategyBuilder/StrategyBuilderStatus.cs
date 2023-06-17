using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum StrategyBuilderStatus
    {
        [Metadata(descriptionKey: "Strategy Builder Not Started")]
        NotStarted = 0,

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
        Completed,

        [Metadata(descriptionKey: "Stopped")]
        Stopped,

        [Metadata(descriptionKey: "Canceled")]
        Canceled,
    }
}