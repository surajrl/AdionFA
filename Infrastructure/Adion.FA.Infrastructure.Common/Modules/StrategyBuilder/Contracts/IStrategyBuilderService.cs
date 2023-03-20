using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.TransferObject.Base;
using System;
using System.Collections.Generic;

namespace Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Contracts
{
    public interface IStrategyBuilderService
    {
        #region Strategy
        CorrelationModel Correlation(string projectName, decimal correlation, EntityTypeEnum entityType);
        #endregion

        #region Backtest
        StrategyBuilderModel BacktestBuild(string label, List<string> node, ConfigurationBaseDTO config, IEnumerable<Candle> data);
        BacktestModel BacktestExecute(string label, DateTime fromDate, DateTime toDate, List<string> node, IEnumerable<Candle> data, int periodId, decimal variation = 0);
        #endregion

        #region Serialization
        void BacktestSerialize(string projectName, BacktestModel model);
        BacktestModel BacktestDeserialize(string path);
        #endregion
    }
}
