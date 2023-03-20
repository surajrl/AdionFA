using Adion.FA.Infrastructure.Enums.Attributes;

namespace Adion.FA.Infrastructure.Enums
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
