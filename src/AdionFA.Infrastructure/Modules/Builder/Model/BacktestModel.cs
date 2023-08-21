using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public class BacktestModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TimeframeId { get; set; }

        public bool CorrelationPass { get; set; }

        public int TotalOpportunity { get; set; }
        public int TotalTrades { get; set; }
        public int WinningTrades { get; set; }
        public int LosingTrades { get; set; }

        public decimal SuccessRatePercent => TotalTrades > 0 && WinningTrades > 0 ? WinningTrades * 100 / TotalTrades : 0;
        public decimal Progressiveness => TotalTrades > 0 && TotalOpportunity > 0 ? TotalTrades * 100 / TotalOpportunity : 0;

        public List<BacktestOperationModel> BacktestOperations { get; set; }
    }
}