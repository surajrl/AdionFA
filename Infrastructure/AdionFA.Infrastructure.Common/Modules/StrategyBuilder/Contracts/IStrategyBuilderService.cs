using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Contracts
{
    public interface IStrategyBuilderService
    {
        // Strategy

        CorrelationModel Correlation(string projectName, decimal correlation, EntityTypeEnum entityType);

        // Backtest

        StrategyBuilderModel BuildBacktest(
            string nodeLabel,
            List<string> node,
            ConfigurationDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}