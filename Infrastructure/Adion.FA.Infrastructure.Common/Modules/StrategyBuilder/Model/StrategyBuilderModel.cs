using System;

namespace Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model
{
    public class StrategyBuilderModel
    {
        public bool WinningStrategy { get; set; }

        public BacktestModel IS { get; set; }
        public BacktestModel OS { get; set; }

        public double VariationPercent => IS != null && OS != null ? Math.Abs(IS.PercentSuccess - OS.PercentSuccess) : 0;
        public double Progressiveness => IS != null && OS != null ? Math.Abs(IS.Progressiveness - OS.Progressiveness) : 0;
    }
}
