using AdionFA.Infrastructure.Common.Weka.Contracts;
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Common.Weka.Services
{
    public static partial class WekaApiClientExtensions
    {
        public static IList<REPTreeOutputModel> GetREPTreeClassifier(
            this IWekaApiClient operations,
            string path,
            int? maxDepth = default,
            int? numDecimalPlaces = default,
            int? minSeed = default,
            int? maxSeed = default,
            int? instances = default,
            double? ratio = default,
            double? total = default,
            bool? isAssembled = default)
        {
            return Task.Factory.StartNew(s => ((IWekaApiClient)s).GetREPTreeClassifierAsync(
                path,
                maxDepth,
                numDecimalPlaces,
                minSeed,
                maxSeed,
                instances,
                ratio,
                total,
                isAssembled),
                operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }

        public static async Task<IList<REPTreeOutputModel>> GetREPTreeClassifierAsync(
            this IWekaApiClient operations,
            string path,
            int? maxDepth = default,
            int? numDecimalPlaces = default,
            int? minSeed = default,
            int? maxSeed = default,
            int? instances = default,
            double? ratio = default,
            double? total = default,
            bool? isAssembled = default,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default)
        {
            using var _result = await operations.GetREPTreeClassifierWithHttpMessagesAsync(
                path,
                maxDepth,
                numDecimalPlaces,
                minSeed,
                maxSeed,
                instances,
                ratio,
                total,
                isAssembled,
                null,
                cancellationToken).ConfigureAwait(false);

            return _result.Body;
        }
    }
}
