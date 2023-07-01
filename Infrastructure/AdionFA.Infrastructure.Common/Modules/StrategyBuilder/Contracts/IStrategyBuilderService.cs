using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Extractor.Model;
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
            AssemblyBuilderModel assembledBuilder,
            decimal maxCorrelation);

        // Backtest

        bool BuildBacktestOfNode(
            REPTreeNodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfAssemblyNode(
            REPTreeNodeModel backtestingAssemblyNode,
            IList<REPTreeNodeModel> childNodes,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfCrossingNode(
            StrategyNodeModel mainNode,
            REPTreeNodeModel backtestingCrossingNode,
            IEnumerable<Candle> mainCandles,
            IEnumerable<Candle> crossingCandles,
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}