using System.ComponentModel;

namespace AdionFA.Domain.Enums
{
    public enum BacktestStatus
    {
        [Description("Not Started")]
        NotStarted = 0,

        [Description("Executing")]
        Executing = 1,

        [Description("Completed")]
        Completed = 2,
    }
}
