using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Project;
using System;
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
            decimal correlation);

        // Backtest

        void BuildBacktestOfNode(
            REPTreeNodeModel node,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        void BuildBacktestOfCrossingNode(
            StrategyNodeModel strategyNode,
            REPTreeNodeModel backtestingNode,
            IEnumerable<Candle> mainCandles,
            IEnumerable<Candle> crossingCandles,
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        void ExecuteBacktest(
            BacktestModel backtest,
            EntityTypeEnum entityType,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            IEnumerable<Candle> candles,
            REPTreeNodeModel parentNode,
            IList<REPTreeNodeModel> childNodes,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}