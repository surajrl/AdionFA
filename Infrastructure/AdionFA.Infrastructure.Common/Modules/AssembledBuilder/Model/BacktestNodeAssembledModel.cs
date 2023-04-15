using AdionFA.Infrastructure.Common.StrategyBuilder.Model;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public class BacktestNodeAssembledModel : NodeAssembledModel
    {
        public override string Label => Backtest?.Label;
        public override string Name => Backtest?.NodeName();
        public BacktestModel Backtest { get; set; }
    }
}
