using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Modules.Weka.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Contracts
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
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfAssemblyNode(
            AssemblyNodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            double meanSuccessRatePercentIS,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfStrategyNode(
            StrategyNodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}