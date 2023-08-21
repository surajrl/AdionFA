using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public interface INodeModel
    {
        string Name { get; }

        Label Label { get; }

        string WinningUPDirectory { get; }
        string WinningDOWNDirectory { get; }

        public bool WinningStrategy { get; set; }

        public BacktestModel BacktestIS { get; set; }

        public BacktestStatus BacktestStatusIS { get; set; }

        public BacktestModel BacktestOS { get; set; }

        public BacktestStatus BacktestStatusOS { get; set; }

        public decimal SuccessRateVariation { get; set; }

        public decimal ProgressivenessVariation { get; set; }
    }
}
