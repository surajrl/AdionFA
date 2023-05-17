using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Base;
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

        StrategyBuilderModel BacktestBuild(string nodeLabel, List<string> node, ConfigurationBaseDTO config, IEnumerable<Candle> candles, CancellationToken token);

        BacktestModel ExecuteBacktest(string nodeLabel, DateTime fromDate, DateTime toDate, List<string> node, IEnumerable<Candle> candles, int timeframeId, CancellationToken token);
    }
}