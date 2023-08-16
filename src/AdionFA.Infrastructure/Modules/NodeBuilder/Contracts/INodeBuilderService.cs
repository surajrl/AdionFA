using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Modules.Strategy;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.NodeBuilder.Contracts
{
    public interface INodeBuilderService
    {
        // Correlation

        IList<TNode> Correlation<TNode>(
            string projectName,
            EntityTypeEnum entityType,
            decimal maxCorrelation) where TNode : INodeModel;

        // Backtest

        bool BuildBacktestOfNode(
            SingleNodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfAssemblyNode(
            AssemblyNodeModel backtestingNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            int timeframeId,
            decimal meanSuccessRatePercentIS,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        bool BuildBacktestOfStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> mainCandles,
            ProjectConfigurationDTO projectConfiguration,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}