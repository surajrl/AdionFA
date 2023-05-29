using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Contracts
{
    public interface IAssembledBuilderService
    {
        AssembledBuilderModel LoadStrategyBuilderResult(string projectName);

        // Extraction

        void CreateExtraction(
            string projectName,
            AssembledBuilderModel assembledBuilder,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration);

        // Backtest

        StrategyBuilderModel BuildBacktestOfNode(
            string nodeLabel,
            IList<string> parentNode,
            IList<BacktestModel> childNodes,
            ConfigurationBaseDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);


        // Serialization

        void SerializeBacktest(string projectName, BacktestModel backtestModel);

        BacktestModel DeserializeBacktest(string path);
    }
}
