using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.NodeBuilder.Model;
using Prism.Mvvm;
using System;

namespace AdionFA.Infrastructure.Modules.Strategy
{
    public class NodeMetadataModel : BindableBase
    {
        private bool _winningStrategy;
        public bool WinningStrategy
        {
            get => _winningStrategy;
            set => SetProperty(ref _winningStrategy, value);
        }

        private BacktestModel _backtestIS;
        public BacktestModel BacktestIS
        {
            get => _backtestIS;
            set
            {
                if (SetProperty(ref _backtestIS, value))
                {
                    SuccessRateVariation = Math.Abs(BacktestIS?.SuccessRatePercent - BacktestOS?.SuccessRatePercent ?? 0);
                }
            }
        }

        private BacktestStatus _backtestStatusIS;
        public BacktestStatus BacktestStatusIS
        {
            get => _backtestStatusIS;
            set => SetProperty(ref _backtestStatusIS, value);
        }

        private BacktestModel _backtestOS;
        public BacktestModel BacktestOS
        {
            get => _backtestOS;
            set
            {
                if (SetProperty(ref _backtestOS, value))
                {
                    SuccessRateVariation = Math.Abs(BacktestIS?.SuccessRatePercent - BacktestOS?.SuccessRatePercent ?? 0);
                }
            }
        }

        private BacktestStatus _backtestStatusOS;
        public BacktestStatus BacktestStatusOS
        {
            get => _backtestStatusOS;
            set => SetProperty(ref _backtestStatusOS, value);
        }

        private decimal _successRateVariation;
        public decimal SuccessRateVariation
        {
            get => _successRateVariation;
            set => SetProperty(ref _successRateVariation, value);
        }

        private decimal _progressivenessVariation;
        public decimal ProgressivenessVariation
        {
            get => _progressivenessVariation;
            set => SetProperty(ref _progressivenessVariation, value);
        }
    }
}
