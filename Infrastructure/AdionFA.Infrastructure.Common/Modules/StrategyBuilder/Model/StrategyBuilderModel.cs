using System;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Model
{
    public class StrategyBuilderModel
    {
        public bool WinningStrategy { get; set; }

        public BacktestModel IS { get; set; }
        public BacktestModel OS { get; set; }

        public double SuccessRateVariation => IS != null && OS != null ? Math.Abs(IS.SuccessRatePercent - OS.SuccessRatePercent) : 0;
        public double ProgressivenessVariation => IS != null && OS != null ? Math.Abs(IS.Progressiveness - OS.Progressiveness) : 0;
    }
}
