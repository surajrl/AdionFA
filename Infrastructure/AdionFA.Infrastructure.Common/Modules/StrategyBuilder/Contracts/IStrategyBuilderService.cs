using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Project;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Contracts
{
    public interface IStrategyBuilderService
    {
        // Strategy

        CorrelationModel Correlation(string projectName, decimal correlation, EntityTypeEnum entityType);

        // Backtest

        StrategyBuilderModel BuildBacktestOfNode(
            string nodeLabel,
            IList<string> node,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        BacktestModel ExecuteBacktest(
            EntityTypeEnum entityType,
            string parentNodeLabel,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            IEnumerable<Candle> candles,
            IList<string> parentNode,
            IList<BacktestModel> childNodes,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}