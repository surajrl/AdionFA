using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Contracts
{
    public interface IAssembledBuilderService
    {
        AssembledBuilderModel LoadAssembledBuilder(string projectName);

        void BuildBacktestOfNode(
            string projectName,
            AssembledBuilderModel assmebledBuilder,
            REPTreeNodeModel parentNode,
            IList<REPTreeNodeModel> childNodes,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        void Correlation(
            string projectName,
            AssembledBuilderModel assembledBuilder,
            decimal maxCorrelation);
    }
}
