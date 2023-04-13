using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum NodeAssembledTypeEnum
    {
        [Metadata(codeKey: "Start")]
        Start = 1,

        [Metadata(codeKey: "End")]
        End = 2,

        [Metadata(codeKey: "Backtest")]
        Backtest = 3,
    }
}
