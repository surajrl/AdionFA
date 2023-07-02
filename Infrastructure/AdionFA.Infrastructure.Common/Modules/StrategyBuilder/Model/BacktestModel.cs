using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Model
{
    public class BacktestModel
    {
        public BacktestModel()
        {
        }

        public BacktestModel(BacktestModel backtestOS)
        {
            FromDate = backtestOS.FromDate;
            ToDate = backtestOS.ToDate;
            TimeframeId = backtestOS.TimeframeId;
            CorrelationPass = backtestOS.CorrelationPass;
            TotalOpportunity = backtestOS.TotalOpportunity;
            TotalTrades = backtestOS.TotalTrades;
            WinningTrades = backtestOS.WinningTrades;
            LosingTrades = backtestOS.LosingTrades;
            BacktestOperations = backtestOS.BacktestOperations;
        }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TimeframeId { get; set; }

        public bool CorrelationPass { get; set; }

        public int TotalOpportunity { get; set; }
        public int TotalTrades { get; set; }
        public int WinningTrades { get; set; }
        public int LosingTrades { get; set; }

        public double SuccessRatePercent => TotalTrades > 0 && WinningTrades > 0 ? WinningTrades * 100 / TotalTrades : 0;
        public double Progressiveness => TotalTrades > 0 && TotalOpportunity > 0 ? TotalTrades * 100 / TotalOpportunity : 0;

        public List<BacktestOperationModel> BacktestOperations { get; set; }
    }
}