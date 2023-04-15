using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;

using AdionFA.TransferObject.Project;

using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Contracts
{
    public interface IAssembledBuilderService
    {
        AssembledBuilderModel LoadStrategyModel(string projectName);

        void ExtractorExecute(
            string projectName, AssembledBuilderModel model, IEnumerable<Candle> candles, ProjectConfigurationDTO config);

        public void Build(string projectName, ProjectConfigurationDTO config, IEnumerable<Candle> candles);

        // Serialization
        void BacktestSerialize(string projectName, BacktestModel model);
        BacktestModel BacktestDeserialize(string path);
    }
}
