﻿using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using Adion.FA.TransferObject.Project;
using System.Collections.Generic;

namespace Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Contracts
{
    public interface IAssembledBuilderService
    {
        #region Assembled
        AssembledBuilderModel LoadStrategyModel(string projectName);

        void ExtractorExecute(
            string projectName, AssembledBuilderModel model, IEnumerable<Candle> candles, ProjectConfigurationDTO config);

        public void Build(string projectName, ProjectConfigurationDTO config, IEnumerable<Candle> candles);
        #endregion

        #region Serialization
        void BacktestSerialize(string projectName, BacktestModel model);
        BacktestModel BacktestDeserialize(string path);
        #endregion
    }
}
