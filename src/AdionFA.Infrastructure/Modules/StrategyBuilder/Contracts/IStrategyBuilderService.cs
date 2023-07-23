using AdionFA.Infrastructure.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Modules.Weka.Model;
using AdionFA.Infrastructure.StrategyBuilder.Model;
using AdionFA.Infrastructure.Weka.Model;
using AdionFA.TransferObject.Base;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.StrategyBuilder.Contracts
{
    public interface IStrategyBuilderService
    {
        // Correlation

        void Correlation(
            string projectName,
            StrategyBuilderModel strategyBuilder,
            decimal maxCorrelation);

        void Correlation(
            string projectName,
            AssemblyBuilderModel assemblyBuilder,
            decimal maxCorrelation);

        // Backtest

        bool BuildBacktestOfNode(
            NodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ConfigurationBaseDTO configuration,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfAssemblyNode(
            AssemblyNodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ConfigurationBaseDTO configuration,
            int timeframeId,
            double meanSuccessRatePercentIS,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> mainCandles,
            IEnumerable<BacktestOperationModel> backtestOperationsIS,
            IEnumerable<BacktestOperationModel> backtestOperationsOS,
            ConfigurationBaseDTO configuration,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}