using System;

namespace AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model
{
    public class BacktestOperationModel
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
        public int Pips { get; set; }
        public bool IsWinner { get; set; }
    }
}
