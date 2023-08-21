using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public interface IBuilderService
    {
        // Correlation

        IList<TNode> Correlation<TNode>(
            string projectName,
            EntityTypeEnum entityType,
            decimal maxCorrelation) where TNode : INodeModel;

        // Backtest

        bool BuildBacktestOfSingleNode(
            SingleNodeModel singleNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            CancellationToken cancellationToken);

        bool BuildBacktestOfAssemblyNode(
            AssemblyNodeModel assemblyNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            decimal meanSuccessRatePercentIS,
            CancellationToken cancellationToken);

        bool BuildBacktestOfStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            decimal previousSuccessRatePercentIS,
            decimal previousSuccessRatePercentOS,
            CancellationToken cancellationToken);
    }
}