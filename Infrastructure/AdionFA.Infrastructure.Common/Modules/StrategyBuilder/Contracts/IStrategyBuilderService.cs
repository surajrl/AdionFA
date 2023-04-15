using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Base;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Contracts
{
    public interface IStrategyBuilderService
    {
        // Strategy

        CorrelationModel Correlation(string projectName, decimal correlation, EntityTypeEnum entityType);

        // Backtest

        StrategyBuilderModel BacktestBuild(string label, List<string> node, ConfigurationBaseDTO config, IEnumerable<Candle> data);
        BacktestModel BacktestExecute(string label, DateTime fromDate, DateTime toDate, List<string> node, IEnumerable<Candle> data, int periodId, decimal variation = 0);

        // Serialization

        void BacktestSerialize(string projectName, BacktestModel model);
        BacktestModel BacktestDeserialize(string path);
    }
}
