using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum NodeAssembledTypeEnum
    {
        [Metadata("Start")]
        Start = 1,

        [Metadata("End")] 
        End = 2,

        [Metadata("Backtest")]
        Backtest = 3,
    }
}
