using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;

namespace Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model
{
    public class BacktestNodeAssembledModel : NodeAssembledModel
    {
        public override string Label => Backtest?.Label;
        public override string Name => Backtest?.NodeName();
        public BacktestModel Backtest { get; set; }
    }
}
