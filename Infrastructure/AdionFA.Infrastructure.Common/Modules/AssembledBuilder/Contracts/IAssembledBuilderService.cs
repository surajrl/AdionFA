using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Contracts
{
    public interface IAssembledBuilderService
    {
        /// <summary>
        /// Loads the correlation backtests from the strategy builder process
        /// and the winning nodes saved from the assembled builder process.
        /// </summary>
        /// <param name="projectName"></param>
        AssembledBuilderModel LoadAssembledBuilderNodes(string projectName);

        // Backtest

        StrategyBuilderModel BuildBacktestOfNode(
            REPTreeNodeModel parentNode,
            IList<BacktestModel> childNodes,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);
    }
}
