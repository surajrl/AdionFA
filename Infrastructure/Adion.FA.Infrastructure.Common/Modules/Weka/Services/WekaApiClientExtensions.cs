using Adion.FA.Infrastructure.Common.Weka.Contracts;
using Adion.FA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Adion.FA.Infrastructure.Common.Weka.Services
{
    public static partial class WekaApiClientExtensions
    {
        public static IList<REPTreeOutputModel> GetREPTreeClassifier(
            this IWekaApiClient operations,
            string path,
            int? maxDepth = default(int?),
            int? numDecimalPlaces = default(int?),
            int? minSeed = default(int?),
            int? maxSeed = default(int?),
            int? instances = default(int?),
            double? ratio = default(double?),
            double? total = default(double?),
            bool? isAssembled = default(bool?))
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
                isAssembled), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }

        public static async Task<IList<REPTreeOutputModel>> GetREPTreeClassifierAsync(
            this IWekaApiClient operations,
            string path,
            int? maxDepth = default(int?),
            int? numDecimalPlaces = default(int?),
            int? minSeed = default(int?),
            int? maxSeed = default(int?),
            int? instances = default(int?),
            double? ratio = default(double?),
            double? total = default(double?),
            bool? isAssembled = default(bool?),
            Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            using (var _result = await operations.GetREPTreeClassifierWithHttpMessagesAsync(
                path,
                maxDepth,
                numDecimalPlaces,
                minSeed,
                maxSeed,
                instances,
                ratio,
                total,
                isAssembled,
                null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
