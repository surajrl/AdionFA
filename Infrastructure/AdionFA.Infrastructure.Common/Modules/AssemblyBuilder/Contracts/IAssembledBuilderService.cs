using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;
using System.Threading;

namespace AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts
{
    public interface IAssemblyBuilderService
    {
        AssemblyBuilderModel LoadAssemblyBuilder(string projectName);

        void BuildBacktestOfNode(
            string projectName,
            AssemblyBuilderModel assmebledBuilder,
            REPTreeNodeModel parentNode,
            IList<REPTreeNodeModel> childNodes,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken);

        void Correlation(
            string projectName,
            AssemblyBuilderModel assembledBuilder,
            decimal maxCorrelation);
    }
}
