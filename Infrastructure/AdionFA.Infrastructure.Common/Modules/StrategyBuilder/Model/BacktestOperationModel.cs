using System;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Model
{
    public class BacktestOperationModel
    {
        public DateTime Date { get; set; }
        public bool IsWinner { get; set; }
    }
}
